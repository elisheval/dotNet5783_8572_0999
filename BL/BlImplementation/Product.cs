using BlApi;
using BO;
using Dal;
using DalApi;

namespace BlImplementation;

internal class Product:IProduct
{
   private IDal Dal = new DalList();
    public IEnumerable<BO.ProductForList> GetAllProduct()
    {
        IEnumerable<DO.Product> productListFromDo = Dal.Product.GetAll();
        List<BO.ProductForList> productForList= new List<BO.ProductForList>();
        foreach(DO.Product product in productListFromDo)
        {
            int tmpId = product.Id;
            string tmpName = product.Name;
            double tmpPrice = product.Price;
            Enum tmpCategory=(BO.Enums.Category)product.Category;
            BO.ProductForList tmp = new BO.ProductForList() { Id=tmpId,Price=tmpPrice,Name=tmpName,Category= (BO.Enums.Category)tmpCategory };
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
                DO.Product productFromDo = Dal.Product.Get(myId);
                int tmpId=productFromDo.Id;
                string tmpName = productFromDo.Name;
                double tmpPrice = productFromDo.Price;
                Enum tmpCategory=(BO.Enums.Category)productFromDo.Category;
                int tmpInStock=productFromDo.InStock;
                BO.Product product = new BO.Product() { Id= tmpId ,Name= tmpName ,Price= tmpPrice, Category=(BO.Enums.Category)tmpCategory ,InStock= tmpInStock };
                return product;
            }
            catch (NoFoundItemExceptions ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
        throw new InvalidValueException("invalid value");
    }
    public Product GetProductById(int myId, Cart myCart)
    {

    }


}
