using PL.Manager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.ProductWindows;
/// <summary>
/// Interaction logic for ProductList.xaml
/// </summary>
/// 
public partial class ProductList : Window
{
    #region properties
    BlApi.IBl? bl = BlApi.Factory.Get();
    public System.Array categoryItems { get; set; } = Enum.GetValues(typeof(BO.Enums.Category));

    #endregion

    #region Dependency Propertys
    public BO.Enums.Category? selectedCategory
    {
        get { return (BO.Enums.Category?)GetValue(selectedCategoryProperty); }
        set { SetValue(selectedCategoryProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty selectedCategoryProperty =
        DependencyProperty.Register("selectedCategory", typeof(BO.Enums.Category?), typeof(ProductList));
    public IEnumerable<BO.ProductForList?> productList
    {
        get { return (IEnumerable<BO.ProductForList?>)GetValue(productListProperty); }
        set { SetValue(productListProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty productListProperty =
        DependencyProperty.Register("productList", typeof(IEnumerable<BO.ProductForList?>), typeof(ProductList));
    public BO.ProductForList productSelected
    {
        get { return (BO.ProductForList)GetValue(productSelectedProperty); }
        set { SetValue(productSelectedProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty productSelectedProperty =
        DependencyProperty.Register("productSelected", typeof(BO.ProductForList), typeof(ProductList));
    #endregion

    #region constructor
    /// <summary>
    /// constructor
    /// </summary>
    public ProductList()
    {
        selectedCategory = null;
        productList =bl.Product.GetAllProduct();//get all the products
        InitializeComponent();
    }
    #endregion

    #region ShowProductWindow_Click
    /// <summary>
    /// open the product window passing the selected product
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e">button</param>
    private void ShowProductWindow_Click(object sender, RoutedEventArgs e)
    {
        if (bl != null)
        {
            this.Hide();
            new ProductWindow().ShowDialog();//after the add window close updating the list
            this.Show();
            productList = bl.Product.GetAllProduct();
        }
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
        if (bl != null)productList =bl.Product.GetProductsByCategory((BO.Enums.Category)selectedCategory!);
    }
    #endregion

    #region ListView_MouseDoubleClick
    /// <summary>
    /// open the update window with the props of the tapped row
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        this.Hide();
        new ProductWindow(productSelected.Id).ShowDialog();//after the add window close updating the list
        this.Show();
        if(bl!=null)productList = bl.Product.GetAllProduct();
    }
    #endregion

    #region NavigateToManagerWindow
    private void NavigateToManagerWindow(object sender, RoutedEventArgs e)
    {
        new ManagerWindow().Show();
        this.Close();
    }
    #endregion

    
}
