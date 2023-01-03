using PL.ProductWindows;
using System.Windows;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    BlApi.IBl? bl = BlApi.Factory.Get();

    #region ShowProductsButton_Click
    private void ShowProductsButton_Click(object sender, RoutedEventArgs e)//button to show all products
    {
        new ProductList().Show();
        this.Close();
    }
    public MainWindow()
    {
        InitializeComponent();
    }
    #endregion
}
