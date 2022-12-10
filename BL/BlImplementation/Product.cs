using BlApi;
using Dal;
//using DalApi;

namespace BlImplementation;

internal class Product : IProduct
{
    private DalApi.IDal _dal = new DalList();

    #region GetAllProduct
    /// <summary>
    /// The function take the list product from the data entity,
    /// and make from it new list from the logic entity
    /// </summary>
    /// <returns>the new logic entity list</returns>
    public IEnumerable<BO.ProductForList> GetAllProduct()
    {
        IEnumerable<DO.Product> productListFromDo = _dal.Product.GetAll();
        List<BO.ProductForList> productForList = new List<BO.ProductForList>();
        foreach (DO.Product product in productListFromDo)//convert the data entity properties to the logic entity propeties
        {
            BO.ProductForList tmp = new BO.ProductForList()
            {
                Id = product.Id,
                Price = product.Price,
                Name = product.Name,
                Category = (BO.Enums.Category)product.Category
            };
            productForList.Add(tmp);//add the new item to the list
        }
        return productForList;
    }
    #endregion

    #region GetProductById manager
    /// <summary>
    /// get Id of specific product and get all the product from the data entity
    /// </summary>
    /// <param name="myId"></param>
    /// <returns>return the product</returns>
    /// <exception cref="BO.NoFoundItemExceptions">if the id  </exception>
    /// <exception cref="BO.InvalidValueException">if the id not valid</exception>
    public BO.Product GetProductById(int myId)
    {
        if (myId > 0)
        {
            try
            {
                DO.Product productFromDo = _dal.Product.Get(myId);
                BO.Product product = new BO.Product()
                {
                    Id = productFromDo.Id,
                    Name = productFromDo.Name,
                    Price = productFromDo.Price,
                    Category = (BO.Enums.Category)productFromDo.Category,
                    InStock = productFromDo.InStock
                };
                return product;
            }
            catch (DO.NoFoundItemExceptions ex)
            {
                throw new BO.NoFoundItemExceptions("no found item with this id", ex);
            }
        }
        throw new BO.InvalidValueException("invalid id");
    }
    #endregion

    #region  private _amountOfProductInCart
    /// <param name="myId">get id of product</param>
    /// <param name="cart">get list with the items in this cart</param>
    /// <summary>
    /// inner method, looking for that product in th cart
    /// </summary
    /// <returns>return the amount of this product</returns>
    private int _amountOfProductInCart(int myId, BO.Cart cart)
    {
        foreach (BO.OrderItem orderItem in cart.OrderItemList)
        {
            if (orderItem.ProductId == myId)
                return orderItem.AmountInCart;
        }
        return 0;
    }
    #endregion

    #region GetProductById client
    /// <param name="myId">get id of product</param>
    /// <param name="myCart">get cart</param>
    /// <summary>
    /// take data of product and convert it to product object in logic entity also with the data cart
    /// </summary>
    /// <returns>the created object</returns>
    /// <exception cref="BO.NoFoundItemExceptions">if the product not exist</exception>
    /// <exception cref="BO.InvalidValueException">if the id not exist </exception>
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
            catch (DO.NoFoundItemExceptions ex)
            {
                throw new BO.NoFoundItemExceptions("no found product with this id", ex);
            }
        }
        throw new BO.InvalidValueException("invalid id");
    }
    #endregion///

    #region AddProduct
    /// <param name="myProduct">get new product</param>
    /// <summary>
    ///add new product, convert it to the data entity and add the created object to existing list products(in the data)
    /// </summary>
    /// <exception cref="BO.InvalidValueException">if the data in the getter object not valid</exception>
    /// <exception cref="BO.ItemAlresdyExsistException">if the product already exist in the list products</exception>
    public void AddProduct(BO.Product myProduct)
    {
        if (myProduct.Id < 0|| myProduct.Id > 999999 || myProduct.Id < 100000)
            throw new BO.InvalidValueException("invalid id");
        if (myProduct.Name == "")
            throw new BO.InvalidValueException("invalid name");
        if (myProduct.Price < 0)
            throw new BO.InvalidValueException("invalid price");
        if (myProduct.InStock < 0)
            throw new BO.InvalidValueException("invalid amount in stock");
        try
        {
            DO.Product productToAdd = new DO.Product(myProduct.Id, myProduct.Name, myProduct.Price, (DO.Enums.Category)myProduct.Category, myProduct.InStock);
            _dal.Product.Add(productToAdd);
        }
        catch (DO.ItemAlresdyExsistException ex)
        {
            throw new BO.ItemAlresdyExsistException("this product already exist", ex);
        }
    }
    #endregion

    #region DeleteProduct
    /// <param name="myId">get id of product</param>
    /// <summary>
    /// delete the existig product in the products list in data entity
    /// </summary>
    /// <exception cref="BO.ProductInOrderException">if the product in order</exception>
    /// <exception cref="BO.NoFoundItemExceptions">if the product not exists</exception>
    public void DeleteProduct(int myId)
    {
        IEnumerable<DO.OrderItem> productListFromDo = _dal.OrderItem.GetAll();

        foreach (DO.OrderItem orderItem in productListFromDo)
        {
            if (orderItem.ProductId == myId)
            {
                DO.Order order = _dal.Order.Get(orderItem.OrderId);
                if (order.ShipDate == DateTime.MinValue)
                    throw new BO.ProductInOrderException("This product cannot be deleted, it is on order");
            }
        }
        try
        {
            _dal.Product.Delete(myId);
        }
        catch (DO.NoFoundItemExceptions ex)
        {
            throw new BO.NoFoundItemExceptions("no found product with this id to delete", ex);
        }
    }
    #endregion

    #region UpdateProduct
    /// <param name="myProduct">get new product</param>
    /// <summary>
    /// convert it to be data entity object, looking for the id in the products list and update the exists object with this object
    /// </summary>
    /// <exception cref="BO.InvalidValueException">the data not valid</exception>
    /// <exception cref="BO.NoFoundItemExceptions">the product not exist</exception>
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
            DO.Product product = new DO.Product(myProduct.Id, myProduct.Name, myProduct.Price, (DO.Enums.Category)myProduct.Category, myProduct.InStock);
            _dal.Product.Update(product);
        }
        catch (DO.NoFoundItemExceptions ex)
        {
            throw new BO.NoFoundItemExceptions("no found product with this id to update", ex);
        }
    }


    #endregion

}

