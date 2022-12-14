using BlApi;
using BlImplementation;
using static BO.Enums;
using BO;
using System.Transactions;

namespace BlTest;
internal class Program
{
    #region print object bunus
    private static void _print<T>(T obj)
    {
        var type = obj?.GetType();
        if (type != null)
            foreach (var pInfo in type?.GetProperties())
            {
                Console.Write(pInfo.Name + ": ");
                Console.WriteLine(pInfo.GetValue(obj, null));

            }
    }
    #endregion

    #region IBl property
    static private IBl blTmp = new Bl();
    static Cart cart = new() { OrderItemList = new List<OrderItem>() };//Creating an instance of a cart
    #endregion

    #region product options
    /// <summary>
    /// all the options of product
    /// </summary>
    static void productOptions()
    {
        ProductMethods choose;
        do
        {
            //showing the user all the options of product
            Console.WriteLine("enter 1 to get all the products");
            Console.WriteLine("enter 2 to get a details of product by id");
            Console.WriteLine("enter 3 to get a details of product item by id");
            Console.WriteLine("enter 4 to add a prduct");
            Console.WriteLine("enter 5 to delete a product");
            Console.WriteLine("enter 6 to update a product");
            Console.WriteLine("enter 0 to exit");

            //convert string to enum type
            Enum.TryParse(Console.ReadLine(), out choose);

            switch (choose)
            {
                //get all products
                case ProductMethods.GetAll:
                    IEnumerable<ProductForList?> productList = blTmp.Product.GetAllProduct();
                    foreach (var product in productList)
                    {
                        _print(product);//print all the product
                        Console.WriteLine();
                    }
                    break;

                //get product by id
                case ProductMethods.GetById:
                    Console.WriteLine("enter product Id");
                    int id = int.Parse(Console.ReadLine());
                    try
                    {
                        Product product = blTmp.Product.GetProductById(id);
                        _print(product);
                        Console.WriteLine();

                    }
                    catch (NoFoundItemExceptions ex)
                    {
                        Console.WriteLine(ex);
                    }
        
                    catch(InvalidValueException ex)
                    {
                        Console.WriteLine(ex);

                    }
                    break;

                ///get product item by id
                case ProductMethods.GetProductItemById:
                    Console.WriteLine("enter product ID");
                    id = int.Parse(Console.ReadLine());
                    try
                    {
                        ProductItem productItem = blTmp.Product.GetProductById(id, cart);
                        _print(productItem);
                    }
                    catch (NoFoundItemExceptions ex)
                    {
                        Console.WriteLine(ex);
                    }
                    catch (InvalidValueException ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;

                ///add product
                case ProductMethods.Add:
                    //getting all the details of product 
                    Console.WriteLine("enter product id");
                    id = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter product name");
                    string name = Console.ReadLine();
                    Console.WriteLine("enter product price");
                    double price = double.Parse(Console.ReadLine());
                    Console.WriteLine("enter product category");
                    Console.WriteLine("0 for percussions");
                    Console.WriteLine("1 for keyboards");
                    Console.WriteLine("2 for exhalation");
                    Console.WriteLine("3 for strings");
                    Console.WriteLine("4 for additional");
                    Category category;
                    Enum.TryParse(Console.ReadLine(), out category);
                    Console.WriteLine("enter amount in stock");
                    int inStock = int.Parse(Console.ReadLine());
                    Product tmpProduct = new Product()//creating an instance of product to add
                    {
                        Id = id,
                        Price = price,
                        Name = name,
                        Category = category,
                        InStock = inStock
                    };
                    try
                    {
                        blTmp.Product.AddProduct(tmpProduct);
                    }
                    catch (ItemAlresdyExsistException ex)
                    {
                        Console.WriteLine(ex);
                    }
                    catch (InvalidValueException ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;

                ///delete product by id
                case ProductMethods.Delete:
                    Console.WriteLine("enter product id");
                    id = int.Parse(Console.ReadLine());
                    try
                    {
                        blTmp.Product.DeleteProduct(id);
                    }
                    catch(ProductInOrderException ex)
                    {
                        Console.WriteLine(ex);
                    }
                    catch(NoFoundItemExceptions ex)
                    {
                        Console.WriteLine(ex);

                    }
                    break;

                    //update product
                case ProductMethods.Update:
                   // getting all product details to update
                    Console.WriteLine("enter product id");
                    id = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter product name");
                    name = Console.ReadLine();
                    Console.WriteLine("enter product price");
                    price = double.Parse(Console.ReadLine());
                    Console.WriteLine("enter product category");
                    Console.WriteLine("0 for percussions");
                    Console.WriteLine("1 for keyboards");
                    Console.WriteLine("2 for exhalation");
                    Console.WriteLine("3 for strings");
                    Console.WriteLine("4 for additional");
                    Enum.TryParse(Console.ReadLine(), out category);
                    Console.WriteLine("enter amount in stock");
                    inStock = int.Parse(Console.ReadLine());
                    tmpProduct = new Product()//creating an instance of product to add
                    {
                        Id = id,
                        Price = price,
                        Name = name,
                        Category = category,
                        InStock = inStock
                    };
                    try
                    {
                        blTmp.Product.UpdateProduct(tmpProduct);
                    }
                    catch (NoFoundItemExceptions ex)
                    {
                        Console.WriteLine(ex);
                    }
                    catch (InvalidValueException ex) 
                    { 
                        Console.WriteLine(ex); 
                    }
                    break;

                ///exit and return to the main
                case ProductMethods.Exit:
                    break;
                default:
                    Console.WriteLine("there is no option with this number, enter new choose");
                    break;
            }
        } while (choose != ProductMethods.Exit);
    }
    #endregion

    #region cart options
    /// <summary>
    /// all the options of cart for the user
    /// </summary>
    static void cartOptions()
    {
        CartMethod choose;
        do
        {
            //all the options to do
            Console.WriteLine("enter 1 to add a product to cart");
            Console.WriteLine("enter 2 to update amount of product in cart");
            Console.WriteLine("enter 3 to confirm the order");
            Console.WriteLine("enter 0 to exit");

            Enum.TryParse(Console.ReadLine(), out choose);

            switch (choose)
            {
                //add a new product to cart
                case CartMethod.AddProduct:
                    Console.WriteLine("enter product ID");
                    int id = int.Parse(Console.ReadLine());
                    try
                    {
                        Cart cartTmp = blTmp.Cart.AddProductToCart(id, cart);
                        _print(cartTmp);
                        Console.WriteLine();

                    }
                    catch (NoFoundItemExceptions ex)
                    {
                        Console.WriteLine(ex);
                    }
                    catch (ItemAlresdyExsistException ex)
                    {
                        Console.WriteLine(ex);
                    }
                    catch (ProductOutOfStockException ex)
                    {
                        Console.WriteLine(ex);
                    }
                    
                    break;

                //update amount of product in cart
                case CartMethod.UpdateAmount:
                    Console.WriteLine("enter the product id");
                    id = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter the new amount");
                    int newAmount = int.Parse(Console.ReadLine());
                    try
                    {
                        Cart cartTmp = blTmp.Cart.UpdateAmountOfProductInCart(id, cart, newAmount);
                        Console.WriteLine("the update cart is: ");
                        _print(cartTmp);
                        Console.WriteLine();

                    }
                    catch (ProductOutOfStockException ex)
                    {
                        Console.WriteLine(ex);
                    }
                    catch(ProductNotInCartException ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                //confirm the order
                case CartMethod.OrderConfirmation:
                    Console.WriteLine("enter customer name");
                    string name = Console.ReadLine();
                    Console.WriteLine("enter customer address");
                    string address = Console.ReadLine();
                    Console.WriteLine("enter customer email");
                    string email = Console.ReadLine();
                    try
                    {
                        blTmp.Cart.OrderConfirmation(cart, name, email, address);
                        cart= new Cart { OrderItemList= new List<OrderItem>()};
                    }
                    catch (InvalidValueException ex)
                    {
                        Console.WriteLine(ex);
                    }
                    catch (NoFoundItemExceptions ex)
                    {
                        Console.WriteLine(ex);
                    }
                    catch (ItemAlresdyExsistException ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                ///exit and return us to the main
                case CartMethod.Exit:
                    break;
                default:
                    Console.WriteLine("there is no option with this number, enter new choose");
                    break;
            }
        } while (choose != CartMethod.Exit);
    }
    #endregion

    #region order options
    static void orderOptions()
    {
        OrderMethod choose;
        do
        {
            //all the options to do
            Console.WriteLine("enter 1 to get all orders");
            Console.WriteLine("enter 2 to get details of order");
            Console.WriteLine("enter 3 to update order status to send");
            Console.WriteLine("enter 4 to update order status to supplied");
            Console.WriteLine("enter 5 to order tracking");
            Console.WriteLine("enter 6 to update order");
            Console.WriteLine("enter 0 to exit");

            Enum.TryParse(Console.ReadLine(), out choose);

            switch (choose)
            {
                //get all orders
                case OrderMethod.GetAllOrders:
                    IEnumerable<OrderForList?> orderForLists = blTmp.Order.GetAllOrders();
                    foreach (var orderForList in orderForLists)//print all the orders
                    {
                        _print(orderForList);
                        Console.WriteLine();
                    }
                    break;
                //get order details by id
                case OrderMethod.GetOrderDetById:
                    Console.WriteLine("enter the order id");
                    int id = int.Parse(Console.ReadLine());
                    try
                    {
                        Order order = blTmp.Order.GetOrderById(id);
                        _print(order);
                        Console.WriteLine();

                    }
                    catch (InvalidValueException ex)
                    {
                        Console.WriteLine(ex);
                    }
                    catch (NoFoundItemExceptions ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;

                //update order status to send
                case OrderMethod.UpdateOrderStatusToSend:
                    Console.WriteLine("enter order id");
                    id = int.Parse(Console.ReadLine());
                    try
                    {
                        Order order = blTmp.Order.OrderShippingUpdate(id);
                        _print(order);
                        Console.WriteLine();
                    }
                    catch (NoFoundItemExceptions ex)
                    {
                        Console.WriteLine(ex);
                    }
                    catch (InvalidDateChange ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;

                    //update order status to supplied
                case OrderMethod.UpdateOrderStatusToSupplied:
                    Console.WriteLine("enter order id");
                    id = int.Parse(Console.ReadLine());
                    try
                    {
                        Order order = blTmp.Order.OrderDeliveryUpdate(id);
                        _print(order);
                        Console.WriteLine();
                    }

                    catch (NoFoundItemExceptions ex)
                    {
                        Console.WriteLine(ex);
                    }
                    catch (InvalidDateChange ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;

                    //get order tracking
                case OrderMethod.OrderTracking:
                    Console.WriteLine("enter order id");
                    id = int.Parse(Console.ReadLine());
                    try
                    {
                        OrderTracking orderTracking = blTmp.Order.Ordertracking(id);
                        _print(orderTracking);
                        Console.WriteLine();
                    }
                    catch (NoFoundItemExceptions ex)
                    {
                        Console.WriteLine(ex);
                    }
                    catch (InvalidValueException ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;

                ///exit and return us to the main
                case OrderMethod.UpdateOrder:
                    Console.WriteLine("enter id of order");
                    int orderId=int.Parse(Console.ReadLine());
                    Console.WriteLine("enter id of product");
                    int productId=int.Parse(Console.ReadLine());
                    Console.WriteLine("enter amount to change, to delete enter 0");
                    int amount=int.Parse(Console.ReadLine());
                    try
                    {
                        blTmp.Order.UpdateOrder(orderId, productId, amount);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case OrderMethod.Exit:
                    break;
                default:
                    Console.WriteLine("there is no option with this number, enter new choose");
                    Console.WriteLine();
                        break;
            }
        } while (choose != OrderMethod.Exit);
    }
    #endregion

    #region Main
    /// <summary>
    /// the main function that getting a number from the user
    /// and navigate him for options of specific entity
    /// </summary>
    /// <param name="args"></param>
    private static void Main(string[] args)
    {
        Entity choose;
        do
        {
            ///showing the user all options 
            Console.WriteLine("enter 1 for product options");
            Console.WriteLine("enter 2 for order options");
            Console.WriteLine("enter 3 for cart options");
            Console.WriteLine("enter 0 to exit");

            //convert string to enum type
            Enum.TryParse(Console.ReadLine(), out choose);

            switch (choose)
            {
                
                case Entity.Product:
                    productOptions();
                    break;

                case Entity.Order:
                    orderOptions();
                    break;

                case Entity.Cart:
                    cartOptions();
                    break;

                case Entity.exit:
                    break;
                default:
                    Console.WriteLine("there is no option with this number,enter new choose");
                    break;
            }

        }
        while (choose != Entity.exit);
    }
    #endregion  
}