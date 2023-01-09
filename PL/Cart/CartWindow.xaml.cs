using PL.Order;
using PL.Product;
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

namespace PL.Cart;

/// <summary>
/// Interaction logic for CartWindow.xaml
/// </summary>
public partial class CartWindow : Window
{
    BO.Cart myCart;
    BlApi.IBl? bl = BlApi.Factory.Get();
    public int amount
    {
        get { return (int)GetValue(amountProperty); }
        set { SetValue(amountProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty amountProperty =
        DependencyProperty.Register("amount", typeof(int), typeof(CartWindow));

    public BO.ProductItem selectedProduct
    {
        get { return (BO.ProductItem)GetValue(selectedProductProperty); }
        set { SetValue(selectedProductProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty selectedProductProperty =
        DependencyProperty.Register("selectedProduct", typeof(BO.ProductItem), typeof(CartWindow));

    public IEnumerable<BO.OrderItem?> orderItemsList
    {
        get { return (IEnumerable<BO.OrderItem?>)GetValue(orderItemsListProperty); }
        set { SetValue(orderItemsListProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty orderItemsListProperty =
        DependencyProperty.Register("orderItemsList", typeof(IEnumerable<BO.OrderItem?>), typeof(OrderItems));

    public CartWindow(BO.Cart myCart)
    {
        amount = 0;
        this.myCart = myCart;
        orderItemsList = myCart.OrderItemList!;
        InitializeComponent();
    }

    private void BtnRemoveItem(object sender, RoutedEventArgs e)
    {
        var element = e.OriginalSource as FrameworkElement;

        if (element != null && element.DataContext is BO.OrderItem)
        {
            if(myCart.OrderItemList!=null)myCart.OrderItemList.Remove((element.DataContext as BO.OrderItem)!);
        }
        orderItemsList = myCart.OrderItemList!;
    }

    private void NavigateToConfirmOrder(object sender, RoutedEventArgs e)
    {
        new ConfirmOrder(myCart).Show();
        this.Close();
    }
    private void NavigateToProductCatalog(object sender, RoutedEventArgs e)
    {
        new ProductCatalog(myCart).Show();
        this.Close();
    }

    private void btnAddAmount(object sender, RoutedEventArgs e)
    {
        var element = e.OriginalSource as FrameworkElement;

        if (element != null && element.DataContext is BO.OrderItem)
        {
            if (myCart.OrderItemList != null && bl!=null)
            {
                MessageBox.Show((element.DataContext as BO.OrderItem)!.ProductId.ToString(),amount.ToString());
                myCart = bl.Cart.UpdateAmountOfProductInCart((element.DataContext as BO.OrderItem)!.ProductId, myCart, amount);
            }
            orderItemsList=myCart.OrderItemList!;
            MessageBox.Show("succesfully");
        }
    }
}
