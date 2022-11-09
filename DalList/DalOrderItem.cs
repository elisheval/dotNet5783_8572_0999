

using DO;
using System.Globalization;

namespace Dal;

public class DalOrderItem
{
    /// <summary>
    /// The add method receives a new order object,
    /// updates it with an automatic object codeת
    /// and saves it in the next place in the array 
    /// </summary>
    /// <returns>Returns the id of the new order</returns>
    public int Add(OrderItem myOrderItem)
    {
        myOrderItem.Id = DataSource.Config.IdentifyOrderItem;
        DataSource.orderItemsArr[DataSource.Config.IndexOrderItem++] = myOrderItem;
        return myOrderItem.Id;
    }

    /// <summary>
    /// Get exist order's id and scan the array's order
    /// </summary>
    /// <returns>A copy of the object whose id is equal to the received id</returns>
    /// <exception cref="Exception">Throw exception if not exists</exception>
    public OrderItem Get(int myId)
    {
        for (int i = 0; i < DataSource.Config.IndexOrderItem; i++)
        {
            if (DataSource.orderItemsArr[i].Id == myId)
                return DataSource.orderItemsArr[i];
        }
        throw new Exception("no orderItem found with this ID");
    }

    /// <summary>
    /// copy the exist array for temp array
    /// </summary>
    /// <returns>the temp array</returns>
    public OrderItem[] GetAll()
    {
        OrderItem[] tmpOrderItemsArr = new OrderItem[DataSource.Config.IndexOrderItem];
        for (int i = 0; i < DataSource.Config.IndexOrderItem; i++)
            tmpOrderItemsArr[i] = DataSource.orderItemsArr[i];
        return tmpOrderItemsArr;
    }

    /// <summary>
    /// Searches for the object whose id was received, 
    /// deletes it from the array and updates the number of full places in the array
    /// </summary>
    /// <exception cref="Exception">Throw exception if not exists</exception>
    public void Delete(int myId)
    {
        for (int i = 0; i < DataSource.Config.IndexOrderItem; i++)
        {
            if (DataSource.orderItemsArr[i].Id == myId)
            {
                DataSource.orderItemsArr[i] = DataSource.orderItemsArr[--DataSource.Config.IndexOrderItem];
                return;
            }

        }
        throw new Exception("no orderItem found to delete with this ID");
    }

    /// <param name="myOrder">Gets an object whose id already exists in another object</param>
    /// <summary>
    /// Searches for the object in the array
    /// whose id is equal to the received id and
    /// changes the rest of its details to the details of the new object
    /// </summary>
    /// <exception cref="Exception">If the id does not exist yet</exception>
    public void Update(OrderItem myOrderItem)
    {
        for (int i = 0; i < DataSource.Config.IndexOrderItem; i++)
        {
            if (DataSource.orderItemsArr[i].Id == myOrderItem.Id)
            {
                DataSource.orderItemsArr[i] = myOrderItem;
                return;
            }
        }
        throw new Exception("no orderItem found to update with this ID");

    }


    /// <param name="myProductId">get id of existing product</param>
    /// <param name="myOrderId">get id of existing order</param>
    /// <summary>
    /// The method looks for order details by product code and order code
    /// </summary>
    /// <returns>The above order details</returns>
    /// <exception cref="Exception">if not exists</exception>
    public OrderItem GetByProductAndOrderIds(int myProductId, int myOrderId)
    {
        for(int i=0; i < DataSource.Config.IndexOrderItem; i++)
        {
            if (DataSource.orderItemsArr[i].OrderId==myOrderId&& DataSource.orderItemsArr[i].ProductId == myProductId)
                return DataSource.orderItemsArr[i];
        }
        throw new Exception("no found orderItem with this IDs");
    }



    /// <param name="myOrderId">get id of existing order</param>
    /// <summary>
    /// The method creates a new temp array
    /// and copies to it all the existing order items in the order items array
    /// </summary>
    /// <returns>the above array</returns>
    /// <exception cref="Exception">If there are no existing order items for this id</exception>
    public OrderItem[] getOrderItemsArrWithSpecificOrderId(int myOrderId)
    {
        OrderItem[] tmpOrderItemsArr = new OrderItem[DataSource.Config.IndexOrderItem - 1];
        int j = 0;
        for (int i = 0; i < DataSource.Config.IndexOrderItem; i++)
        {
            if(DataSource.orderItemsArr[i].OrderId == myOrderId)
            {
                tmpOrderItemsArr[j++] = DataSource.orderItemsArr[i];
            }
        }
        if (j == 0)
            throw new Exception("no found orderItems with this orderId");
         return tmpOrderItemsArr;
    }

}
