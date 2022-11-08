

using DO;
using System.Globalization;

namespace Dal;

public struct DalOrderItem
{
    public int Add(OrderItem myOrderItem)
    {
        myOrderItem.Id = DataSource.Config.IdentifyOrderItem;
        DataSource.orderItemsArr[DataSource.Config.IndexOrderItem++] = myOrderItem;
        return myOrderItem.Id;
    }
    public OrderItem Get(int myId)
    {
        for (int i = 0; i < DataSource.Config.IndexOrderItem; i++)
        {
            if (DataSource.orderItemsArr[i].Id == myId)
                return DataSource.orderItemsArr[i];
        }
        throw new Exception("no orderItem found with this ID");
    }

    public OrderItem[] GetAll()
    {
        return DataSource.orderItemsArr;
    }

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

    public OrderItem GetByProductAndOrderIds(int myProductId, int myOrderId)
    {
        for(int i=0; i < DataSource.Config.IndexOrderItem; i++)
        {
            if (DataSource.orderItemsArr[i].OrderId==myOrderId&& DataSource.orderItemsArr[i].ProductId == myProductId)
                return DataSource.orderItemsArr[i];
        }
        throw new Exception("no found orderItem with this IDs");
    }
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
