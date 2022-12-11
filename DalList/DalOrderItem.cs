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
        List<OrderItem?> tmpOrderItemsList = new();
        if (func != null)
        {
            tmpOrderItemsList = DataSource.orderItemsList.FindAll(x => func(x));
            return tmpOrderItemsList;
        }
        for (int i = 0; i < DataSource.orderItemsList.Count; i++)
            tmpOrderItemsList.Add(DataSource.orderItemsList[i]);
        return tmpOrderItemsList;
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
        foreach (var item in DataSource.orderItemsList)
        {
            if (item != null)
            {
                if (item.Value.Id == myId)
                {
                    DataSource.orderItemsList.Remove(item);
                    return;
                }
            }
        }
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
        for (int i = 0; i < DataSource.orderItemsList.Count; i++)
        {
            if (DataSource.orderItemsList[i] != null)
                if (DataSource.orderItemsList[i].Value.Id == myOrderItem.Id)
                {
                    DataSource.orderItemsList[i] = myOrderItem;
                    return;
                }
        }
        throw new NoFoundItemExceptions("no orderItem found to update with this ID");

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
        OrderItem? orderItem1 = DataSource.orderItemsList.Find(x => func(x));
        if (orderItem1 == null)
            throw new NoFoundItemExceptions("no found order item with this condition");
        return (OrderItem)orderItem1;

    }
    #endregion
}
