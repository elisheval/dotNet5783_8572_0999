using System.Windows;
using PL.Manager;
using PL.Order;
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
        new ManagerWindow().Show();
        this.Close();
    }

    private void NavigateToOrderWindow(object sender, RoutedEventArgs e)
    {
        //new OrderWindow().Show();
        //this.Close();
    }

    private void NavigateToOrderTrackingWindow(object sender, RoutedEventArgs e)
    {
        new OrderTracking().Show();
        this.Close();
    }
}
