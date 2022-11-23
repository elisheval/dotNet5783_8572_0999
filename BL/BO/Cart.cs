using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Cart
{
    public string CustomerName { get; set; }    
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public List<OrderItem> OrderItemList { get; set; }
    public double TotalOrderPrice { get; set; }

    #region ToString
    public override string ToString()
    {
        string orderItems = "";
        foreach (OrderItem item in OrderItemList)
        {
            orderItems += item;
        }
        return 
        $@" Customer name: {CustomerName}, 
        customerEmail - {CustomerEmail},
    	CustomerAddress: {CustomerAddress},
        order items list: {orderItems},
    	Total Order Price: {TotalOrderPrice}";
        #endregion
    }
}
