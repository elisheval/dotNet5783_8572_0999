using static BO.Enums;

namespace BO;
public class Order
{
    #region properties
    public int Id { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    public OrderStatus? OrderStatus { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public List<OrderItem>? OrderItemList { get; set; }
    public double TotalOrderPrice{ get;set;}
    #endregion

    #region ToString
    /// <returns> the props of this object</returns>

    public override string ToString() {
        string item = "";
        foreach (OrderItem orderItem in OrderItemList) {
            item += orderItem;
        }
        return
    $@" order ID: {Id}
        customer name: {CustomerName}, 
        customer email: {CustomerEmail}
    	customerAddress: {CustomerAddress}
        order status: {OrderStatus}
    	orderDate: {OrderDate}
    	shipDate: {ShipDate}
        all order items:{item}
    	deliveryDate: {DeliveryDate}
        total order price: {TotalOrderPrice}";
    }
    #endregion
}
