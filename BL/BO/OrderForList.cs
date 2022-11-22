using static BO.Enums;

namespace BO;
public class OrderForList
{
    #region properties
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public int  ItemsAmount { get; set; }
    public int TotalPrice { get; set; }
    #endregion

    #region ToString
    /// <returns> the props of this object</returns>
    public override string ToString() =>
    $@" order for list ID: {Id}
        customer name: {CustomerName}, 
        order status: {OrderStatus}
        items amount: {ItemsAmount}
        total price: {TotalPrice}";
    #endregion
}
