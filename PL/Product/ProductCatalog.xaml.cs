using BO;
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
using PL.Cart;

namespace PL.Product;

/// <summary>
/// Interaction logic for ProductCatalog.xaml
/// </summary>
public partial class ProductCatalog : Window
{
    BlApi.IBl? bl = BlApi.Factory.Get();
    BO.Cart myCart = new() {OrderItemList=new()};
    public BO.ProductItem selectedProduct
    {
        get { return (BO.ProductItem)GetValue(selectedProductProperty); }
        set { SetValue(selectedProductProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty selectedProductProperty =
        DependencyProperty.Register("selectedProduct", typeof(BO.ProductItem), typeof(ProductCatalog));

    public BO.Enums.Category? selectedCategory
    {
        get { return (BO.Enums.Category?)GetValue(selectedCategoryProperty); }
        set { SetValue(selectedCategoryProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty selectedCategoryProperty =
        DependencyProperty.Register("selectedCategory", typeof(BO.Enums.Category?), typeof(ProductCatalog));
    public System.Array categoryItems { get; set; } = Enum.GetValues(typeof(BO.Enums.Category));
    public IEnumerable<BO.ProductItem?> productCatalog
    {
        get { return (IEnumerable<BO.ProductItem?>)GetValue(productCatalogProperty); }
        set { SetValue(productCatalogProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty productCatalogProperty =
        DependencyProperty.Register("productCatalog", typeof(IEnumerable<BO.ProductItem?>), typeof(ProductCatalog));

    public ProductCatalog()
    {
        selectedCategory = null;
        if (bl != null) productCatalog = bl.Product.GetAllProductItems(myCart);
        InitializeComponent();
    }
    public ProductCatalog(BO.Cart c)
    {
        myCart = c;
        selectedCategory = null;
        if (bl != null) productCatalog = bl.Product.GetAllProductItems(myCart);
        InitializeComponent();
    }
    private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        new ProductItem(selectedProduct,myCart).ShowDialog();//after the add window close updating the list
        this.Close();
        if (bl != null) productCatalog = bl.Product.GetAllProductItems(myCart);
        selectedCategory = null;
    }
    #region categorySelector_SelectionChanged
    /// <summary>
    /// screen out the list of product by category that selected
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void categorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (bl != null) productCatalog = bl.Product.GetProductItemsByCategory(myCart,(BO.Enums.Category?)selectedCategory);
    }
    #endregion

    private void NavigateToCartWindow(object sender, RoutedEventArgs e)
    {
        new CartWindow(myCart).Show();
        this.Close();
    }
}
