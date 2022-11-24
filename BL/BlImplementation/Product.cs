using BlApi;
using Dal;
//using DalApi;

namespace BlImplementation;

internal class Product:IProduct
{
   private DalApi.IDal _dal = new DalList();
    public IEnumerable<BO.ProductForList> GetAllProduct()
    {
        IEnumerable<DO.Product> productListFromDo = _dal.Product.GetAll();
        List<BO.ProductForList> productForList= new List<BO.ProductForList>();
        foreach(DO.Product product in productListFromDo)
        {
            BO.ProductForList tmp = new BO.ProductForList() { 
                Id = product.Id,
                Price= product.Price,
                Name = product.Name,
                Category = (BO.Enums.Category)product.Category
            };
            productForList.Add(tmp);
        }
        return productForList;
    }
    public BO.Product GetProductById(int myId)
    {
        if (myId > 0)
        {
            try
            {
                DO.Product productFromDo = _dal.Product.Get(myId);
                BO.Product product = new BO.Product() { 
                    Id = productFromDo.Id,
                    Name = productFromDo.Name, 
                    Price = productFromDo.Price, 
                    Category = (BO.Enums.Category)productFromDo.Category,
                    InStock = productFromDo.InStock };
                return product;
            }
            catch (DalApi.NoFoundItemExceptions ex) 
            {
                throw new BO.NoFoundItemExceptions("no found item with this id",ex);
            }
        }
        throw new BO.InvalidValueException("invalid id");
    }
    private int _amountOfProductInCart(int myId,BO.Cart cart)
    {
        foreach (BO.OrderItem orderItem in cart.OrderItemList)
        {
            if (orderItem.ProductId == myId)
                return orderItem.AmountInCart;
        }
        return 0;
    }
    public BO.ProductItem GetProductById(int myId, BO.Cart myCart)
    {
        if (myId > 0)
        {
            try
            {
                DO.Product productFromDo = _dal.Product.Get(myId);

                BO.ProductItem productItem = new BO.ProductItem()
                {
                    Id = productFromDo.Id,
                    Name = productFromDo.Name,
                    Price = productFromDo.Price,
                    Category = (BO.Enums.Category)productFromDo.Category,
                    InStock = productFromDo.InStock > 0,
                    AmountInCart = _amountOfProductInCart(myId, myCart)
                };
                return productItem;
            }
            catch (DalApi.NoFoundItemExceptions ex)
            {
                throw new BO.NoFoundItemExceptions("no found product with this id",ex);
            }
        }
        throw new BO.InvalidValueException("invalid id");
    }
    public void AddProduct(BO.Product myProduct)
    {
        if (myProduct.Id < 0)
            throw new BO.InvalidValueException("invalid id");
        if (myProduct.Name == "")
            throw new BO.InvalidValueException("invalid name");
        if (myProduct.Price < 0)
            throw new BO.InvalidValueException("invalid price");
        if (myProduct.InStock < 0)
            throw new BO.InvalidValueException("invalid amount in stock");
        try
        {
            DO.Product productToAdd = new DO.Product(myProduct.Id, myProduct.Name,myProduct.Price,(DO.Enums.Category)myProduct.Category,myProduct.InStock);
            _dal.Product.Add(productToAdd);
        }
        catch(DalApi.ItemAlresdyExsistException ex)
        {
            throw new BO.ItemAlresdyExsistException("this product already exist",ex);
        }
    }
    public void DeleteProduct(int myId)
    {
        IEnumerable<DO.OrderItem> productListFromDo = _dal.OrderItem.GetAll();

        foreach(DO.OrderItem orderItem in productListFromDo)
        {
            if (orderItem.ProductId == myId)
            {
                DO.Order order = _dal.Order.Get(orderItem.OrderId);
                if (order.ShipDate <= DateTime.Now) return;
                else throw new BO.ProductInOrderException("This product cannot be deleted, it is on order");
            }
        }

        try
        {
            _dal.Product.Delete(myId);
        }
        catch(DalApi.NoFoundItemExceptions ex)
        {
            throw new BO.NoFoundItemExceptions("no found product with this id to delete",ex);
        }
    }
    public void UpdateProduct(BO.Product myProduct)
    {
        if (myProduct.Id < 0)
            throw new BO.InvalidValueException("invalid id");
        if (myProduct.Name == "")
            throw new BO.InvalidValueException("invalid name");
        if (myProduct.Price < 0)
            throw new BO.InvalidValueException("invalid price");
        if (myProduct.InStock < 0)
            throw new BO.InvalidValueException("invalid amount in stock");
        try
        {
            DO.Product product=new DO.Product(myProduct.Id,myProduct.Name, myProduct.Price,(DO.Enums.Category)myProduct.Category, myProduct.InStock);
            _dal.Product.Update(product);
        }
        catch (DalApi.NoFoundItemExceptions ex)
        {
            throw new BO.NoFoundItemExceptions("no found product with this id to update",ex);
        }
    }

}

