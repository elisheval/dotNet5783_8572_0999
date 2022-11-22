using DO;
using System.Globalization;
using System.Collections.Generic;
using DalApi;

namespace Dal;
internal class DalOrderItem:IOrderItem
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

    #region Get
    /// <summary>
    /// Get exist order's id and scan the array's order
    /// </summary>
    /// <returns>A copy of the object whose id is equal to the received id</returns>
    /// <exception cref="Exception">Throw exception if not exists</exception>
    public OrderItem Get(int myId)
    {
        for (int i = 0; i < DataSource.orderItemsList.Count; i++)
        {
            if (DataSource.orderItemsList[i].Id == myId)
                return DataSource.orderItemsList[i];
        }
        throw new NoFoundItemExceptions("no orderItem found with this ID");
    }
    #endregion

    #region GetAll
    /// <summary>
    /// copy the exist array for temp array
    /// </summary>
    /// <returns>the temp array</returns>
    public IEnumerable<OrderItem> GetAll()
    {
        List<OrderItem> tmpOrderItemsList=new List<OrderItem>();
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
        for (int i = 0; i < DataSource.orderItemsList.Count; i++)
        {
            if (DataSource.orderItemsList[i].Id == myId)
            {
                OrderItem tmp=DataSource.orderItemsList[i];
                DataSource.orderItemsList.Remove(tmp);
                return;
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
            if (DataSource.orderItemsList[i].Id == myOrderItem.Id)
            {
                DataSource.orderItemsList[i] = myOrderItem;
                return;
            }
        }
        throw new NoFoundItemExceptions("no orderItem found to update with this ID");

    }
    #endregion

    #region GetByProductAndOrderIds
    /// <param name="myProductId">get id of existing product</param>
    /// <param name="myOrderId">get id of existing order</param>
    /// <summary>
    /// The method looks for order details by product code and order code
    /// </summary>
    /// <returns>The above order details</returns>
    /// <exception cref="Exception">if not exists</exception>
    public OrderItem GetByProductAndOrderIds(int myProductId, int myOrderId)
    {
        for(int i=0; i < DataSource.orderItemsList.Count; i++)
        {
            if (DataSource.orderItemsList[i].OrderId==myOrderId&& DataSource.orderItemsList[i].ProductId == myProductId)
                return DataSource.orderItemsList[i];
        }
        throw new NoFoundItemExceptions("no found orderItem with this IDs");
    }
    #endregion

    #region getOrderItemsArrWithSpecificOrderId
    /// <param name="myOrderId">get id of existing order</param>
    /// <summary>
    /// The method creates a new temp array
    /// and copies to it all the existing order items in the order items array
    /// </summary>
    /// <returns>the above array</returns>
    /// <exception cref="Exception">If there are no existing order items for this id</exception>
    public IEnumerable<OrderItem> getOrderItemsArrWithSpecificOrderId(int myOrderId)
    {
       List<OrderItem> orderItemsList=new List<OrderItem>();

        for(int i=0;i < DataSource.orderItemsList.Count; i++)
        {
            if(DataSource.orderItemsList[i].OrderId == myOrderId)
            {
                orderItemsList.Add(DataSource.orderItemsList[i]);
            }
        }
         return orderItemsList;
    }
    #endregion
}
