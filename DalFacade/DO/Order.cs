

namespace DO;

public struct Order
{
    public int ID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryDate { get; set; }

    public override string ToString() => $@"
        order ID={ID}: {CustomerName}, 
        email - {CustomerEmail}
    	CustomerAddress: {CustomerAddress}
    	OrderDate: {OrderDate}
    	ShipDate: {ShipDate}
    	DeliveryDate: {DeliveryDate}
         ";

}
