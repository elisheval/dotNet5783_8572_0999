using DO;
namespace Dal;

public class DalOrder
{

    /// <summary>
    /// The add method receives a new order object,
    /// updates it with an automatic object codeת
    /// and saves it in the next place in the array 
    /// </summary>
    /// <returns>Returns the id of the new order</returns>
    public int Add(Order myOrder)
    {
        myOrder.ID = DataSource.Config.IdentifyOrder;
        DataSource.orderArr[DataSource.Config.IndexOrder++] = myOrder;
        return myOrder.ID;
    }

    /// <summary>
    /// Get exist order's id and scan the array's order
    /// </summary>
    /// <returns>A copy of the object whose id is equal to the received id</returns>
    /// <exception cref="Exception">Throw exception if not exists</exception>
    public Order Get(int myId)
    {
        for (int i = 0; i < DataSource.Config.IndexOrder; i++)
        {
            if (DataSource.orderArr[i].ID == myId)
                return DataSource.orderArr[i];
        }
        throw new Exception("no order found with this ID");

    }

    /// <summary>
    /// copy the exist array for temp array
    /// </summary>
    /// <returns>the temp array</returns>
    public Order[] GetAll()
    {
        Order[] tmpOrderArr = new Order[DataSource.Config.IndexOrder];
        for (int i = 0; i < DataSource.Config.IndexOrder; i++)
            tmpOrderArr[i] = DataSource.orderArr[i];
        return tmpOrderArr;
    }

    /// <summary>
    /// Searches for the object whose id was received,
    /// deletes it from the array and updates the number of full places in the array
    /// </summary>
    /// <exception cref="Exception">Throw exception if not exists</exception>
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
    /// <param name="myOrder">Gets an object whose id already exists in another object</param>
    /// <summary>
    /// Searches for the object in the array
    /// whose id is equal to the received id and
    /// changes the rest of its details to the details of the new object
    /// </summary>
    /// <exception cref="Exception">If the id does not exist yet</exception>
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
