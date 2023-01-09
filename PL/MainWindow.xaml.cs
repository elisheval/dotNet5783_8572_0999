using System.Windows;
using PL.Manager;
using PL.Order;
using PL.Product;
namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    BlApi.IBl? bl = BlApi.Factory.Get();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void NavigateToManagerWindow(object sender, RoutedEventArgs e)
    {
        this.Hide();
        new ManagerWindow().ShowDialog();
        this.Show();
    }

    private void NavigateToCatalogWindow(object sender, RoutedEventArgs e)
    {
        new ProductCatalog().Show(); 
    }

    private void NavigateToOrderTrackingWindow(object sender, RoutedEventArgs e)
    {
        this.Hide();
        new OrderTracking().ShowDialog();
        this.Show();
    }
}
