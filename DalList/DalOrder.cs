using DO;

namespace Dal;

//לברר אם מחלקה או מבנה
public struct DalOrder
{
    public int Add(Order myOrder)
    {
        myOrder.ID = DataSource.Config.IdentifyOrder;
        DataSource.orderArr[DataSource.Config.IndexOrder++] = myOrder;
        return myOrder.ID;
    }
    public Order Get(int myId)
    {
        for (int i = 0; i < DataSource.Config.IndexOrder; i++)
        {
            if (DataSource.orderArr[i].ID == myId)
                return DataSource.orderArr[i];
        }
        throw new Exception("no order found with this ID");

    }

    public Order[] GetAll()
    {
        return DataSource.orderArr;
    }

    public void Delete(int myId)
    {
        for( int i = 0; i < DataSource.Config.IndexOrder; i++)
        {
            if (DataSource.orderArr[i].ID == myId)
            {
                DataSource.orderArr[i] = DataSource.orderArr[--DataSource.Config.IndexOrder];
                return;
            }

        }
        throw new Exception("no order found to delete with this ID");

    }
    public void Update(Product myProduct)
    {
        for (int i = 0; i < DataSource.Config.IndexProduct; i++)
        {
            if (DataSource.productArr[i].Id == myProduct.Id)
            {
                DataSource.productArr[i] = myProduct;
                return;
            }
        }
        throw  new Exception("no order found to update with this ID");

    }
}
