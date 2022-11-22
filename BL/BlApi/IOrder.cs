
using BO;

namespace BlApi;
public interface IOrder
{
    public IEnumerable<OrderForList> GetAllOrders();
    public Order GetOrderById(int orderId);
    public Order OrderShippingUpdate(int orderId);//עדכון שההזמנה נשלחה
    public Order OrderDeliveryUpdate(int orderId);
    public OrderTracking Ordertracking(int orderId);
    public void UpdateOrder();//בונוס

}
