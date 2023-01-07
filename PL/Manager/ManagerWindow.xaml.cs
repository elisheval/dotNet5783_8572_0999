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
using PL.ProductWindows;
using PL.Order;
namespace PL.Manager;

/// <summary>
/// Interaction logic for managerWindow.xaml
/// </summary>
public partial class ManagerWindow : Window
{
    public ManagerWindow()
    {
        InitializeComponent();
    }

    private void NavigateToProductListWindow(object sender, RoutedEventArgs e)
    {
        new ProductList().Show();
        this.Close();
    }
    private void NavigateToOrderListWindow(object sender, RoutedEventArgs e)
    {
        new OrderList().Show();
        this.Close();
    }
}
