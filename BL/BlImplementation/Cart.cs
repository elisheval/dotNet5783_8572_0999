using BlApi;
using Dal;
using DalApi;
using System.Net.Mail;

namespace BlImplementation;
internal class Cart : ICart
{
    private DalApi.IDal _dal = new DalList();
    private bool ProductInStock(int productId)
    {
        IEnumerable<DO.Product> products = _dal.Product.GetAll();
        foreach (DO.Product product in products)
        {
            if (product.Id == productId)
            {
                if (product.InStock > 0) return true;
                else return false;
            }
        }
        return false;
    }
    public BO.Cart AddProductToCart(int productId, BO.Cart myCart)
    {
            foreach (BO.OrderItem orderItem in myCart.OrderItemList)
            {
                if (orderItem.ProductId == productId)
                {
                    if (ProductInStock(productId))
                    {
                        DO.Product product = _dal.Product.Get(productId);
                        orderItem.AmountInCart++;
                        orderItem.TotalPriceForItem += product.Price;
                        orderItem.Price = product.Price;
                        myCart.TotalOrderPrice += product.Price;
                        return myCart;
                    }
                    else
                    {
                        throw new BO.ProductOutOfStockException("Product is out of stock");
                    }
                }
            }
        //the product is not in the cart

            foreach(DO.Product product in _dal.Product.GetAll())
            {
                if(product.Id == productId)
                {
                  if (product.InStock > 0){
                        BO.OrderItem orderItemToAddToCart=new BO.OrderItem() {
                            ProductId = productId,
                            ProductName=product.Name,
                            Price=product.Price,
                            AmountInCart=1,
                            TotalPriceForItem=product.Price
                        };
                        myCart.OrderItemList.Add(orderItemToAddToCart);
                    return myCart;
                    }
                  else throw new BO.ProductOutOfStockException("Product is out of stock");
                }
            }
            throw new BO.NoFoundItemExceptions("No product with this ID exists");
        }
    public BO.Cart UpdateAmountOfProductInCart(int productId, BO.Cart myCart, int newAmount)
    {
        if (newAmount < 0)
            throw new BO.InvalidValueException("invalid amount");
        if (productId < 0)
            throw new BO.InvalidValueException("invalid product id");
        foreach (BO.OrderItem orderItem in myCart.OrderItemList)
        {
            if (orderItem.ProductId == productId)
            {
                if (newAmount == 0)
                {
                    myCart.OrderItemList.Remove(orderItem);
                }
                else
                {
                    IEnumerable<DO.Product> products = _dal.Product.GetAll();
                    foreach (DO.Product product in products)
                    {
                        if (product.InStock == 0)
                        {
                            myCart.TotalOrderPrice -= orderItem.Price * orderItem.AmountInCart;
                            _dal.OrderItem.Delete(orderItem.Id);
                            throw new BO.ProductOutOfStockException("product is out of stock");
                        }
                        else if (product.InStock >= newAmount)
                        {
                            orderItem.TotalPriceForItem = newAmount * product.Price;
                            myCart.TotalOrderPrice += product.Price * (newAmount - orderItem.AmountInCart);
                            orderItem.AmountInCart = newAmount;
                            return myCart;
                        }
                        else if (product.InStock < newAmount)
                        {
                            orderItem.TotalPriceForItem = product.InStock * product.Price;
                            myCart.TotalOrderPrice += product.Price * (product.InStock - orderItem.AmountInCart);
                            orderItem.AmountInCart = product.InStock;
                            return myCart;
                        }
                    }
                }

            }
        }
        throw new BO.ProductNotInCartException("Product does not exist in the cart");
    }
    private bool IsValid(string email)
    {
        bool valid = true;
        try { MailAddress emailAddress = new MailAddress(email); }
        catch{ valid = false; }
        return valid;
    }
    public void OrderConfirmation(BO.Cart myCart, string customerName, string customerEmail, string customerAddress)
    {
        if (customerName == null)
            throw new BO.InvalidValueException("invalid name");
        if (customerAddress == null)
            throw new BO.InvalidValueException("invalid address");
        if (customerEmail!=null&&IsValid(customerEmail))
            throw new BO.InvalidValueException("invalid email");

        DO.Order order = new DO.Order(customerName,customerEmail,customerAddress,DateTime.Now,DateTime.MinValue,DateTime.MinValue);
        int orderId=_dal.Order.Add(order);
        string massegeOfLackProducts = "";
        foreach(BO.OrderItem orderItem in myCart.OrderItemList)
        {
            try
            {
                DO.Product productFromDo = _dal.Product.Get(orderItem.ProductId);
                if (productFromDo.InStock == 0)
                    massegeOfLackProducts+= productFromDo.Name + " is out of stock";
               else if (productFromDo.InStock<orderItem.AmountInCart)
                {
                    DO.OrderItem orderItemToAdd = new DO.OrderItem(productFromDo.Id, orderId, productFromDo.Price, productFromDo.InStock);
                    int orderItemId = _dal.OrderItem.Add(orderItemToAdd);
                    productFromDo.InStock = 0;
                    _dal.Product.Update(productFromDo);
                    massegeOfLackProducts +=(orderItem.AmountInCart-productFromDo.InStock)+ " out of stock ,you will get  " + productFromDo.InStock + " insted of" + orderItem.AmountInCart; 
                }
                else 
                {
                    DO.OrderItem orderItemToAdd = new DO.OrderItem(productFromDo.Id, orderId, productFromDo.Price,orderItem.AmountInCart);
                    int orderItemId = _dal.OrderItem.Add(orderItemToAdd);
                    productFromDo.InStock-= orderItem.AmountInCart;
                    _dal.Product.Update(productFromDo);
                }
            }
            catch(DalApi.NoFoundItemExceptions ex)
            {
                throw new BO.NoFoundItemExceptions("No product with this ID exists",ex);
            }
        }
        if (massegeOfLackProducts != "")
        {
            throw new BO.ProductOutOfStockException(" the order completed' some items is out of stock, "+massegeOfLackProducts);
        }
    }
}
