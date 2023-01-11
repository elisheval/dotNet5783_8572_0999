using System.Windows;
using PL.ProductWindows;
using PL.Order;

namespace PL.Manager;
/// <summary>
/// Interaction logic for managerWindow.xaml
/// </summary>
public partial class ManagerWindow : Window
{
    #region constractor
    public ManagerWindow()
    {
        InitializeComponent();
    }
    #endregion

    #region navigation functions 
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
    #endregion
}
