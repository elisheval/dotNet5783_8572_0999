using static BO.Enums;

namespace BO;
public class OrderTracking
{
    #region properties
    public int Id { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public List<(DateTime,string)> DetailOrderStatuses { get; set; }
    #endregion

    #region ToString
    /// <returns> the props of this object</returns>
    public override string ToString()
    {
        string DetailOrderStatusesString = "";
        foreach(var detailOrderStatus in DetailOrderStatuses)
        {
            DetailOrderStatusesString+=detailOrderStatus.Item1+" : "+ detailOrderStatus.Item2;
        }
        return $@" order id: {Id}
        order status: {OrderStatus},
        detail Order Status: {DetailOrderStatusesString}";
    }
    
    #endregion
}
