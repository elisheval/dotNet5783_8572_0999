using BO;

namespace BlApi;
public interface IOrder
{
    /// <summary>
    /// Get All Orders
    /// </summary>
    /// <returns>list of orderForList entity with all the details of the orders</returns>
    public IEnumerable<OrderForList?> GetAllOrders();
    /// <summary>
    /// Get Order By Id
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns>the details of the order with this id</returns>
    public Order GetOrderById(int orderId);
    /// <summary>
    /// Order Shipping Update
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns>the order details after updating the status</returns>
    public Order OrderShippingUpdate(int orderId);
    /// <summary>
    /// Order Delivery Update
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns>the order details after updating the status</returns>
    public Order OrderDeliveryUpdate(int orderId);
    /// <summary>
    /// Order tracking
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns>the order tracking details</returns>
    public OrderTracking Ordertracking(int orderId);
    /// bonus method
    /// <param name="orderId">get order of id</param>
    /// <param name="productId">get id of product to add, delete or change amount</param>
    /// <param name="amount">get amount</param>
    /// <summary>
    /// the manager can add, delete or change amount of product in confirm order.
    /// </summary>
    /// <exception cref="BO.InvalidValueException"></exception>
    /// <exception cref="BO.NoAccessToSentOrder"></exception>
    /// <exception cref="BO.NoFoundItemExceptions"></exception>
    public void UpdateOrder(int orderId, int productId, int amount);
    public int? GetNextOrderToHandle();
}
