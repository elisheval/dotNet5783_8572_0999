

namespace DO;

public struct OrderItem
{
    #region properties
    public int Id { get; set; } = -1;
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    #endregion

    #region OrderItem constructor
    public OrderItem(int productId, int orderId, double price, int amount)
    {
        ProductId= productId;
        OrderId= orderId;
        Price= price;
        Amount= amount;
    }
    #endregion

    /// <returns> the props of this object</returns>
    public override string ToString() => $@"
        orderItem:{Id},
        ProductId:{ProductId}, 
        OrderId - {OrderId}
    	Price: {Price}
        Amount:{Amount}
         ";
}
