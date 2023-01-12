using BO;
using System;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Reflection.Emit;
using System.Windows.Controls;

namespace PL.ProductWindows;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    #region properties
    BlApi.IBl? bl = BlApi.Factory.Get();
    public bool idIsReadOnly { get; set; } = false;
    public BO.Product product { get; set; } = new();
    public System.Array categoryItems { get; set; } = Enum.GetValues(typeof(BO.Enums.Category));
    #endregion

    #region Dependency Properties
    //public Label lblMessage
    //{
    //    get { return (Label)GetValue(marginProperty); }
    //    set { SetValue(marginProperty, value); }
    //}
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    //public static readonly DependencyProperty marginProperty =
    //    DependencyProperty.Register("lblMessage", typeof(Label), typeof(ProductWindow));


    public string message
    {
        get { return (string)GetValue(messageProperty); }
        set { SetValue(messageProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty messageProperty =
        DependencyProperty.Register("message", typeof(string), typeof(ProductWindow));

    // public string 
    public string btnAddOrUpdateContent
    {
        get { return (string)GetValue(btnAddOrUpdateContentProperty); }
        set { SetValue(btnAddOrUpdateContentProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty btnAddOrUpdateContentProperty =
        DependencyProperty.Register("btnAddOrUpdateContent", typeof(string), typeof(ProductWindow));


    #endregion

    #region AddButton_Click
    /// <summary>
    /// add new product or update existing product
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddButton_Click(object sender, RoutedEventArgs e)
    {

        //check validation
        if (product.Id == 0)
        {
            message = "invalid id"; return;
        }
        if (product.Category.ToString() == "")
        {
            message = "invalid category"; return;
        }
        if (product.Name == null)
        {
            message = "invalid name"; return;
            //lblMessage = new Label()
            //{
            //    Content = "name",
            //    HorizontalAlignment = HorizontalAlignment.Left,
            //    Margin = new Thickness(400, 230, 0,0),
            //    //Grid.Row = "1",
            //    VerticalAlignment = VerticalAlignment.Top
            //    //RenderTransformOrigin = "0.392,0.239"
            //};
            ////400,213,0,0
        }
        if (product.Price == 0)
        {
            message = "invalid price"; return;
        }
        if (product.InStock == 0)
        {
            message = "invalid in stock"; return;
        }
        try
        {
            if (btnAddOrUpdateContent == "update")
            { //update
                bl?.Product.UpdateProduct(product);
            }
            else//add
                bl?.Product.AddProduct(product);
            MessageBox.Show("succesfully");
            this.Close();//close the window
        }
        catch (ItemAlresdyExsistException ex)
        {
            message = ex.Message;
        }
        catch (InvalidValueException ex)
        {
            message = ex.Message;
        }

    }
    #endregion

    #region constructors
    /// <summary>
    /// constructor for adding product with out getting parameters
    /// </summary>
    public ProductWindow()
    {
        message = "";
        btnAddOrUpdateContent = "add";
        InitializeComponent();
    }

    /// <summary>
    /// constructor for building update window
    /// </summary>
    /// <param name="productId">id of product to update</param>
    public ProductWindow(int productId)
    {
        try
        {
            idIsReadOnly = true;
            BO.Product p = bl.Product.GetProductById(productId);
            product = new() { Name = p.Name, Id = p.Id, InStock = p.InStock, Price = p.Price, Category = p.Category };
            btnAddOrUpdateContent = "update";
        }
        catch (BO.ItemAlresdyExsistException ex)
        {
            message = ex.Message;
            new ProductList().Show();
            this.Close();
        }
        InitializeComponent();
    }
    #endregion

    #region PreviewTextInput

    private void PreviewTextInputInt(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }
    private void PreviewTextInputDouble(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new("[^0-9,.]+");
        e.Handled = regex.IsMatch(e.Text);
    }
    #endregion

    #region NavigateToProductList
    private void NavigateToProductList(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
    #endregion
}
