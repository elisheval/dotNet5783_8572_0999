using System.Windows;

namespace PL.Order;

/// <summary>
/// Interaction logic for OrderTracking.xaml
/// </summary>
public partial class OrderTracking : Window
{
    #region properties
    BlApi.IBl? bl = BlApi.Factory.Get();
    public int orderId { get; set; } = 0;
    #endregion

    #region Dependency Propertys
    public string message
    {
        get { return (string)GetValue(messageProperty); }
        set { SetValue(messageProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty messageProperty =
        DependencyProperty.Register("message", typeof(string), typeof(OrderTracking));

    public BO.OrderTracking orderTracking
    {
        get { return (BO.OrderTracking)GetValue(orderTrackingProperty); }
        set { SetValue(orderTrackingProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty orderTrackingProperty =
        DependencyProperty.Register("orderTracking", typeof(BO.OrderTracking), typeof(OrderTracking));
    #endregion

    #region constractor
    public OrderTracking()
    {
        message= "";
        InitializeComponent();
    }
    #endregion

    #region BtnOrderTracking
    /// <summary>
    /// display the list of order tracking :date and status
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BtnOrderTracking(object sender, RoutedEventArgs e)
    {
        if (orderId == 0)
            message = "enter order id";
        try
        {
            if (bl != null) orderTracking = bl.Order.Ordertracking(orderId);
        }
        catch (BO.InvalidValueException ex)
        {
            message = ex.Message;
        }
        catch(BO.NoFoundItemExceptions ex)
        {
            message = ex.Message;
        }
    }
    #endregion

    #region showAllDetailsOfOrder
    /// <summary>
    /// navigate to window that shows all the order details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void showAllDetailsOfOrder(object sender, RoutedEventArgs e)
    {
        try
        {
            if (bl != null)bl.Order.GetOrderById(orderId);
            this.Hide();
            new OrderWindow(orderId, false, false).ShowDialog();
            this.Show();
        }
        catch (BO.NoFoundItemExceptions ex)
        {
            message = ex.Message;

        }
        catch(BO.InvalidValueException ex)
        {
            message = ex.Message;

        }
    }
    #endregion

    private void backMainWindow(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        this.Close();
    }
}
