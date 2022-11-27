using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Cart
{
    #region properties
    public string CustomerName { get; set; }    
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public List<OrderItem> OrderItemList { get; set; }
    public double TotalOrderPrice { get; set; }
    #endregion

    #region ToString
    public override string ToString()
    {
        string orderItems = "";
        foreach (OrderItem item in OrderItemList)
        {
            orderItems += item;
        }
        if (CustomerName == null) CustomerName = "NOT UPDATE YET";
        if (CustomerEmail == null) CustomerEmail = "NOT UPDATE YET";
        if (CustomerAddress== null) CustomerAddress = "NOT UPDATE YET";
        return (
        $@"customer name: {CustomerName}, 
        customer email: {CustomerEmail},
    	customer address: {CustomerAddress},
        order items list:
       {orderItems},
    	total order price: {TotalOrderPrice}");
    }
    #endregion

}
