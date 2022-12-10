﻿using BlApi;
using Dal;
using DalApi;
using System.Net.Mail;

namespace BlImplementation;
internal class Cart : ICart
{
    private DalApi.IDal _dal = new DalList();

    #region private productInStock
    /// <param name="productId">get id of product</param>
    /// <summary>
    /// check if product in stock
    /// </summary>
    /// <returns>true- if in, false- if not in</returns>
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
    #endregion

    #region AddProductToCart
    /// <param name="productId">get product id</param>
    /// <param name="myCart">get list of the product in the cart</param>
    /// <summary>
    /// if the product not exist in the cart:add the product to the cart
    /// if exist: add 1 to the amount of the product
    /// </summary>
    /// <returns>the new cart after change</returns>
    /// <exception cref="BO.ProductOutOfStockException">if not in stock</exception>
    /// <exception cref="BO.NoFoundItemExceptions">if not exist product with that id</exception>
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
                        myCart.TotalOrderPrice += product.Price*orderItem.AmountInCart-orderItem.TotalPriceForItem;
                        orderItem.TotalPriceForItem = product.Price*orderItem.AmountInCart;
                        orderItem.Price = product.Price;
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
                    myCart.TotalOrderPrice += product.Price;
                    myCart.OrderItemList.Add(orderItemToAddToCart);
                    return myCart;
                    }
                  else throw new BO.ProductOutOfStockException("Product is out of stock");
                }
            }
            throw new BO.NoFoundItemExceptions("No product with this ID exists");
        }

    #endregion

    #region UpdateAmountOfProductInCart
    /// <param name="productId">get id of exist product</param>
    /// <param name="myCart">get the cart</param>
    /// <param name="newAmount">get new amount to update</param>
    /// <summary>
    /// if the amount increase: works like add
    /// if decrease: change the amount of the in the cart
    /// if equal to zero: delete the product from the order and the cart
    /// </summary>
    /// <returns>the new cart after changing</returns>
    /// <exception cref="BO.InvalidValueException"> if the amount or the product id not valid</exception>
    /// <exception cref="BO.ProductOutOfStockException">if he want to increase and the product not in stock</exception>
    /// <exception cref="BO.ProductNotInCartException">if product not in cart</exception>
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
                        if (product.Id == productId)
                        {
                            if (product.InStock == 0)
                                throw new BO.ProductOutOfStockException("product is out of stock");
                            else if (product.InStock >= newAmount)
                            {
                                myCart.TotalOrderPrice += product.Price * newAmount-orderItem.TotalPriceForItem;
                                orderItem.TotalPriceForItem = newAmount * product.Price;
                                orderItem.AmountInCart = newAmount;
                                orderItem.Price=product.Price;
                                return myCart;
                            }
                            else if (product.InStock < newAmount)
                            {
                                if (product.InStock > orderItem.AmountInCart)
                                {
                                    myCart.TotalOrderPrice += product.Price * product.InStock - orderItem.TotalPriceForItem;
                                    orderItem.TotalPriceForItem = product.InStock * product.Price;
                                    orderItem.AmountInCart = product.InStock;
                                    orderItem.Price = product.Price;
                                    return myCart;
                                }
                                throw new BO.ProductOutOfStockException("It is not possible to add in quantity because it is out of stock\r\n\r\n");
                            }
                        }
                    }
                }

            }
        }
        throw new BO.ProductNotInCartException("Product does not exist in the cart");
    }
    #endregion

    #region OrderConfirmation
    /// <param name="email">get mail</param>
    /// <summary>
    /// inner method check if mail address is valid
    /// </summary>
    /// <returns>true- if valid, false- if not</returns>
    private bool IsValid(string email)
    {
        bool valid = true;
        try { MailAddress emailAddress = new MailAddress(email); }
        catch{ valid = false; }
        return valid;
    }


    /// <param name="myCart">get cart</param>
    /// get details of customer:
    /// <param name="customerName">name</param>
    /// <param name="customerEmail">email</param>
    /// <param name="customerAddress>address</param>
    /// <summary>
    /// perform the order
    /// </summary>
    /// <exception cref="BO.InvalidValueException"></exception>
    /// <exception cref="BO.NoFoundItemExceptions"></exception>
    /// <exception cref="BO.ProductOutOfStockException"></exception>
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
                    orderItem.Id = orderItemId;
                    _dal.Product.Update(productFromDo);
                    massegeOfLackProducts +=(orderItem.AmountInCart-productFromDo.InStock)+ " out of stock ,you will get  " + productFromDo.InStock + " insted of" + orderItem.AmountInCart;
                    myCart.TotalOrderPrice += productFromDo.InStock * productFromDo.Price- orderItem.AmountInCart * orderItem.Price;
                    productFromDo.InStock = 0;

                }
                else 
                {
                    DO.OrderItem orderItemToAdd = new DO.OrderItem(productFromDo.Id, orderId, productFromDo.Price,orderItem.AmountInCart);
                    int orderItemId = _dal.OrderItem.Add(orderItemToAdd);
                    productFromDo.InStock-= orderItem.AmountInCart;
                    _dal.Product.Update(productFromDo);
                    myCart.TotalOrderPrice += productFromDo.InStock * productFromDo.Price - orderItem.AmountInCart * orderItem.Price;
                }
            }
            catch(DO.NoFoundItemExceptions ex)
            {
                throw new BO.NoFoundItemExceptions("No product with this ID exists",ex);
            }
        }
        if (massegeOfLackProducts != "")
        {
            throw new BO.ProductOutOfStockException(" the order completed' some items is out of stock, "+massegeOfLackProducts);
        }
    }
    #endregion
}
