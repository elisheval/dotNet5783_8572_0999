
using BO;

namespace BlApi;
public interface IProduct
{
    public IEnumerable<ProductForList> GetAllProduct();
    public BO.Product GetProductById(int myId);
    public BO.Product GetProductById(int myId,Cart myCart);
    public void AddProduct(Product myProduct);
    public void DeleteProduct(int myId);
    public void UpdateProduct(Product myProduct);

}
