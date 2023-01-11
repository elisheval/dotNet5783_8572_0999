using System.Linq;
using System.Windows;

namespace PL.Product;
/// <summary>
/// Interaction logic for ProductItem.xaml
/// </summary>
public partial class ProductItem : Window
{
    #region properties

    BlApi.IBl? bl = BlApi.Factory.Get();
    public BO.Cart myCart { get; set; }
    #endregion

    #region Dependency Property
    public BO.ProductItem productItem
    {
        get { return (BO.ProductItem)GetValue(productItemProperty); }
        set { SetValue(productItemProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty productItemProperty =
        DependencyProperty.Register("productItem", typeof(BO.ProductItem), typeof(ProductItem));
    #endregion

    #region constractor
    public ProductItem(BO.ProductItem pi,BO.Cart cart)
    {
        myCart = cart;
        productItem = pi;
        InitializeComponent();
    }
    #endregion

    #region AddProductToCart
    private void AddProductToCart(object sender, RoutedEventArgs e)
    {
        try
        {
            if (bl != null) myCart = bl.Cart.AddProductToCart(productItem.Id, myCart);
            MessageBox.Show("succesfully");
        }
        catch(BO.ProductOutOfStockException ex)
        {
            MessageBox.Show(ex.Message);
        }
        new ProductCatalog(myCart).Show();
        this.Close();
    }
    #endregion

    #region RemoveProductFromCart
    private void RemoveProductFromCart(object sender, RoutedEventArgs e)
    {
        if (myCart.OrderItemList != null)
        {
            BO.OrderItem? orderItem = myCart.OrderItemList.Where(x => x.ProductId == productItem.Id).FirstOrDefault();
            if (orderItem != null)
            {
                if(bl!=null)myCart=bl.Cart.UpdateAmountOfProductInCart(orderItem.ProductId,myCart,0);
                MessageBox.Show("succesfully");
            }
            else MessageBox.Show("the product don't exist in your cart");
        }
        else
        MessageBox.Show("the product don't exist in your cart");
        new ProductCatalog(myCart).Show();
        this.Close();
        return;
    }
    #endregion

    #region NavigateToProductCatalog
    private void NavigateToProductCatalog(object sender, RoutedEventArgs e)
    {
        new ProductCatalog(myCart).Show();
        this.Close();
    }
    #endregion
}
