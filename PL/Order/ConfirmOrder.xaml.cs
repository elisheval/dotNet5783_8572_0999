using BO;
using PL.Product;
using System.Windows;

namespace PL.Order;

/// <summary>
/// Interaction logic for ConfirmOrder.xaml
/// </summary>
public partial class ConfirmOrder : Window
{
    #region properties
    BlApi.IBl? bl = BlApi.Factory.Get();
    BO.Cart myCart = new() { OrderItemList = new() };
    #endregion

    #region Dependency Propertys
    public string message
    {
        get { return (string)GetValue(messageProperty); }
        set { SetValue(messageProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty messageProperty =
        DependencyProperty.Register("message", typeof(string), typeof(ConfirmOrder));

    public BO.Order order
    {
        get { return (BO.Order)GetValue(orderProperty); }
        set { SetValue(orderProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty orderProperty =
        DependencyProperty.Register("order", typeof(BO.Order), typeof(ConfirmOrder));
    #endregion

    #region constractor
    /// <summary>
    /// getting the user cart
    /// </summary>
    /// <param name="cart"></param>
    public ConfirmOrder(BO.Cart cart)
    {
        message = "";
        order = new();
        myCart = cart;
        order.TotalOrderPrice = cart.TotalOrderPrice;
        InitializeComponent();
    }
    #endregion

    #region Order Confirmation
    /// <summary>
    /// check validation of user details input,
    /// and confirm the order
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OrderConfirmation(object sender, RoutedEventArgs e)
    {
        if (order.CustomerName == null)
        {
            message = "enter customer name ";
            return;
        }
        if (order.CustomerAddress == null)
        {
            message = "enter customer address ";
            return;
        }
        
        if (order.CustomerEmail == null)
        {
            message = "enter customer email ";
            return;
        }
        if (order.TotalOrderPrice == 0)
        {
            message = "no items in the order";
            return;
        }
        try
        {
            if (bl != null) bl.Cart.OrderConfirmation(myCart, order.CustomerName!, order.CustomerEmail!, order.CustomerAddress!);
            MessageBox.Show("succsesfully");
            new MainWindow().Show();
            this.Close();
        }
        catch (BO.InvalidValueException ex)
        {
            message = ex.Message;
        }
        catch (BO.ProductOutOfStockException ex)
        {
            message =ex.Message;
        }
    }
    #endregion

    #region NavigateToProductCatalog
    /// <summary>
    /// navigate back to product catalog
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NavigateToProductCatalog(object sender, RoutedEventArgs e)
    {
        new ProductCatalog(myCart).Show();
        this.Close();
    }
    #endregion

    #region IsValidEmail

    private bool IsValidEmail()
    {
        var trimmedEmail = order.CustomerEmail;

        if (trimmedEmail!=null&&trimmedEmail.EndsWith("."))
        {
            return false; // suggested by @TK-421
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(order.CustomerEmail);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }
    #endregion
}
