using BO;
using PL.ProductWindows;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PL.Cart;

namespace PL.Product;

/// <summary>
/// Interaction logic for ProductCatalog.xaml
/// </summary>
public partial class ProductCatalog : Window
{
    #region properties
    BlApi.IBl? bl = BlApi.Factory.Get();
    public System.Array categoryItems { get; set; } = Enum.GetValues(typeof(BO.Enums.Category));

    BO.Cart myCart = new() {OrderItemList=new()};
    #endregion

    #region Dependency Propertys
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
    public IEnumerable<BO.ProductItem?> productCatalog
    {
        get { return (IEnumerable<BO.ProductItem?>)GetValue(productCatalogProperty); }
        set { SetValue(productCatalogProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty productCatalogProperty =
        DependencyProperty.Register("productCatalog", typeof(IEnumerable<BO.ProductItem?>), typeof(ProductCatalog));
    #endregion

    #region constractors
    /// <summary>
    /// this window opens either from the main window,
    /// either from the cart window,
    /// when she opens from the cart she getting the update cart  
    /// that may had changes in the cat window
    /// </summary>
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
    #endregion

    #region ListView_MouseDoubleClick
    /// <summary>
    /// navigate to product window passing the selected produt
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        new ProductItem(selectedProduct,myCart).Show();//after the add window close updating the list
        this.Close();
        if (bl != null) productCatalog = bl.Product.GetAllProductItems(myCart);
        selectedCategory = null;
    }
    #endregion

    #region categorySelector_SelectionChanged
    /// <summary>
    /// screen out the list of product by category that selected
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void categorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
         if (bl != null) productCatalog = bl.Product.GetProductItemsByCategory(myCart, (BO.Enums.Category?)selectedCategory);
    }
    #endregion

    #region NavigateToCartWindow
    private void NavigateToCartWindow(object sender, RoutedEventArgs e)
    {
        new CartWindow(myCart).Show();
        this.Close();
    }
    #endregion

    private void NavigateToMainWindow(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        this.Close();
    }
}
