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
    public bool visiblity
    {
        get { return (bool)GetValue(visiblityProperty); }
        set { SetValue(visiblityProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty visiblityProperty =
        DependencyProperty.Register("visiblity", typeof(bool), typeof(OrderWindow));

    public BO.Order order
    {
        get { return (BO.Order)GetValue(orderProperty); }
        set { SetValue(orderProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty orderProperty =
        DependencyProperty.Register("order", typeof(BO.Order), typeof(OrderWindow));

    public OrderWindow(int orderId,bool visible)
    {
        visiblity= visible;

        if(bl!=null)order=bl.Order.GetOrderById(orderId);
        InitializeComponent();
    }

    private void ShowAllOrderItems(object sender, RoutedEventArgs e)
    {
        new OrderItems(order.OrderItemList).Show();
    }
}
