using BO;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Text.RegularExpressions;

namespace PL.ProductWindows;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    BlApi.IBl? bl = BlApi.Factory.Get();
    public string btnAddOrUpdateContent
    {
        get { return (string)GetValue(btnAddOrUpdateContentProperty); }
        set { SetValue(btnAddOrUpdateContentProperty, value); }
    }
    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty btnAddOrUpdateContentProperty =
        DependencyProperty.Register("btnAddOrUpdateContent", typeof(string), typeof(ProductWindow));

    public bool idIsReadOnly { get; set; }=false;
    public BO.Product product { get; set; } = new();
    public System.Array categoryItems { get; set; }=Enum.GetValues(typeof(BO.Enums.Category));

    #region AddButton_Click
    /// <summary>
    /// add new product or update existing product
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            //check validation
            if (product.Id ==0)
                throw new InvalidValueException("invalid id");
            if (product.Category.ToString() == "")
                throw new InvalidValueException("invalid category");
            if (product.Name == null)
                throw new InvalidValueException("invalid name");
            if (product.Price==0)
                throw new InvalidValueException("invalid price");
            if (product.InStock == 0)
                throw new InvalidValueException("invalid amount in stock");

            if (btnAddOrUpdateContent == "update")
            { //update
                bl?.Product.UpdateProduct(product);
            }
            else//add
                bl?.Product.AddProduct(product);
            MessageBox.Show("succesfully");
            Close();//close the window
        }
        catch (InvalidValueException ex)
        {
            //makes the place of lable acording to the type of error
            int place = 0;
            if (ex.Message == "invalid id")
                place = 122;
            if (ex.Message == "invalid price")
                place = 271;
            if (ex.Message == "invalid category")
                place = 182;
            if (ex.Message == "invalid amount in stock")
                place = 312;
            if (ex.Message == "invalid name")
                place = 231;
            Label invalidValue = new()//create label under the invalid textBox
            {
                Name = "invalidValue",
                Margin = new Thickness(80, place, 0, 0),
                Content = ex.Message,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Foreground = new SolidColorBrush(Colors.Red),
            };
            Grid.SetRow(invalidValue, 1); //put the label under the invalid textBox
            MainGrid.Children.Add(invalidValue);
            _addLableWithErrorMassege(place, "invalidValue", ex.Message);

        }
        catch (ItemAlresdyExsistException ex)
        {
            _addLableWithErrorMassege(36, "lblAlreadyExists", ex.Message);
        }
        
    }
    #endregion

    #region _addLableWithErrorMassege
    /// <summary>
    /// Prepares a lable for notification of an exception in an appropriate location according to the field in which there is an error
    /// </summary>
    /// <param name="place">margin where to put the lable</param>
    /// <param name="name"> name of the lable</param>
    /// <param name="massege">massege og the exception</param>
    private void _addLableWithErrorMassege(int place,string name,string massege)
    {
        Label lbl = new Label()
        {
            Name = name,
            Margin = new Thickness(0, place, 0, 0),
            Content = massege,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Top,
            Foreground = new SolidColorBrush(Colors.Red),
        };
        if (name == "lblAlreadyExists")
        {
            Grid.SetRow(lbl, 1);
            MainGrid.Children.Add(lbl);
        }
        if(massege == "invalidValue")
        {
            Grid.SetRow(lbl, 1);
            MainGrid.Children.Add(lbl);
        }
    }
    #endregion

    #region constructors
    /// <summary>
    /// constructor for adding product with out getting parameters
    /// </summary>
    public ProductWindow() 
    {
        btnAddOrUpdateContent = "add";
        InitializeComponent();
    }

    /// <summary>
    /// constructor for building update window
    /// </summary>
    /// <param name="productId">id of product to update</param>
    public ProductWindow(int productId)
    {
        idIsReadOnly = true;
        BO.Product p = bl.Product.GetProductById(productId);
        product = new() { Name = p.Name, Id = p.Id, InStock = p.InStock, Price = p.Price, Category = p.Category };
        btnAddOrUpdateContent = "update";
        InitializeComponent();
    }
    #endregion

    #region Text Changed in input
    /// <summary>
    /// remove the error massege when the text in the input changes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProductId_TextChanged(object sender, TextChangedEventArgs e)
    {
        var child1 = MainGrid.Children.OfType<Control>().Where(x => x.Name == "invalidValue").FirstOrDefault();
        if (child1 != null)
            MainGrid.Children.Remove(child1);
        var child2 = MainGrid.Children.OfType<Control>().Where(x => x.Name == "lblAlreadyExists").FirstOrDefault();
        if (child2 != null)
            MainGrid.Children.Remove(child2);
    }
    
    /// <summary>
    /// remove the error massege when the text in the input changes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProductPrice_TextChanged(object sender, TextChangedEventArgs e)
    {
        var child = MainGrid.Children.OfType<Control>().Where(x => x.Name == "invalidValue").FirstOrDefault();
        if (child != null)
            MainGrid.Children.Remove(child);
    }
    
    /// <summary>
    /// remove the error massege when the text in the input changes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProductInStock_TextChanged(object sender, TextChangedEventArgs e)
    {
        var child = MainGrid.Children.OfType<Control>().Where(x => x.Name == "invalidValue").FirstOrDefault();
        if (child != null)
            MainGrid.Children.Remove(child);
    }

    /// <summary>
    /// remove the error massege when the text in the input changes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProductCategory_TextChanged(object sender, SelectionChangedEventArgs e)
    {
        var child = MainGrid.Children.OfType<Control>().Where(x => x.Name == "invalidValue").FirstOrDefault();
        if (child != null)
            MainGrid.Children.Remove(child);
    }
    /// <summary>
    /// remove the error massege when the text in the input changes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProductName_TextChanged(object sender, TextChangedEventArgs e)
    {
        var child = MainGrid.Children.OfType<Control>().Where(x => x.Name == "invalidValue").FirstOrDefault();
        if (child != null)
            MainGrid.Children.Remove(child);
    }
    #endregion

    #region PreviewTextInput

    private void PreviewTextInputInt(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new ("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }
    private void PreviewTextInputDouble(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new("[^0-9,.]+");
        e.Handled = regex.IsMatch(e.Text);
    }
    #endregion

}
