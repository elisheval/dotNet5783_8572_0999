﻿using DO;
using Dal;
using static DO.Enums;

namespace DalTest;

public class Program
{
    #region properties
    static private DalOrder order1=new DalOrder();
    static private DalOrderItem orderItem1=new DalOrderItem();
    static private DalProduct product1=new DalProduct();
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
                    Order orderToAdd = new Order(name, email, address, orderDate, shipDate, deliveryDate);
                    try
                    {
                        int orderId = order1.Add(orderToAdd);
                        Console.WriteLine("the id of the order:  " + orderId);
                    }
                    catch (Exception ex)
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
                        Order orderDetails = order1.Get(id);
                        Console.WriteLine(orderDetails);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                    ///get all the orders
                case CRUD.ReadAll:
                    Order[] orderArr = order1.GetAll();
                    foreach (Order order in orderArr)
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
                        order1.Delete(id);
                    }
                    catch (Exception ex)
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
                        orderDetailstmp = order1.Get(id);
                        Console.WriteLine(orderDetailstmp);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        break;
                    }
                    Order tmpOrder = new Order();
                    tmpOrder = orderDetailstmp;
                    String input;
                    ///the user dont have to update all the details of the order, 
                    ///so with thisvariable we checks what the user wanted to change and convert it to the currect type
                    Console.WriteLine("enter customer name");
                    input = Console.ReadLine();
                    if (input != "")
                        tmpOrder.CustomerName =input;
                    Console.WriteLine("enter customer email");
                    input = Console.ReadLine();
                    if (input != "")
                        tmpOrder.CustomerEmail =input;
                    Console.WriteLine("enter customer address");
                    input = Console.ReadLine();
                    if (input != "")
                        tmpOrder.CustomerAddress =input;
                    Console.WriteLine("enter order date");
                    input = Console.ReadLine();
                    if (input != "")
                        tmpOrder.OrderDate=DateTime.Parse(input);
                    Console.WriteLine("enter ship date");
                    input = Console.ReadLine();
                    if (input != "")
                        tmpOrder.ShipDate = DateTime.Parse(input);
                    Console.WriteLine("enter delivery date");
                    input = Console.ReadLine();
                    if (input != "")
                        tmpOrder.DeliveryDate = DateTime.Parse(input);
                    order1.Update(tmpOrder);
                    break;
                    ///exit and return us to the main
                case CRUD.exit:
                    break;
                default:
                    break;
            }
        }while (choose != CRUD.exit);
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
                    int productID =int.Parse(Console.ReadLine());
                    Console.WriteLine("enter order ID");
                    int orderID = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter order item price");
                    double price=double.Parse(Console.ReadLine());
                    Console.WriteLine("enter order item amount");
                    int amount = int.Parse(Console.ReadLine());
                    OrderItem tmpOrderItem = new OrderItem(productID,orderID,price,amount);
                   int id= orderItem1.Add(tmpOrderItem);
                    Console.WriteLine("the id of the order item is: " + id);
                    break;
                   ///get an order item by id
                case CRUD.Read:
                    Console.WriteLine("enter order item ID");
                     id=int.Parse(Console.ReadLine());
                    try
                    {
                        tmpOrderItem=orderItem1.Get(id);
                        Console.WriteLine(tmpOrderItem);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                    ///get all order items
                case CRUD.ReadAll:
                    OrderItem[] ordersItemsArr = orderItem1.GetAll();
                    foreach (OrderItem orderItem in ordersItemsArr)
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
                        orderItem1.Delete(id);
                    }
                    catch (Exception ex)
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
                        tmpOrderItem = orderItem1.Get(id);
                        Console.WriteLine(tmpOrderItem);
                    }
                    catch (Exception ex)
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
                    orderItem1.Update(tmpOrderItem);
                    break;
                    ///get an order item by order id and product id
                case CRUD.ReadByOrderAndProductIds:
                    Console.WriteLine("enter product id");
                    productID= int.Parse(Console.ReadLine());
                    Console.WriteLine("enter order id");
                    orderID = int.Parse(Console.ReadLine());
                    try
                    {
                        tmpOrderItem = orderItem1.GetByProductAndOrderIds(productID, orderID);
                        Console.WriteLine(tmpOrderItem);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                    ///get all order items by order id
                case CRUD.ReadByOrderId:
                    Console.WriteLine("enter order id");
                    orderID= int.Parse(Console.ReadLine());
                    try
                    {
                       OrderItem[] tmpOrderItemArr=orderItem1.getOrderItemsArrWithSpecificOrderId(orderID);
                        foreach (OrderItem orderItem in tmpOrderItemArr)
                        {
                            Console.WriteLine(orderItem);
                        }

                    }
                    catch (Exception ex)
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
                    Product tmpProduct = new Product(name, price, category, inStock);
                    int productID=product1.Add(tmpProduct);
                    Console.WriteLine("pruduct id you added is: " + productID);
                    break;
                    ///get product by id
                case CRUD.Read:
                    Console.WriteLine("enter product ID");
                    int id=int.Parse(Console.ReadLine());
                    try
                    {
                        tmpProduct= product1.Get(id);
                        Console.WriteLine(tmpProduct);
                    }
                    catch(Exception ex){
                        Console.WriteLine(ex.Message);
                    }
                    break;
                    ///get all products
                case CRUD.ReadAll:
                    Product[] productArr=product1.GetAll();
                    foreach(Product product in productArr)
                    {
                        Console.WriteLine(product);
                    }
                    break;
                    ///delete product by id
                case CRUD.Delete:
                    Console.WriteLine("enter product ID to delete");
                    id=int.Parse(Console.ReadLine());
                    try
                    {
                        product1.Delete(id);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                    ///update product by id
                case CRUD.Update:
                    Console.WriteLine("enter product ID to update");
                    tmpProduct=new Product();
                    id = int.Parse(Console.ReadLine());
                    try
                    {
                        tmpProduct = product1.Get(id);
                        Console.WriteLine(tmpProduct);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    ///the user dont have to update all the details of the order, 
                    ///so with thisvariable we checks what the user wanted to change and convert it to the currect type
                    String input;
                    Console.WriteLine("enter product name");
                    input = Console.ReadLine();
                    if(input!="")
                        tmpProduct.Name = input;
                    Console.WriteLine("enter product price");
                    input = Console.ReadLine();
                    if(input!="")
                        tmpProduct.Price=double.Parse(input);
                    Console.WriteLine("enter product category");
                    Console.WriteLine("enter product category");
                    Console.WriteLine("0 for percussions");
                    Console.WriteLine("1 for keyboards");
                    Console.WriteLine("2 for exhalation");
                    Console.WriteLine("3 for strings");
                    Console.WriteLine("4 for additional");
                    input =Console.ReadLine();
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
                    product1.Update(tmpProduct);
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
        orderItem1.Add(new OrderItem(1,1,1,1));

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
        while (choose!=Entity.exit);
    }
    #endregion
}
