using PL.ProductWindows;
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

namespace PL.Product;

/// <summary>
/// Interaction logic for ProductItem.xaml
/// </summary>
public partial class ProductItem : Window
{
    BlApi.IBl? bl = BlApi.Factory.Get();
    BO.Cart myCart;
    public BO.ProductItem productItem
    {
        get { return (BO.ProductItem)GetValue(productItemProperty); }
        set { SetValue(productItemProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty productItemProperty =
        DependencyProperty.Register("productItem", typeof(BO.ProductItem), typeof(ProductItem));

    public ProductItem(BO.ProductItem pi,BO.Cart cart)
    {
        myCart = cart;
        productItem = pi;
        InitializeComponent();
    }

    private void AddProductToCart(object sender, RoutedEventArgs e)
    {
      if(bl!=null) myCart=bl.Cart.AddProductToCart(productItem.Id,myCart);
        new ProductCatalog(myCart).Show();
        this.Close();
    }

    private void RemoveProductFromCart(object sender, RoutedEventArgs e)
    {
        BO.OrderItem? orderItem= myCart.OrderItemList.Where(x => x.ProductId==productItem.Id).FirstOrDefault();
        if(orderItem!=null)
        myCart.OrderItemList.Remove(orderItem);
        MessageBox.Show("succesfully");
        new ProductCatalog(myCart).Show();
        this.Close();
    }
}
