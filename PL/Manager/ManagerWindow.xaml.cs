using System.Windows;
using PL.ProductWindows;
using PL.Order;

namespace PL.Manager;
/// <summary>
/// Interaction logic for managerWindow.xaml
/// </summary>
public partial class ManagerWindow : Window
{
    #region constructor
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

    private void NavigateToMainWindow(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        this.Close();
    }
}
