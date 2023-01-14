
using System.ComponentModel;

namespace BO;
public class OrderItem: INotifyPropertyChanged
{
    #region properties
    private int id;
    public int Id
    {
        get { return id; }
        set
        {
            id = value;
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ID"));
        }
    }
    private int productId;
    public int ProductId
    {
        get { return productId; }
        set
        {
            productId = value;
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ProductId"));
        }
    }
    private string? productName;
    public string? ProductName
    {
        get { return productName; }
        set
        {
            productName = value;
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ProductName"));
        }
    }
    private double price;
    public double Price
    {
        get { return price; }
        set
        {
            price = value;
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Price"));
        }
    }
    private int amountInCart;
    public int AmountInCart
    {
        get { return amountInCart; }
        set
        {
            amountInCart = value;
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("AmountInCart"));
        }
    }
    private double totalPriceForItem;
    public double TotalPriceForItem
    {
        get { return totalPriceForItem; }
        set
        {
            totalPriceForItem = value;
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("TotalPriceForItem"));
        }
    }
    public event PropertyChangedEventHandler? PropertyChanged;

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
