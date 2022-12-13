using BlApi;
using BlImplementation;
using BO;
using System;
using System.Collections.Generic;
//using System.Diagnostics.Metrics;
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
using System.Xaml;
using System.Text.RegularExpressions;

namespace PL.ProductWindows;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    IBl bl = new Bl();

    /// <summary>
    /// add new product or update existing product
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        BO.Product p = new BO.Product()//create bl product
        {
            Id = int.Parse(ProductId.Text),
            Price = double.Parse(ProductPrice.Text),
            InStock = int.Parse(ProductInStock.Text),
            Name = ProductName.Text,
            Category = (BO.Enums.Category?)CategorySelector.SelectedValue
        };
        try
        {
            if (btnAddOrUpdate.Content.ToString() == "update")//update
                bl.Product.UpdateProduct(p);
            else//add
                bl.Product.AddProduct(p);
            MessageBox.Show("succesfully");
            Close();//close the window
        }
        catch (InvalidValueException ex)
        {
            int place = 0;
            if (ex.Message == "invalid id")
                place = 122;
            if (ex.Message == "invalid price")
                place = 271;
            if (ex.Message == "invalid amount in stock")
                place = 312;
            Label invalidValue = new Label()//create label under the invalid textBox
            {
                Name = "invalidValue",
                Margin = new Thickness(80, place, 0, 0),
                Content = ex.Message,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Foreground = new SolidColorBrush(Colors.Red),
            };
            Grid.SetRow(invalidValue, 1);
            MainGrid.Children.Add(invalidValue);
        }
        catch (ItemAlresdyExsistException ex)
        {
            StackPanel DeptStackPanel = new StackPanel();
            Label lblAlreadyExists = new Label()
            {
                Name = "lblAlreadyExists",
                Margin = new Thickness(0, 36, 0, 0),
                Content = ex.Message,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Foreground = new SolidColorBrush(Colors.Red),
            };
            Grid.SetRow(lblAlreadyExists, 1);
            MainGrid.Children.Add(lblAlreadyExists);
        };
    }
    /// <summary>
    /// 
    /// </summary>
    public ProductWindow()
    {
        InitializeComponent();
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="productId"></param>
    public ProductWindow(int productId)
    {

        InitializeComponent();
        BO.Product p = bl.Product.GetProductById(productId);
        btnAddOrUpdate.Content = "update";
        ProductId.Text = p.Id.ToString();
        ProductId.IsReadOnly = true;
        ProductPrice.Text = p.Price.ToString();
        ProductInStock.Text = p.InStock.ToString();
        ProductName.Text = p.Name;
        CategorySelector.SelectedItem = p.Category;
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProductId_TextChanged(object sender, TextChangedEventArgs e)
    {
        var child1 = MainGrid.Children.OfType<Control>().Where(x => x.Name == "invalidValue").FirstOrDefault();
        if (child1 != null)
            MainGrid.Children.Remove(child1);
        var child2 = MainGrid.Children.OfType<Control>().Where(x => x.Name == "lblAlreadyExists").FirstOrDefault();
        if (child2 != null)
            MainGrid.Children.Remove(child2);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProductPrice_TextChanged(object sender, TextChangedEventArgs e)
    {
        var child = MainGrid.Children.OfType<Control>().Where(x => x.Name == "invalidValue").FirstOrDefault();
        if (child != null)
            MainGrid.Children.Remove(child);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProductInStock_TextChanged(object sender, TextChangedEventArgs e)
    {
        var child = MainGrid.Children.OfType<Control>().Where(x => x.Name == "invalidValue").FirstOrDefault();
        if (child != null)
            MainGrid.Children.Remove(child);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }
}
