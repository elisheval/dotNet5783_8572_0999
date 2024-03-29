﻿using static BO.Enums;

namespace BO;
public class OrderTracking
{
    #region properties
    public int Id { get; set; }
    public OrderStatus? OrderStatus { get; set; }
    public List<(DateTime?,string)?>? DetailOrderStatuses { get; set; }
    #endregion

    #region ToString
    /// <returns> the props of this object</returns>
    public override string ToString()
    {
        string DetailOrderStatusesString = "";
        if (DetailOrderStatuses != null)
        {
            foreach (var detailOrderStatus in DetailOrderStatuses)
            {
                DetailOrderStatusesString += detailOrderStatus?.Item2 + " : " + detailOrderStatus?.Item1 + "\n        ";
            }
        }
        return $@"      order id: {Id}
        order status: {OrderStatus},
        detail Order Status:
        {DetailOrderStatusesString}";
    }
    
    #endregion
}
