using System.Windows;

namespace PL.Order;

/// <summary>
/// Interaction logic for OrderItem.xaml
/// </summary>
public partial class OrderItem : Window
{
    #region properties
    BlApi.IBl? bl = BlApi.Factory.Get();
    int orderId;
    #endregion

    #region Dependency Propertys
    public BO.OrderItem selectedOrderItem
    {
        get { return (BO.OrderItem)GetValue(OIProperty); }
        set { SetValue(OIProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty OIProperty =
        DependencyProperty.Register("selectedOrderItem", typeof(BO.OrderItem), typeof(OrderItem));
    #endregion

    #region constractor
    public OrderItem(BO.OrderItem selectedOrderItem, int orderId)
    {
        if (selectedOrderItem != null)
        {
            this.selectedOrderItem = selectedOrderItem;
            this.orderId = orderId;
            InitializeComponent();
        }
    }
    #endregion

    #region  confirmDelete
    /// <summary>
    /// delete order item from order
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void confirmDelete(object sender, RoutedEventArgs e)
    {
        if (bl != null)
        {
            bl.Order.UpdateOrder(orderId, selectedOrderItem.ProductId, 0);
            this.Hide();
        }
    }
    #endregion

    #region confirmUpdate
    /// <summary>
    /// updating amount of order item in order
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void confirmUpdate(object sender, RoutedEventArgs e)
    {
        if (bl != null)
        {
            bl.Order.UpdateOrder(orderId, selectedOrderItem.ProductId, selectedOrderItem.AmountInCart);
            this.Close();
        }
    }
    #endregion
}
