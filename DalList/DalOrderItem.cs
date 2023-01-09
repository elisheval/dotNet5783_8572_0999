using DO;
using System.Globalization;
using System.Collections.Generic;
using DalApi;

namespace Dal;
internal class DalOrderItem : IOrderItem
{
    #region Add
    /// <summary>
    /// The add method receives a new order object,
    /// updates it with an automatic object codeת
    /// and saves it in the next place in the array 
    /// </summary>
    /// <returns>Returns the id of the new order</returns>
    public int Add(OrderItem myOrderItem)
    {
        myOrderItem.Id = DataSource.Config._IdentifyOrderItem;
        DataSource.orderItemsList.Add(myOrderItem);
        return myOrderItem.Id;
    }
    #endregion

    #region GetAll
    /// <summary>
    /// copy the exist array for temp array
    /// </summary>
    /// <returns>the temp array</returns>
    public IEnumerable<OrderItem?> GetAll(Predicate<OrderItem?>? func = null)
    {
        if (func != null)
        {
            return DataSource.orderItemsList.Where(x => func(x)).ToList();
        }
        return DataSource.orderItemsList;
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
        if (DataSource.orderItemsList.RemoveAll(x => x != null && x?.Id == myId) == 0) 
            throw new NoFoundItemExceptions("no orderItem found to delete with this ID");
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
    public void Update(OrderItem myOrderItem)
    {
        int indexToUpdate = DataSource.orderItemsList.FindIndex(x => x?.Id == myOrderItem.Id);
        if (indexToUpdate >= 0) DataSource.orderItemsList[indexToUpdate] = myOrderItem;
        else throw new NoFoundItemExceptions("no orderItem found to update with this ID");

    }
    #endregion

    #region GetByCondition
    /// <summary>
    /// 
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="NoFoundItemExceptions"></exception>
    public OrderItem GetByCondition(Predicate<OrderItem?> func)
    {
        OrderItem? oi= DataSource.orderItemsList.Where(x => func(x)).FirstOrDefault();
        if(oi==null)
            throw (new NoFoundItemExceptions("no found order item with this condition"));
        else
            return (OrderItem)oi;
    }
    #endregion
}
