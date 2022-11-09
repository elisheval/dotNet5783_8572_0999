using DO;
namespace Dal;

public class DalOrder
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
        Order[] tmpOrderArr = new Order[DataSource.Config.IndexOrder];
        for (int i = 0; i < DataSource.Config.IndexOrder; i++)
            tmpOrderArr[i] = DataSource.orderArr[i];
        return tmpOrderArr;
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
    public void Update(Order myOrder)
    {
        for (int i = 0; i < DataSource.Config.IndexOrder; i++)
        {
            if (DataSource.orderArr[i].ID == myOrder.ID)
            {
                Console.WriteLine("rr");
                DataSource.orderArr[i] = myOrder;
                return;
            }
        }
        throw  new Exception("no order found to update with this ID");

    }
}
