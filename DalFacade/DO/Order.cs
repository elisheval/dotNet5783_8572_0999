﻿
namespace DO;

public struct Order
{
    #region properties
    public int ID { get; set; } = -1;
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public DateTime OrderDate { get; set; } =DateTime.MinValue;
    public DateTime ShipDate { get; set; }=DateTime.MinValue;
    public DateTime DeliveryDate { get; set; }= DateTime.MinValue;
    #endregion

    #region  order constructor
    public Order(string customerName, string customerEmail, string customerAddress, DateTime orderDate)
    {  //DateTime.MinValue מה זה timespan
        CustomerName = customerName;
        CustomerEmail = customerEmail;
        CustomerAddress = customerAddress;
        OrderDate = orderDate;
        //ShipDate = shipDate;
        //DeliveryDate = deliveryDate;
    }
    #endregion
    
    
    /// <returns> the props of this object</returns>
    public override string ToString() => $@"
        order ID={ID}: {CustomerName}, 
        email - {CustomerEmail}
    	CustomerAddress: {CustomerAddress}
    	OrderDate: {OrderDate}
    	ShipDate: {ShipDate}
    	DeliveryDate: {DeliveryDate}
         ";

}
