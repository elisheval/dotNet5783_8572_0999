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
using PL.Order;

namespace PL.Order;

/// <summary>
/// Interaction logic for OrderList.xaml
/// </summary>
public partial class OrderList : Window
{
    BlApi.IBl? bl = BlApi.Factory.Get();
    public IEnumerable<BO.OrderForList?> orderList
    {
        get { return (IEnumerable<BO.OrderForList?>)GetValue(orderListProperty); }
        set { SetValue(orderListProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty orderListProperty =
        DependencyProperty.Register("orderList", typeof(IEnumerable<BO.OrderForList?>), typeof(OrderList));
    public BO.OrderForList orderSelected
    {
        get { return (BO.OrderForList)GetValue(orderSelectedProperty); }
        set { SetValue(orderSelectedProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty orderSelectedProperty =
        DependencyProperty.Register("orderSelected", typeof(BO.OrderForList), typeof(OrderList));

    public OrderList()
    {
        orderSelected = new();
        orderList = bl.Order.GetAllOrders();
        InitializeComponent();
    }
    private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        MessageBox.Show(orderSelected.ToString());
        bool sent = false;
        bool supplied = false;
        if (orderSelected.OrderStatus == (BO.Enums.OrderStatus)0)
            sent = true;
        else if (orderSelected.OrderStatus == (BO.Enums.OrderStatus)1)
            supplied = true;
        new OrderWindow(orderSelected.Id,sent, supplied, true ).ShowDialog();//after the add window close updating the list
        if(bl!=null) orderList = bl.Order.GetAllOrders();
    }
}
