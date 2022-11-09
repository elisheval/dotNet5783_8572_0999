

namespace DO;

public struct Order
{
    public int ID { get; set; } = -1;
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.MinValue;
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryDate { get; set; }

    Order(string customerName, string customerEmail, string customerAddress, DateTime shipDate, DateTime deliveryDate, DateTime orderDate)// = DateTime.MinValue)
    {
        //DateTime.MinValue מה זה timespan
        CustomerName = customerName;
        CustomerEmail = customerEmail;
        CustomerAddress = customerAddress;     
        OrderDate = orderDate;
        ShipDate = shipDate;
        DeliveryDate = deliveryDate;
 
    }
    public override string ToString() => $@"
        order ID={ID}: {CustomerName}, 
        email - {CustomerEmail}
    	CustomerAddress: {CustomerAddress}
    	OrderDate: {OrderDate}
    	ShipDate: {ShipDate}
    	DeliveryDate: {DeliveryDate}
         ";

}
