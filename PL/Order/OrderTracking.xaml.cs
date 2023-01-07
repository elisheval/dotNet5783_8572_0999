using PL.ProductWindows;
using System;
using System.Collections.Generic;
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
/// Interaction logic for OrderTracking.xaml
/// </summary>
public partial class OrderTracking : Window
{
    BlApi.IBl? bl = BlApi.Factory.Get();
    public BO.OrderTracking orderTracking
    {
        get { return (BO.OrderTracking)GetValue(orderTrackingProperty); }
        set { SetValue(orderTrackingProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty orderTrackingProperty =
        DependencyProperty.Register("orderTracking", typeof(BO.OrderTracking), typeof(OrderTracking));

    public int orderId { get; set; }=0;
    public OrderTracking()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (bl != null) orderTracking = bl.Order.Ordertracking(orderId);
    }

    private void showAllDetailsOfOrder(object sender, RoutedEventArgs e)
    {
        new OrderWindow(orderId,true).Show();
    }
}
