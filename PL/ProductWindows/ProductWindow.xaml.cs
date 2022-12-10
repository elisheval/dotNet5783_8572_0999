using BlApi;
using BlImplementation;
using BO;
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

namespace PL.ProductWindows;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    IBl bl = new Bl();
    private void AddButton_Click(object sender, RoutedEventArgs e) {
        BO.Product p=new BO.Product() {Id=int.Parse(ProductId.Text),
                                       Price=double.Parse(ProductPrice.Text),
                                       InStock=int.Parse(ProductInStock.Text),
                                       Name=ProductName.Text,
                                       Category=(BO.Enums.Category?)CategorySelector.SelectedValue };
        try { 
            if (btnAddOrUpdate.Content.ToString() == "update") 
                   bl.Product.UpdateProduct(p);
            else  bl.Product.AddProduct(p);
            MessageBox.Show("succesfully");
        }

        catch(ItemAlresdyExsistException ex)
        {
            MessageBox.Show(ex.Message);
        }
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
        ProductPrice.Text = p.Price.ToString();
        ProductInStock.Text = p.InStock.ToString();
        ProductName.Text = p.Name;
        CategorySelector.SelectedItem = p.Category;
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        
     
    }

    private void ProductId_TextChanged(object sender, TextChangedEventArgs e)
    {

    }
}
