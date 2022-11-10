using DO;

namespace Dal;
public class DalProduct
{
    #region Add
    /// <summary>
    /// The add method receives a new order object,
    /// updates it with an automatic object codeת
    /// and saves it in the next place in the array 
    /// </summary>
    /// <returns>Returns the id of the new order</returns>
    public int Add(Product myProduct)
    {
        myProduct.Id= DataSource.Config._IdentifyProduct;
        DataSource.productArr[DataSource.Config._IndexProduct++] = myProduct;
        return myProduct.Id;
    }
    #endregion

    #region Get
    /// <summary>
    /// Get exist order's id and scan the array's order
    /// </summary>
    /// <returns>A copy of the object whose id is equal to the received id</returns>
    /// <exception cref="Exception">Throw exception if not exists</exception>

    public Product Get(int myId)
    {
        for(int i = 0; i < DataSource.Config._IndexProduct; i++)
        {
            if (DataSource.productArr[i].Id==myId)
                return DataSource.productArr[i];
        }
        throw new Exception("no product found with this ID");
    }
    #endregion

    #region GetAll
    /// <summary>
    /// copy the exist array for temp array
    /// </summary>
    /// <returns>the temp array</returns>
    public Product[] GetAll()
    {
        Product[] tmpProductArr = new Product[DataSource.Config._IndexProduct];
        for (int i = 0; i < DataSource.Config._IndexProduct; i++)
            tmpProductArr[i] = DataSource.productArr[i];
        return tmpProductArr;
    }
    #endregion

    #region Delete
    /// <summary>
    /// Searches for the object whose id was received,
    /// deletes it from the array and updates the number of full places in the array
    /// </summary>
    /// <exception cref="Exception">Throw exception if not exists</exception>
    public void Delete(int myId)
    {
        for (int i = 0; i < DataSource.Config._IndexProduct; i++)
        {
            if (DataSource.productArr[i].Id == myId)
            {
                DataSource.productArr[i] = DataSource.productArr[--DataSource.Config._IndexProduct];
                return;
            }

        }
        throw new Exception("no product found to delete with this ID");

    }
    #endregion

    #region Update
    /// <param name="myOrder">Gets an object whose id already exists in another object</param>
    /// <summary>
    /// Searches for the object in the array
    /// whose id is equal to the received id and
    /// changes the rest of its details to the details of the new object
    /// </summary>
    /// <exception cref="Exception">If the id does not exist yet</exception>
    public void Update(Product myProduct)
    {
        for (int i = 0; i < DataSource.Config._IndexProduct; i++)
        {
            if (DataSource.productArr[i].Id == myProduct.Id)
            {
                DataSource.productArr[i] = myProduct;
                return;
            }
        }
        throw new Exception("no product found to update with this ID");

    }
    #endregion
}
