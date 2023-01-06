using System;
using System.Collections.Generic;
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
    BlApi.IBl? bl = BlApi.Factory.Get();
    public IEnumerable<BO.ProductForList?> productList
    {
        get { return (IEnumerable<BO.ProductForList?>)GetValue(productListProperty); }
        set { SetValue(productListProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty productListProperty =
        DependencyProperty.Register("productList", typeof(IEnumerable<BO.ProductForList?>), typeof(ProductList));
    public BO.Product productSelected
    {
        get { return (BO.Product)GetValue(productSelectedProperty); }
        set { SetValue(productSelectedProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty productSelectedProperty =
        DependencyProperty.Register("productSelected", typeof(BO.Product), typeof(ProductList));

    #region constructor
    /// <summary>
    /// constructor
    /// </summary>
    public ProductList()

    {
        productSelected = new();
        productList = bl.Product.GetAllProduct();//get all the products
        InitializeComponent();
        //foreach (var item in Enum.GetValues(typeof(BO.Enums.Category)))//show it in rows
        //    categorySelector.Items.Add(item);
        //categorySelector.Items.Add("");
    }
    #endregion

    #region ShowProductWindow_Click
    /// <summary>
    /// open the add window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e">button</param>
    private void ShowProductWindow_Click(object sender, RoutedEventArgs e)
    {
        new ProductWindow().ShowDialog();//after the add window close updating the list
        //ProductListview.ItemsSource = bl?.Product.GetAllProduct();
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

        //if (categorySelector.SelectedItem.ToString() == "")
            //ProductListview.ItemsSource = bl?.Product.GetAllProduct();
        //else
            //ProductListview.ItemsSource = bl?.Product.GetProductsByCategory((BO.Enums.Category)categorySelector.SelectedValue);

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
        MessageBox.Show(productSelected.ToString());
        //BO.ProductForList pfl = (BO.ProductForList)ProductListview.SelectedValue;
        new ProductWindow(productSelected.Id).ShowDialog();//after the add window close updating the list
        //ProductListview.ItemsSource = bl?.Product.GetAllProduct();
    }
    #endregion
}
