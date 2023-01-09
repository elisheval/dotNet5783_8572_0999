using DO;
using System.Collections.Generic;
using DalApi;
using System.Linq;

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
         if(DataSource.productList.Where(product => product != null&& product?.Id == myProduct.Id).FirstOrDefault()!=null)
            throw new ItemAlresdyExsistException("product with this id already exist");
        DataSource.productList.Add(myProduct);
        return myProduct.Id;
    }
    #endregion

    #region GetAll
    /// <summary>
    /// copy the exist array for temp array
    /// </summary>
    /// <returns>the temp array</returns>
    public IEnumerable<Product?> GetAll(Predicate<Product?>? func=null)
    {
        if (func != null)
        {
            return DataSource.productList.Where(x => func(x));
        }
        return DataSource.productList;
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
        if (DataSource.productList.RemoveAll(x => x != null && x?.Id == myId) == 0) 
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
        int indexToUpdate = DataSource.productList.FindIndex(x => x?.Id == myProduct.Id);
        if (indexToUpdate >= 0) DataSource.productList[indexToUpdate] = myProduct;
        else throw new NoFoundItemExceptions("no product found to update with this ID");
    }
    #endregion

    #region GetByCondition
    /// <summary>
    /// 
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="NoFoundItemExceptions"></exception>
    public Product GetByCondition(Predicate<Product?> func)
    {
        Product? p = DataSource.productList.Where(x => func(x)).FirstOrDefault();
        if (p == null)
            throw (new NoFoundItemExceptions("no found p item with this condition"));
        else
            return (Product)p;
    }
    #endregion
}
