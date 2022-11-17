using DO;
using System.Collections.Generic;
using DalApi;

namespace Dal;
internal class DalProduct:IProduct
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
        DataSource.productList.Add(myProduct);
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
        for(int i = 0; i < DataSource.productList.Count; i++)
        {
            if (DataSource.productList[i].Id==myId)
                return DataSource.productList[i];
        }
        throw new NoFoundItemExceptions("no product found with this ID");
    }
    #endregion

    #region GetAll
    /// <summary>
    /// copy the exist array for temp array
    /// </summary>
    /// <returns>the temp array</returns>
    public IEnumerable<Product> GetAll()
    {
        List<Product> tmpProductList = new List<Product>();
        for (int i = 0; i < DataSource.productList.Count; i++)
        {
            tmpProductList.Add(DataSource.productList[i]);
        }
        return tmpProductList;
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
        for (int i = 0; i < DataSource.productList.Count; i++)
        {
            if (DataSource.productList[i].Id == myId)
            {
                Product tmpProduct=DataSource.productList[i];
                DataSource.productList.Remove(tmpProduct);
                return;
            }

        }
        throw new NoFoundItemExceptions("no product found to delete with this ID");

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
        for (int i = 0; i < DataSource.productList.Count; i++)
        {
            if (DataSource.productList[i].Id == myProduct.Id)
            {
                DataSource.productList[i] = myProduct;
                return;
            }
        }
        throw new NoFoundItemExceptions("no product found to update with this ID");

    }
    #endregion
}
