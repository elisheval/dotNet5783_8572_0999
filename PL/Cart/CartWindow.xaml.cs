using PL.Order;
using PL.Product;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PL.Cart;

/// <summary>
/// Interaction logic for CartWindow.xaml
/// </summary>
public partial class CartWindow : Window
{
    #region properties
    BO.Cart myCart;
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

    public ObservableCollection<BO.OrderItem?> OIList
    {
        get { return (ObservableCollection<BO.OrderItem?>)GetValue(OIListProperty); }
        set { SetValue(OIListProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty OIListProperty =
        DependencyProperty.Register("OIList", typeof(ObservableCollection<BO.OrderItem?>), typeof(OrderItems));
    #endregion

    #region constracror
    /// <summary>
    /// the window opening from productt catalog window 
    /// </summary>
    /// <param name="myCart"> user cart</param>
    public CartWindow(BO.Cart c)
    {

        myCart = c;
        OIList = new ObservableCollection<BO.OrderItem?>(c.OrderItemList!);
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
            //if (myCart.OrderItemList != null) myCart.OrderItemList.Remove((element.DataContext as BO.OrderItem)!);
            //if(myCart.OrderItemList!=null)orderItemsList = myCart.OrderItemList!;
            var a=element.DataContext as BO.OrderItem;
            if (myCart.OrderItemList != null&&bl!=null) OIList = new ObservableCollection<BO.OrderItem?>(((bl.Cart.UpdateAmountOfProductInCart(a.ProductId, myCart, 0)).OrderItemList));
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
        new ConfirmOrder(myCart).Show();
        this.Close();
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
        new ProductCatalog(myCart).Show();
        this.Close();
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
            if (myCart.OrderItemList != null && bl!=null)
            {
                try
                {
                    myCart = bl.Cart.UpdateAmountOfProductInCart((element.DataContext as BO.OrderItem)!.ProductId, myCart, (element.DataContext as BO.OrderItem)!.AmountInCart + 1);
                    OIList = new ObservableCollection<BO.OrderItem?>(myCart.OrderItemList!.Cast<BO.OrderItem?>());
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
            if (myCart.OrderItemList != null && bl != null)
            {
                try
                {
                    myCart = bl.Cart.UpdateAmountOfProductInCart((element.DataContext as BO.OrderItem)!.ProductId, myCart, (element.DataContext as BO.OrderItem)!.AmountInCart - 1);
                    OIList = new ObservableCollection<BO.OrderItem?>(myCart.OrderItemList!.Cast<BO.OrderItem?>());
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
