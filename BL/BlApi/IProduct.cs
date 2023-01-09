using BO;

namespace BlApi;
public interface IProduct
{
    /// <summary>
    /// Get All Product
    /// </summary>
    /// <returns>list of all the products details</returns>
    public IEnumerable<ProductForList?> GetAllProduct();
    /// <summary>
    /// Get Product By Id
    /// </summary>
    /// <param name="myId"></param>
    /// <returns>details of product with the id param</returns>
    public BO.Product GetProductById(int myId);
    /// <summary>
    /// Get Product By Id
    /// </summary>
    /// <param name="myId"></param>
    /// <param name="myCart"></param>
    /// <returns>Product details with this id number in relation to the customer's shopping cart</returns>
    public BO.ProductItem GetProductById(int myId,Cart myCart);
    /// <summary>
    /// Add Product
    /// </summary>
    /// <param name="myProduct"></param>
    public void AddProduct(Product myProduct);
    /// <summary>
    /// Delete Product
    /// </summary>
    /// <param name="myId"></param>
    public void DeleteProduct(int myId);
    /// <summary>
    /// Update Product
    /// </summary>
    /// <param name="myProduct"></param>
    public void UpdateProduct(Product myProduct);
    /// <summary>
    /// get product by category
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    public IEnumerable<ProductForList?> GetProductsByCategory(BO.Enums.Category category);
    public IEnumerable<ProductItem?> GetAllProductItems(BO.Cart myCart);
    public IEnumerable<ProductItem?> GetProductItemsByCategory(BO.Cart myCart,BO.Enums.Category? category);

}
