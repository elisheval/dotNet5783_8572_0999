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
/// Interaction logic for OrderItems.xaml
/// </summary>
public partial class OrderItems : Window
{
    bool manager;

    public BO.OrderItem selectedOrderItem
    {
        get { return (BO.OrderItem)GetValue(orderItemProperty); }
        set { SetValue(orderItemProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty orderItemProperty =
        DependencyProperty.Register("selectedOrderItem", typeof(BO.OrderItem), typeof(OrderItems));

    public IEnumerable<BO.OrderItem?> orderItemsList
    {
        get { return (IEnumerable<BO.OrderItem?>)GetValue(orderItemsListProperty); }
        set { SetValue(orderItemsListProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty orderItemsListProperty =
        DependencyProperty.Register("orderItemsList", typeof(IEnumerable<BO.OrderItem?>), typeof(OrderItems));

    public OrderItems(List<BO.OrderItem>? orderItems, bool manager)
    {
        if (orderItems != null)
            orderItemsList = orderItems;
            this.manager = manager;
        InitializeComponent();
    }

    private void updateOrderItem(object sender, MouseButtonEventArgs e)
    {
        MessageBox.Show(manager.ToString());
        if(manager)
            new OrderItem(selectedOrderItem).ShowDialog();
    }
}
