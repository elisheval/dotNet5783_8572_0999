
using DO;

namespace Dal;

public class DalProduct
{
    public int Add(Product myProduct)
    {
        myProduct.Id= DataSource.Config.IdentifyProduct;
        DataSource.productArr[DataSource.Config.IndexProduct++] = myProduct;
        return myProduct.Id;
    }
    public Product Get(int myId)
    {
        for(int i = 0; i < DataSource.Config.IndexProduct; i++)
        {
            if (DataSource.productArr[i].Id==myId)
                return DataSource.productArr[i];
        }
        throw new Exception("no product found with this ID");
    }


    public Product[] GetAll()
    {
        Product[] tmpProductArr = new Product[DataSource.Config.IndexProduct];
        for (int i = 0; i < DataSource.Config.IndexProduct; i++)
            tmpProductArr[i] = DataSource.productArr[i];
        return tmpProductArr;
    }

    public void Delete(int myId)
    {
        for (int i = 0; i < DataSource.Config.IndexProduct; i++)
        {
            if (DataSource.productArr[i].Id == myId)
            {
                DataSource.productArr[i] = DataSource.productArr[--DataSource.Config.IndexProduct];
                return;
            }

        }
        throw new Exception("no product found to delete with this ID");

    }
    public void Update(Product myProduct)
    {
        for (int i = 0; i < DataSource.Config.IndexProduct; i++)
        {
            if (DataSource.productArr[i].Id == myProduct.Id)
            {
                DataSource.productArr[i] = myProduct;
                return;
            }
        }
        throw new Exception("no product found to update with this ID");

    }
}
