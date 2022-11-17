using System.Collections.Generic;

namespace DO;

public struct Order
{
    #region properties
    public int ID { get; set; } = -1;
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    #endregion

    #region  order constructor
    public Order(string customerName, string customerEmail, string customerAddress, DateTime orderDate, DateTime shipDate, DateTime deliveryDate)
    {  
        CustomerName = customerName;
        CustomerEmail = customerEmail;
        CustomerAddress = customerAddress;
        OrderDate = orderDate;
        ShipDate = shipDate;
        DeliveryDate = deliveryDate;
    }

    #endregion

    #region ToString
    /// <returns> the props of this object</returns>

    public override string ToString() =>
    $@" order ID={ID}: {CustomerName}, 
        email - {CustomerEmail}
    	CustomerAddress: {CustomerAddress}
    	OrderDate: {OrderDate}
    	ShipDate: {ShipDate}
    	DeliveryDate: {DeliveryDate}";
    #endregion
}
