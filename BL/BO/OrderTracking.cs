using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BO;
public class OrderTracking
{
    #region properties
    public int Id { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public List<DetailOrderStatus> DetailOrderStatuses { get; set; }
    #endregion

    #region ToString
    /// <returns> the props of this object</returns>
    public override string ToString()
    {
        string DetailOrderStatusesString = "";
        foreach(DetailOrderStatus detailOrderStatus in DetailOrderStatuses)
        {
            DetailOrderStatusesString += detailOrderStatus;
        }
        return $@" order id: {Id}
        order status: {OrderStatus},
        detail Order Status: {DetailOrderStatusesString}";
    }
    
    #endregion
}
