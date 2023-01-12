using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PL.Order;

/// <summary>
/// Interaction logic for OrderItems.xaml
/// </summary>
public partial class OrderItems : Window
{
    #region properties
    BlApi.IBl? bl = BlApi.Factory.Get();
    public  bool manager { get; set; }
    public bool notSent { get; set; }
    public  int orderId { get; set; }
    #endregion

    #region Dependency Propertys include ObservableCollections
    public string message
    {
        get { return (string)GetValue(messageProperty); }
        set { SetValue(messageProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty messageProperty =
        DependencyProperty.Register("message", typeof(string), typeof(OrderItems));

    public bool visiblity
    {
        get { return (bool)GetValue(visiblityProperty); }
        set { SetValue(visiblityProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty visiblityProperty =
        DependencyProperty.Register("visiblity", typeof(bool), typeof(OrderItems));
    public bool visibiltyForm
    {
        get { return (bool)GetValue(visiblityFormProperty); }
        set { SetValue(visiblityFormProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty visiblityFormProperty =
        DependencyProperty.Register("visibiltyForm", typeof(bool), typeof(OrderItems));
   
    public int amount
    {
        get { return (int)GetValue(amountProperty); }
        set { SetValue(amountProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty amountProperty =
        DependencyProperty.Register("amount", typeof(int), typeof(OrderItems));
     public int productId
    {
        get { return (int)GetValue(productIdProperty); }
        set { SetValue(productIdProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty productIdProperty =
        DependencyProperty.Register("productId", typeof(int), typeof(OrderItems));

    public BO.OrderItem selectedOrderItem
    {
        get { return (BO.OrderItem)GetValue(orderItemProperty); }
        set { SetValue(orderItemProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty orderItemProperty =
        DependencyProperty.Register("selectedOrderItem", typeof(BO.OrderItem), typeof(OrderItems));

    public ObservableCollection<BO.OrderItem?> orderItemsList
    {
        get { return (ObservableCollection<BO.OrderItem?>)GetValue(orderItemsListProperty); }
        set { SetValue(orderItemsListProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty orderItemsListProperty =
        DependencyProperty.Register("orderItemsList", typeof(ObservableCollection<BO.OrderItem?>), typeof(OrderItems));
    #endregion

    #region constractor
    /// <summary>
    /// get the order items to display,
    /// getting details if the incoming is customer or manager,
    /// getting details about the status order 
    /// </summary>
    /// <param name="orderItems"></param>
    /// <param name="manager"></param>
    /// <param name="notSent"></param>
    /// <param name="orderId"></param>
    public OrderItems(List<BO.OrderItem>? orderItems, bool manager, bool notSent, int orderId )
    {
        if (orderItems != null)
        {
            orderItemsList = new ObservableCollection<BO.OrderItem?>((bl.Order.GetOrderById(orderId).OrderItemList!).Cast<BO.OrderItem?>());
            this.manager = manager;
            this.notSent = notSent;
            this.orderId = orderId;
            visiblity = manager;

            visibiltyForm = false;
            amount = 0;
            productId = 0;
            message = "";
        }
        InitializeComponent();
    }
    #endregion

    #region updateOrderItem
    private void updateOrderItem(object sender, MouseButtonEventArgs e)
    {
        if (manager && notSent&&bl!=null)
        {
            this.Hide();
            new OrderItem(selectedOrderItem, orderId).ShowDialog();
            orderItemsList = new ObservableCollection<BO.OrderItem?>((bl.Order.GetOrderById(orderId).OrderItemList!).Cast<BO.OrderItem?>());
            this.Show();
        }
    }
    #endregion

    #region AddNewOrderItem
    /// <summary>
    /// opening the form to take details of order item to add
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddNewOrderItem(object sender, RoutedEventArgs e)
    {
        visibiltyForm = true;
    }
    #endregion

    #region ConfirmAdding
    /// <summary>
    /// after taking details of order item to add the function updating the cart
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    private void ConfirmAdding(object sender, RoutedEventArgs e)
    {
        if (bl != null)
        {
            if (amount <= 0)
                message = "invalid amount";
            if(productId<=0)
                message = "invalid product id";

            try
            {
                bl.Order.UpdateOrder(orderId, productId, amount);
                MessageBox.Show("succesfully");
                visibiltyForm=false;
                orderItemsList = new ObservableCollection<BO.OrderItem?>((bl.Order.GetOrderById(orderId).OrderItemList!).Cast<BO.OrderItem?>());
                amount = 0;
                productId = 0;
            }
            catch (BO.InvalidValueException) { message = "invalid amount"; }
            catch (BO.NoFoundItemExceptions) { message = "no found product with this id"; }
        }
    }
    #endregion

    
}
