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
using System.ComponentModel;

namespace PL.ProductWindows;
/// <summary>
/// Interaction logic for ProductList.xaml
/// </summary>
/// 
public partial class ProductList : Window
{
    IBl bl = new Bl();
    
    /// <summary>
    /// constructor
    /// </summary>
    public ProductList()
    {
        InitializeComponent();
        ProductListview.ItemsSource = bl.Product.GetAllProduct();//get all the products
        foreach (var item in Enum.GetValues(typeof(BO.Enums.Category)))//show it in rows
            categorySelector.Items.Add(item);
        categorySelector.Items.Add("");
    }

    /// <summary>
    /// open the add window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e">button</param>
    private void ShowProductsButton_Click(object sender, RoutedEventArgs e)
    {
        new ProductWindow().ShowDialog();
        ProductListview.ItemsSource = bl.Product.GetAllProduct();//after the add window close updating the list
    }
    
    /// <summary>
    /// select the list by category
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void categorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

        if (categorySelector.SelectedItem.ToString() == "")
            ProductListview.ItemsSource = bl.Product.GetAllProduct();
        else
            ProductListview.ItemsSource = bl.Product.GetProductsByCategory((BO.Enums.Category)categorySelector.SelectedValue);

    }
    
    /// <summary>
    /// open the update window with the props of the tapped row
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        BO.ProductForList pfl = (BO.ProductForList)ProductListview.SelectedValue;
        new ProductWindow(pfl.Id).ShowDialog();//after the add window close updating the list
        ProductListview.ItemsSource = bl.Product.GetAllProduct();
    }

}
