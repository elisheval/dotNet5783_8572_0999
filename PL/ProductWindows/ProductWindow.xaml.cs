using BlApi;
using BlImplementation;
using BO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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

namespace PL.ProductWindows;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    IBl bl = new Bl();
    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        BO.Product p = new BO.Product()
        {
            Id = int.Parse(ProductId.Text),
            Price = double.Parse(ProductPrice.Text),
            InStock = int.Parse(ProductInStock.Text),
            Name = ProductName.Text,
            Category = (BO.Enums.Category?)CategorySelector.SelectedValue
        };
        try
        {
            if (btnAddOrUpdate.Content.ToString() == "update")
                bl.Product.UpdateProduct(p);
            else bl.Product.AddProduct(p);
            MessageBox.Show("succesfully");
            Close();
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
            Label lblAlreadyExists = new Label()
            {
                Margin = new Thickness(80, place, 0, 0),
                Content = ex.Message,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Foreground = new SolidColorBrush(Colors.Red),
            };//        <Label Content="Label" HorizontalAlignment="Left" Margin="400,130,0,0" Grid.Row="1" VerticalAlignment="Top" Width="96"/>
            Grid.SetRow(lblAlreadyExists, 1);
            MainGrid.Children.Add(lblAlreadyExists);
        }

        catch (ItemAlresdyExsistException ex)
        {
            StackPanel DeptStackPanel = new StackPanel();
            Label lblAlreadyExists = new Label()
            {
                Margin = new Thickness(0, 36, 0, 0),
                Content = ex.Message,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Foreground = new SolidColorBrush(Colors.Red),
            };
            Grid.SetRow(lblAlreadyExists, 1);
            MainGrid.Children.Add(lblAlreadyExists);
        };
        //< Label Content = "Label" HorizontalAlignment = "Center" Margin = "" Grid.Row = "1" VerticalAlignment = "Top" Width = "96" /
    }
    public ProductWindow()
    {
        InitializeComponent();
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
    }
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

    private void ProductId_TextChanged(object sender, TextChangedEventArgs e)
    {
        var child = MainGrid.Children.OfType<Control>().Where(x => x.Name== "lblAlreadyExists").FirstOrDefault();
        if (child != null)
            MainGrid.Children.Remove(child);
        //UserTable.Children.Remove(lblAlreadyExists);
    }
}
