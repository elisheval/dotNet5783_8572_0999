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
using BlImplementation;
using BlApi;
namespace PL.ProductWindows;

/// <summary>
/// Interaction logic for ProductList.xaml
/// </summary>
/// 
public partial class ProductList : Window
{
    IBl bl = new Bl();

    public ProductList()
    {
        InitializeComponent();
        ProductListview.ItemsSource = bl.Product.GetAllProduct();
        foreach (var item in Enum.GetValues(typeof(BO.Enums.Category)))
            categorySelector.Items.Add(item);
        categorySelector.Items.Add("");


    }
    private void ShowProductsButton_Click(object sender, RoutedEventArgs e) => new ProductWindow().Show();
    private void categorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        
            if (categorySelector.SelectedItem.ToString() == "")
                ProductListview.ItemsSource = bl.Product.GetAllProduct();
            else
                ProductListview.ItemsSource = bl.Product.GetProductsByCategory((BO.Enums.Category)categorySelector.SelectedValue);
    
    }

    private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        BO.ProductForList pfl = (BO.ProductForList)ProductListview.SelectedValue;
        new ProductWindow(pfl.Id).Show();
    }

    
}
