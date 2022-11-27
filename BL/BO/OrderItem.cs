
namespace BO;
public class OrderItem
{
    #region properties
    public int Id { get; set; }
    public int ProductId { get; set; } 
    public string ProductName { get; set; }
    public double Price { get; set; }
    public int AmountInCart { get; set; }
    public double TotalPriceForItem { get; set; }
    #endregion

    #region ToString
    /// <returns> the props of this object</returns>
    public override string ToString() =>
    $@" 
                order item ID: {Id}
                product id: {ProductId}
                product name: {ProductName}
    	        price: {Price}
                amount in cart: {AmountInCart}
    	        total price for item: {TotalPriceForItem}
    ";
    #endregion
}
