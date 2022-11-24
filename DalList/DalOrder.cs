using DO;
using System.Collections.Generic;
using DalApi;

namespace Dal;
internal class DalOrder:IOrder
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

    #region Get
    /// <summary>
    /// Get exist order's id and scan the array's order
    /// </summary>
    /// <returns>A copy of the object whose id is equal to the received id</returns>
    /// <exception cref="Exception">Throw exception if not exists</exception>
    public Order Get(int myId)
    {
        for (int i = 0; i < DataSource.orderList.Count; i++)
        {
            if (DataSource.orderList[i].ID == myId)
                return DataSource.orderList[i];
        }
        throw new NoFoundItemExceptions("no order found with this ID");

    }
    #endregion

    #region GetAll
    /// <summary>
    /// copy the exist array for temp array
    /// </summary>
    /// <returns>the temp array</returns>
    public IEnumerable<Order> GetAll()
    {
        List<Order> tmpOrderList = new List<Order>();
        for (int i = 0; i < DataSource.orderList.Count; i++)
            tmpOrderList.Add(DataSource.orderList[i]);
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
        for( int i = 0; i < DataSource.orderList.Count; i++)
        {
            if (DataSource.orderList[i].ID == myId)
            {
                Order tmpOrder = DataSource.orderList[i];
                DataSource.orderList.Remove(tmpOrder) ;
                return;
            }

        }
        throw new NoFoundItemExceptions("no order found to delete with this ID");

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
        for (int i = 0; i < DataSource.orderList.Count; i++)
        {
            if (DataSource.orderList[i].ID == myOrder.ID)
            {
                DataSource.orderList[i] = myOrder;
                return;
            }
        }
        throw  new NoFoundItemExceptions("no order found to update with this ID");

    }
    #endregion
}