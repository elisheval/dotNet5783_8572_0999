using DO;
using Dal;
using static DO.Enums;
using DalApi;

namespace DalTest;
public class Program
{
    #region properties
    //static private DalOrder order1=new DalOrder();
    //static private DalOrderItem orderItem1=new DalOrderItem();
    //static private DalProduct product1=new DalProduct();
    static private IDal dalListTmp = new DalList();
    #endregion

    #region orderOptions
    /// <summary>
    /// all the options of order for the user
    /// </summary>
    static void orderOptions()
    {
        CRUD choose;
        do
        {
            ///all the options to do
            Console.WriteLine("enter 1 to add an order");
            Console.WriteLine("enter 2 to get a details of order");
            Console.WriteLine("enter 3 to get all the orders");
            Console.WriteLine("enter 4 to delete an order");
            Console.WriteLine("enter 5 to update an order");
            Console.WriteLine("enter 0 to exit");

            Enum.TryParse(Console.ReadLine(), out choose);
            switch (choose)
            {
                ///add a new order
                case CRUD.Create:
                    Console.WriteLine("enter customer name");
                    string name = Console.ReadLine();
                    Console.WriteLine("enter customer email");
                    string email = Console.ReadLine();
                    Console.WriteLine("enter customer address");
                    string address = Console.ReadLine();
                    Console.WriteLine("enter order date");
                    DateTime orderDate = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("enter ship date");
                    DateTime shipDate = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("enter delivery date");
                    DateTime deliveryDate = DateTime.Parse(Console.ReadLine());
                    Order orderToAdd = new() { CustomerName=name,CustomerEmail=email,CustomerAddress=address,OrderDate=orderDate,ShipDate=shipDate,DeliveryDate=deliveryDate};
                    try
                    {
                        int orderId = dalListTmp.Order.Add(orderToAdd);
                        Console.WriteLine("the id of the order:  " + orderId);
                    }
                    catch (NoFoundItemExceptions ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                ///get an order by id
                case CRUD.Read:
                    Console.WriteLine("enter the order id");
                    int id = int.Parse(Console.ReadLine());
                    try
                    {
                        Order orderDetails = dalListTmp.Order.GetByCondition(x=>x!=null&&x.Value.ID==id);
                        Console.WriteLine(orderDetails);
                    }
                    catch (NoFoundItemExceptions ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                ///get all the orders
                case CRUD.ReadAll:
                    IEnumerable<Order?> orderArr =dalListTmp.Order.GetAll();
                    foreach (var order in orderArr)
                    {
                        Console.WriteLine(order);
                    }
                    break;
                ///delete an order by id
                case CRUD.Delete:
                    Console.WriteLine("enter the order id");
                    id = int.Parse(Console.ReadLine());
                    try
                    {
                        dalListTmp.Order.Delete(id);
                    }
                    catch (NoFoundItemExceptions ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                ///update an order by id
                case CRUD.Update:
                    Console.WriteLine("enter the order id");
                    id = int.Parse(Console.ReadLine());
                    Order orderDetailstmp;
                    try
                    {
                        orderDetailstmp = dalListTmp.Order.GetByCondition(x=>x!=null&&x.Value.ID==id);
                        Console.WriteLine(orderDetailstmp);
                    }
                    catch (NoFoundItemExceptions ex)
                    {
                        Console.WriteLine(ex.Message);
                        break;
                    }
                    Order tmpOrder = new();
                    tmpOrder = orderDetailstmp;
                    String input;
                    ///the user dont have to update all the details of the order, 
                    ///so with thisvariable we checks what the user wanted to change and convert it to the currect type
                    Console.WriteLine("enter customer name");
                    input = Console.ReadLine();
                    if (input != "")
                        tmpOrder.CustomerName = input;
                    Console.WriteLine("enter customer email");
                    input = Console.ReadLine();
                    if (input != "")
                        tmpOrder.CustomerEmail = input;
                    Console.WriteLine("enter customer address");
                    input = Console.ReadLine();
                    if (input != "")
                        tmpOrder.CustomerAddress = input;
                    Console.WriteLine("enter order date");
                    input = Console.ReadLine();
                    if (input != "")
                        tmpOrder.OrderDate = DateTime.Parse(input);
                    Console.WriteLine("enter ship date");
                    input = Console.ReadLine();
                    if (input != "")
                        tmpOrder.ShipDate = DateTime.Parse(input);
                    Console.WriteLine("enter delivery date");
                    input = Console.ReadLine();
                    if (input != "")
                        tmpOrder.DeliveryDate = DateTime.Parse(input);
                    dalListTmp.Order.Update(tmpOrder);
                    break;
                ///exit and return us to the main
                case CRUD.exit:
                    break;
                default:
                    break;
            }
        } while (choose != CRUD.exit);
    }
    #endregion

    #region orderItemOptions
    /// <summary>
    /// all the options of order items for the user
    /// </summary>
    static void orderItemOptions()
    {
        CRUD choose;
        do
        {
            ///showing the user all the options of order items
            Console.WriteLine("enter 1 to add an order item");
            Console.WriteLine("enter 2 to get a details of order item");
            Console.WriteLine("enter 3 to get all the orders");
            Console.WriteLine("enter 4 to delete an order item");
            Console.WriteLine("enter 5 to update an order item");
            Console.WriteLine("enter 6 to get an order item by product and order id");
            Console.WriteLine("enter 7 get all order items with specific order id");

            Console.WriteLine("enter 0 to exit");
            ///converting string to enum type
            Enum.TryParse(Console.ReadLine(), out choose);

            switch (choose)
            {
                ///add a new order item
                case CRUD.Create:
                    Console.WriteLine("enter product ID");
                    int productID = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter order ID");
                    int orderID = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter order item price");
                    double price = double.Parse(Console.ReadLine());
                    Console.WriteLine("enter order item amount");
                    int amount = int.Parse(Console.ReadLine());
                    OrderItem tmpOrderItem = new() { ProductId=productID,OrderId=orderID,Price=price,Amount=amount};
                    int id = dalListTmp.OrderItem.Add(tmpOrderItem);
                    Console.WriteLine("the id of the order item is: " + id);
                    break;
                ///get an order item by id
                case CRUD.Read:
                    Console.WriteLine("enter order item ID");
                    id = int.Parse(Console.ReadLine());
                    try
                    {
                        tmpOrderItem = dalListTmp.OrderItem.GetByCondition(x => x != null && x.Value.Id == id);
                        Console.WriteLine(tmpOrderItem);
                    }
                    catch (NoFoundItemExceptions ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                ///get all order items
                case CRUD.ReadAll:
                    IEnumerable<OrderItem?> ordersItemsArr =dalListTmp.OrderItem.GetAll();
                    foreach (var orderItem in ordersItemsArr)
                    {
                        Console.WriteLine(orderItem);
                    }
                    break;
                ///delete an order items by id
                case CRUD.Delete:
                    Console.WriteLine("enter order item ID to delete");
                    id = int.Parse(Console.ReadLine());
                    try
                    {
                        dalListTmp.OrderItem.Delete(id);
                    }
                    catch (NoFoundItemExceptions ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                ///update an order item by id 
                case CRUD.Update:
                    Console.WriteLine("enter order item ID to update");
                    tmpOrderItem = new OrderItem();
                    id = int.Parse(Console.ReadLine());
                    try
                    {
                        tmpOrderItem = dalListTmp.OrderItem.GetByCondition(x=>x!=null&&x.Value.Id==id);
                        Console.WriteLine(tmpOrderItem);
                    }
                    catch (NoFoundItemExceptions ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    ///the user dont have to update all the details of the order, 
                    ///so with thisvariable we checks what the user wanted to change and convert it to the currect type
                    String input;
                    Console.WriteLine("enter product ID");
                    input = Console.ReadLine();
                    if (input != "")
                        tmpOrderItem.ProductId = int.Parse(input);
                    Console.WriteLine("enter order ID");
                    input = Console.ReadLine();
                    if (input != "")
                        tmpOrderItem.OrderId = int.Parse(input);
                    Console.WriteLine("enter amount of order item");
                    input = Console.ReadLine();
                    if (input != "")
                        tmpOrderItem.Amount = int.Parse(input);
                    Console.WriteLine("enter order item price");
                    input = Console.ReadLine();
                    if (input != "")
                        tmpOrderItem.Price = double.Parse(input);
                    dalListTmp.OrderItem.Update(tmpOrderItem);
                    break;
                ///get an order item by order id and product id
                case CRUD.ReadByOrderAndProductIds:
                    Console.WriteLine("enter product id");
                    productID = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter order id");
                    orderID = int.Parse(Console.ReadLine());
                    try
                    {
                        tmpOrderItem = dalListTmp.OrderItem.GetByCondition(x => x!=null&&productID == x.Value.ProductId && orderID == x.Value.OrderId);
                        Console.WriteLine(tmpOrderItem);
                    }
                    catch (NoFoundItemExceptions ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                ///get all order items by order id
                case CRUD.ReadByOrderId:
                    Console.WriteLine("enter order id");
                    orderID = int.Parse(Console.ReadLine());
                    try
                    {
                        IEnumerable<OrderItem?> tmpOrderItemArr =dalListTmp.OrderItem.GetAll(x => x!=null&&orderID == x.Value.OrderId);
                        foreach (var orderItem in tmpOrderItemArr)
                        {
                            Console.WriteLine(orderItem);
                        }

                    }
                    catch (NoFoundItemExceptions ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                //exit and returns the user to the main
                case CRUD.exit:
                    break;
                default:
                    Console.WriteLine("there is no option with this number, enter new choose");
                    break;

            }
        } while (choose != CRUD.exit);
    }
    #endregion

    #region productOptions
    /// <summary>
    /// all the options of product
    /// </summary>
    static void productOptions()
    {
        CRUD choose;
        do
        {
            ///showing the user all the options of product
            Console.WriteLine("enter 1 to add a prduct");
            Console.WriteLine("enter 2 to get a details of product by id");
            Console.WriteLine("enter 3 to get all the products");
            Console.WriteLine("enter 4 to delete a product");
            Console.WriteLine("enter 5 to update a product");
            Console.WriteLine("enter 0 to exit");
            ///convert string to enum type
            Enum.TryParse(Console.ReadLine(), out choose);

            switch (choose)
            {
                ///add product
                case CRUD.Create:
                    Console.WriteLine("enter product id");
                    int id = int.Parse(Console.ReadLine());
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
                    Product tmpProduct = new() { Id=id,Name=name,Price=price,Category=(Category?)category,InStock=inStock};
                    try
                    {
                        int productID = dalListTmp.Product.Add(tmpProduct);
                        Console.WriteLine("pruduct id you added is: " + productID);
                    }
                    catch (ItemAlresdyExsistException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                ///get product by id
                case CRUD.Read:
                    Console.WriteLine("enter product ID");
                    id = int.Parse(Console.ReadLine());
                    try
                    {
                        tmpProduct = dalListTmp.Product.GetByCondition(x=>x!=null&&x.Value.Id==id);
                        Console.WriteLine(tmpProduct);
                    }
                    catch (NoFoundItemExceptions ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                ///get all products
                case CRUD.ReadAll:
                    IEnumerable<Product?> productArr =dalListTmp.Product.GetAll();
                    foreach (var product in productArr)
                    {
                        Console.WriteLine(product);
                    }
                    break;
                ///delete product by id
                case CRUD.Delete:
                    Console.WriteLine("enter product ID to delete");
                    id = int.Parse(Console.ReadLine());
                    try
                    {
                        dalListTmp.Product.Delete(id);
                    }
                    catch (NoFoundItemExceptions ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                ///update product by id
                case CRUD.Update:
                    Console.WriteLine("enter product ID to update");
                    tmpProduct = new Product();
                    id = int.Parse(Console.ReadLine());
                    try
                    {
                        tmpProduct = dalListTmp.Product.GetByCondition(x => x != null && x.Value.Id == id);
                        Console.WriteLine(tmpProduct);
                    }
                    catch (NoFoundItemExceptions ex)
                    {
                        Console.WriteLine(ex.Message);
                        break;
                    }

                    ///the user dont have to update all the details of the order, 
                    ///so with thisvariable we checks what the user wanted to change and convert it to the currect type
                    String input;
                    Console.WriteLine("enter product name");
                    input = Console.ReadLine();
                    if (input != "")
                        tmpProduct.Name = input;
                    Console.WriteLine("enter product price");
                    input = Console.ReadLine();
                    if (input != "")
                        tmpProduct.Price = double.Parse(input);
                    Console.WriteLine("enter product category");
                    Console.WriteLine("enter product category");
                    Console.WriteLine("0 for percussions");
                    Console.WriteLine("1 for keyboards");
                    Console.WriteLine("2 for exhalation");
                    Console.WriteLine("3 for strings");
                    Console.WriteLine("4 for additional");
                    input = Console.ReadLine();
                    if (input != "")
                    {
                        Category tmpC;
                        Enum.TryParse(input, out tmpC);
                        tmpProduct.Category = tmpC;
                    }
                    Console.WriteLine("enter amount in stock");
                    input = Console.ReadLine();
                    if (input != "")
                        tmpProduct.InStock = int.Parse(input);
                    dalListTmp.Product.Update(tmpProduct);
                    break;
                ///exit and return to the main
                case CRUD.exit:
                    break;
                default:
                    Console.WriteLine("there is no option with this number, enter new choose");
                    break;
            }
        } while (choose != CRUD.exit);
    }
    #endregion

    #region Main
    /// <summary>
    /// the main function that getting a number from the user
    /// and navigate him for options of specific struct
    /// </summary>
    /// <param name="args"></param>
    private static void Main(string[] args)
    {
        //adding item to start the date source class
        dalListTmp.OrderItem.Add(new OrderItem(1, 1, 1, 1));

        Entity choose;

        do
        {
            ///showing the user all options 
            Console.WriteLine("enter 1 for product options");
            Console.WriteLine("enter 2 for order options");
            Console.WriteLine("enter 3 for orderItem options");
            Console.WriteLine("enter 0 to exit");

            //convert string to enum type
            Enum.TryParse(Console.ReadLine(), out choose);

            switch (choose)
            {
                case Entity.Order:
                    orderOptions();
                    break;
                case Entity.OrderItem:
                    orderItemOptions();
                    break;
                case Entity.Product:
                    productOptions();
                    break;
                case Entity.exit:
                    break;
                default:
                    Console.WriteLine("there is no ooption with this number,enter new choose");
                    break;
            }

        }
        while (choose != Entity.exit);
    }
    #endregion
}