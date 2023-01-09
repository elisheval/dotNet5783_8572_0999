using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Order;

/// <summary>
/// Interaction logic for OrderWindow.xaml
/// </summary>
public partial class OrderWindow : Window
{
    BlApi.IBl? bl = BlApi.Factory.Get();
    bool manager;
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
    
    public OrderWindow(int orderId,bool visibleShip,bool visibleDelivery, bool manager=false)
    {
        visiblityShip= visibleShip;
        visiblityDelivery = visibleDelivery;
        if (bl != null) order=bl.Order.GetOrderById(orderId);
        this.manager = manager;
        InitializeComponent();
    }
    
    private void ShowAllOrderItems(object sender, RoutedEventArgs e)
    {
        this.Hide();
        new OrderItems(order.OrderItemList, manager, visiblityShip, order.Id).ShowDialog();
        if (bl != null) order = bl.Order.GetOrderById(order.Id);
        this.Show();
    }

    private void btnConfirmShip(object sender, RoutedEventArgs e)
    {
        if (bl != null)
        {
            bl.Order.OrderShippingUpdate(order.Id);
            visiblityShip = false;
            visiblityDelivery = true;
            order = bl.Order.GetOrderById(order.Id);
        }
    }
    
    private void btnConfirmDelivery(object sender, RoutedEventArgs e)
    {
        if (bl != null)
        {
            bl.Order.OrderDeliveryUpdate(order.Id);
            visiblityDelivery = false;
            order = bl.Order.GetOrderById(order.Id);
        }
    }
}
