using DO;
using System.Collections.Generic;
using DalApi;
using System.Security.Cryptography;

namespace Dal;
internal class DalOrder : IOrder
{
    #region Add
    /// <summary>
    /// The add method receives a new order object,
    /// updates it with an automatic object codeת
    /// and saves it in the next place in the array 
    /// </summary>
    /// <returns>Returns the id of the new order</returns>
    public int Add(Order myOrder)
    {
        myOrder.ID = DataSource.Config._IdentifyOrder;
        DataSource.orderList.Add(myOrder);
        return myOrder.ID;
    }
    #endregion

    #region GetAll
    /// <summary>
    /// copy the exist array for temp array
    /// </summary>
    /// <returns>the temp array</returns>
    public IEnumerable<Order?> GetAll(Predicate<Order?>? func = null)
    {

        List<Order?> tmpOrderList = new();
        if (func != null)
        {
            tmpOrderList = DataSource.orderList.FindAll(x => func(x));
            return tmpOrderList;
        }
        tmpOrderList= DataSource.orderList;
        return tmpOrderList;
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
        int i=DataSource.orderList.RemoveAll(x=>x!=null&&x?.ID== myId);
        if(i==0)throw new NoFoundItemExceptions("no order found to delete with this ID");
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
    public void Update(Order myOrder)
    {
        DataSource.orderList.Where(x => x != null && x?.ID == myOrder.ID).ToList().ForEach(w => w =myOrder); return;
        throw new NoFoundItemExceptions("no order found to update with this ID");
    }
    #endregion

    #region GetByCondition
    /// <param name="func"> predicate</param>
    /// <summary>
    /// get filtering predicate and return the orderList filtering according to the method
    /// </summary>
    /// <returns>filtering orderList</returns>
    /// <exception cref="NoFoundItemExceptions"> if the </exception>
    public Order GetByCondition(Predicate<Order?> func)
    {
        Order? order1 = DataSource.orderList.Where(x => func(x)).FirstOrDefault();
        if (order1 == null)
            throw new NoFoundItemExceptions("no found order with this condition");
        return (Order)order1;
       
    }
    #endregion

}