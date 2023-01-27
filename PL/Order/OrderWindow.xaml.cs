using BO;
using System.Windows;

namespace PL.Order;

/// <summary>
/// Interaction logic for OrderWindow.xaml
/// </summary>
public partial class OrderWindow : Window
{
    #region properties
    BlApi.IBl? bl = BlApi.Factory.Get();
    public bool manager { get; set; }
    #endregion

    #region Dependency Propertys
    public string message
    {
        get { return (string)GetValue(messageProperty); }
        set { SetValue(messageProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty messageProperty =
        DependencyProperty.Register("message", typeof(string), typeof(OrderWindow));

    public bool visiblityShip
    {
        get { return (bool)GetValue(visiblityShipProperty); }
        set { SetValue(visiblityShipProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty visiblityShipProperty =
        DependencyProperty.Register("visiblityShip", typeof(bool), typeof(OrderWindow));

    public bool visiblityDelivery
    {
        get { return (bool)GetValue(visiblityDeliveryProperty); }
        set { SetValue(visiblityDeliveryProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty visiblityDeliveryProperty =
        DependencyProperty.Register("visiblityDelivery", typeof(bool), typeof(OrderWindow));
    
    public BO.Order order
    {
        get { return (BO.Order)GetValue(orderProperty); }
        set { SetValue(orderProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty orderProperty =
        DependencyProperty.Register("order", typeof(BO.Order), typeof(OrderWindow));
    #endregion

    #region constructor
    public OrderWindow(int orderId,bool visibleShip,bool visibleDelivery, bool manager=false)
    {
        try
        {
            message = "";
            visiblityShip = visibleShip;
            visiblityDelivery = visibleDelivery;
            if (bl != null) order = bl.Order.GetOrderById(orderId);
            this.manager = manager;
            InitializeComponent();
        }
        catch(InvalidValueException ex){};
    }
    #endregion

    #region ShowAllOrderItems
    private void ShowAllOrderItems(object sender, RoutedEventArgs e)
    {
        if (bl != null) order = bl.Order.GetOrderById(order.Id);
        this.Hide();
        new OrderItems(order.OrderItemList, manager, visiblityShip, order.Id).ShowDialog();
        this.Show();
    }
    #endregion

    #region btnConfirmShip
    /// <summary>
    /// updating the order status to br shiped
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnConfirmShip(object sender, RoutedEventArgs e)
    {
        try
        {
            if (bl != null)
            {
                bl.Order.OrderShippingUpdate(order.Id);
                visiblityShip = false;
                visiblityDelivery = true;
                order = bl.Order.GetOrderById(order.Id);
            }
        }
        catch (BO.InvalidDateChange ex) { message = ex.Message; }
        catch (BO.NoFoundItemExceptions ex) { message = ex.Message; }
    }
    #endregion

    #region btnConfirmDelivery
    /// <summary>
    /// updating the order status to be delivery
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnConfirmDelivery(object sender, RoutedEventArgs e)
    {
        try
        {
            if (bl != null)
            {
                bl.Order.OrderDeliveryUpdate(order.Id);
                visiblityDelivery = false;
                order = bl.Order.GetOrderById(order.Id);
            }
        }
        catch (BO.InvalidDateChange ex) { message = ex.Message; }
        catch (BO.NoFoundItemExceptions ex) { message = ex.Message; }
    }
    #endregion

    #region NavigateToOrderList

    private void NavigateToOrderList(object sender, RoutedEventArgs e)
    {
        new OrderList().Show();
        this.Close();
    }
    #endregion
}
