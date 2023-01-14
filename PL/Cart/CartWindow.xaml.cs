using PL.Order;
using PL.Product;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace PL.Cart;

/// <summary>
/// Interaction logic for CartWindow.xaml
/// </summary>
public partial class CartWindow : Window, INotifyPropertyChanged
{
    #region properties
    public event PropertyChangedEventHandler? PropertyChanged;
    private BO.Cart? myCart;
    public BO.Cart? MyCart
    {
        get { return myCart; }
        set
        {
            myCart = value;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("Cart"));
        }
    }
    BlApi.IBl? bl = BlApi.Factory.Get();
    #endregion

    #region Dependency Propertys include ObservableCollections
    public string message
    {
        get { return (string)GetValue(messageProperty); }
        set { SetValue(messageProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty messageProperty =
        DependencyProperty.Register("message", typeof(string), typeof(CartWindow));
    #endregion

    #region construcror
    /// <summary>
    /// the window opening from productt catalog window 
    /// </summary>
    /// <param name="myCart"> user cart</param>
    public CartWindow(BO.Cart cart)
    {
        MyCart = cart;
        InitializeComponent();
    }
    #endregion

    #region BtnRemoveItem
    /// <summary>
    /// remove order item from user cart
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BtnRemoveItem(object sender, RoutedEventArgs e)
    {
        var element = e.OriginalSource as FrameworkElement;

        if (element != null && element.DataContext is BO.OrderItem)
        {
            var a = element.DataContext as BO.OrderItem;
            if (MyCart!.OrderItemList != null && bl != null) MyCart.OrderItemList.RemoveAll(item => item.ProductId == a!.ProductId);
            MessageBox.Show("the order item removed");
            NavigateToProductCatalog(sender, e);
        }
    }
    #endregion

    #region NavigateToConfirmOrder
    /// <summary>
    /// the function opens the confirm order window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NavigateToConfirmOrder(object sender, RoutedEventArgs e)
    {
        if (MyCart!= null)
        {
            new ConfirmOrder(MyCart).Show();
            this.Close();
        }
    }
    #endregion

    #region NavigateToProductCatalog
    /// <summary>
    /// return back to product catalog
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NavigateToProductCatalog(object sender, RoutedEventArgs e)
    {
        if (MyCart != null)
        {
            new ProductCatalog(MyCart).Show();
            this.Close();
        }
    }
    #endregion

    #region btnUpdateAmount
    /// <summary>
    /// updating amount of item in the cart
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnAddAmount(object sender, RoutedEventArgs e)
    {
        var element = e.OriginalSource as FrameworkElement;

        if (element != null && element.DataContext is BO.OrderItem)
        {
            if (MyCart!.OrderItemList != null && bl != null)
            {
                try
                {
                    MyCart = bl.Cart.UpdateAmountOfProductInCart((element.DataContext as BO.OrderItem)!.ProductId, MyCart, (element.DataContext as BO.OrderItem)!.AmountInCart + 1);
                    message = "the amount update succesfully";
                }
                catch (BO.ProductOutOfStockException ex)
                {
                    message = ex.Message;
                }
                catch (BO.InvalidValueException ex)
                {
                    message = ex.Message;
                }
            }

        }
    }
    private void btnSubtractAmount(object sender, RoutedEventArgs e)
    {
        var element = e.OriginalSource as FrameworkElement;

        if (element != null && element.DataContext is BO.OrderItem)
        {
            if (myCart!.OrderItemList != null && bl != null)
            {
                if ((element.DataContext as BO.OrderItem)!.AmountInCart ==1) { 
                    myCart.OrderItemList.RemoveAll(item => item.ProductId == (element.DataContext as BO.OrderItem)!.ProductId);
                    MessageBox.Show("the order item removed");
                    NavigateToProductCatalog(sender, e);
                    return;
                }
                try
                {
                    MyCart = bl.Cart.UpdateAmountOfProductInCart((element.DataContext as BO.OrderItem)!.ProductId, myCart, (element.DataContext as BO.OrderItem)!.AmountInCart - 1);
                    message = "the amount update succesfully";
                }
                catch (BO.ProductOutOfStockException ex)
                {
                    message = ex.Message;
                }
                catch (BO.InvalidValueException ex)
                {
                    message = ex.Message;
                }
            }

        }
    }
    #endregion
}
