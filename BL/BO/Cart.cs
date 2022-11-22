using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Cart
{//להדפיס את רשימת פרטי ההזמנה
    public string CustomerName { get; set; }    
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public List<OrderItem> OrderItemList { get; set; }
    public int TotalOrderPrice { get; set; }

    #region ToString
    public override string ToString() =>
    $@" Customer name: {CustomerName}, 
        customerEmail - {CustomerEmail}
    	CustomerAddress: {CustomerAddress}
    	Total Order Price: {TotalOrderPrice}";
        #endregion

}
