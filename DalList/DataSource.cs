﻿using DO;
using static DO.Enums;
namespace Dal;


internal static class DataSource
{
    #region variables
    internal readonly static Random _rnd = new();
    /// <summary>
    /// data list of order items
    /// </summary>
    internal static List<OrderItem?> orderItemsList=new();
    /// <summary>
    /// data list of products
    /// </summary>
    internal static List<Product?> productList=new();
    /// <summary>
    /// data list of order
    /// </summary>
    internal static List<Order?> orderList=new();
    #endregion

    #region DataSource ctor
    static DataSource()
    {
        s_Initialize();
    }
    #endregion

    #region addToOrderItemList
    /// <summary>
    /// method that get an order item and pushing it to the order items list
    /// </summary>
    /// <param name="myOrderItem">the order item to push to the array</param>
    private static void addToOrderItemList(OrderItem myOrderItem)
    {
        myOrderItem.Id = Config._IdentifyOrderItem;
        orderItemsList.Add(myOrderItem);
    }
    #endregion

    #region addToProductList
    /// <summary>
    /// method that get a product and pushing it to the product list
    /// </summary>
    /// <param name="myProduct">the product to add</param>
    private static void addToProductList(Product myProduct)
    {
        productList.Add(myProduct);
    }
    #endregion

    #region addToOrderList
    /// <summary>
    /// method that get an order and pushing it to the orders list
    /// <param name="myOrder">the order to add</param>
    private static void addToOrderList(Order myOrder)
    {
        myOrder.ID = Config._IdentifyOrder;
        orderList.Add(myOrder);
    }
    #endregion

    #region s_Initialize
    private static void s_Initialize()
    {
        #region converting strings to category enum type
        Category percussions = new ();
        Category keyboards = new ();
        Category exhalation = new ();
        Category strings = new ();
        Category additional = new ();
        Enum.TryParse("0", out percussions);
        Enum.TryParse("1", out keyboards);
        Enum.TryParse("2", out exhalation);
        Enum.TryParse("3", out strings);
        Enum.TryParse("4", out additional);
        #endregion

        List<Product> tmpProductList = new(){
            #region assignment of 10 products
            new Product(100001,"drums", 900, percussions, 5),
            new Product(100002,"Salute",400,percussions,2),
            new Product(100003,"piano",20000,keyboards,1),
            new Product(100004,"accordion",3000,keyboards,10),
            new Product(100005,"harp",15000,strings,0),
            new Product(100006,"violin",1500,strings,7),
            new Product(100007,"flute",4000,exhalation,3),
            new Product(100008,"saxophone",2500,exhalation,1),
            new Product(100009,"guitar_textbook",70,additional,30),
            new Product(100010,"accordion_case",300,additional,10)
            #endregion
        };
        for (int i = 0; i < 10; i++)
        {
            addToProductList(tmpProductList[i]);
        }

        List<Order> tmpOrderList = new(){
            
            #region assignment of 20 orders using dataTime.MinValue and timespan
            new Order(){CustomerName="elisheva",CustomerEmail="ee@",CustomerAddress="ddd76",OrderDate=new DateTime(2022,11,09),ShipDate=null,DeliveryDate=null},
            new Order(){CustomerName="miri",CustomerEmail="eemiri22" ,CustomerAddress="katz19",OrderDate=new DateTime(2022,10,05),ShipDate=null,DeliveryDate=null},
            new Order(){CustomerName="elis",CustomerEmail="@",CustomerAddress="agasu45",OrderDate=new DateTime(2022,11,02),ShipDate=null,DeliveryDate=null},
            new Order(){CustomerName="rina",CustomerEmail="rina",CustomerAddress="drouk76",OrderDate=new DateTime(2022,09,30),ShipDate=null,DeliveryDate=null},
            new Order(){CustomerName="tamar",CustomerEmail="tamar@",CustomerAddress="drook476",OrderDate=new DateTime(2022,09,15),ShipDate=new DateTime(2022,09,22),DeliveryDate=null},
            new Order(){CustomerName="nomi",CustomerEmail="nomi@",CustomerAddress="hakablan12",OrderDate=new DateTime(2022,09,07),ShipDate=new DateTime(2022, 09,09),DeliveryDate=null},
            new Order(){CustomerName="shimon",CustomerEmail="simon@",CustomerAddress="agasi8",OrderDate=new DateTime(2022,11,09),ShipDate=new DateTime(2022, 09,09),DeliveryDate=null},
            new Order(){CustomerName="refael",CustomerEmail="refael@",CustomerAddress="shaoolzon49",OrderDate=new DateTime(2022,08,22),ShipDate=new DateTime(2022,09,30),DeliveryDate=null},
            new Order("chaiim","chaim@","chai_taib19",new DateTime(2022,04,10),new DateTime(2022,04,14),new DateTime(2022,04,16)),
            new Order("nechama","nechama@","brand432",new DateTime(2022,04,03),new DateTime(2022,05,10),new DateTime(2022/05/12)),
            new Order("rachel","rachel@","katc54",new DateTime(2022,03,05),new DateTime(2022,05,10),new DateTime(2022,05,15)),
            new Order("yael","yael@","hantke87",new DateTime(2021,11,11),new DateTime(2022,11,11),new DateTime(2021,11,11)),
            new Order("maayan","maayan@","hakablan90",new DateTime(2021,11,11),new DateTime(2022,11,11),new DateTime(2021,11,11)),
            new Order("hadas","hadas@","drok76",new DateTime(2021,09,02),new DateTime(2022,11,11), new DateTime(2021,11,20)),
            new Order("efrat","efrat@","toravavodah9",new DateTime(2020,01,08),new DateTime(2020,01,22),new DateTime(2020,01,25)),
            new Order("ayala","ayala@","agasi7",new DateTime(2020,01,07),new DateTime(2020,01,22),new DateTime(2020,01,26)),
            new Order("tali","tali@","chaimchaviv35",new DateTime(2020,01,07),new DateTime(2020,02,22),new DateTime(2020,02,23)),
            new Order("dan","dan@","parvshtein89",new DateTime(2021,10,01),new DateTime(2020,10,10),new DateTime(2021,10,12)),
            new Order("gavriel","gavriel@","bergman3",new DateTime(2021,07,28),new DateTime(2021,07,30),new DateTime(2021,08,01)),
            new Order("ayala","qwerty@","miriam hanevia",new DateTime(2021,08,27),new DateTime(2021,09,03),new DateTime(2021,09,05))
            #endregion
        };
        for (int i = 0; i < 20; i++)
        {
            addToOrderList(tmpOrderList[i]);
        }

        Random rand = new();
        
        List<OrderItem> tmpOrderItemList =new(){
            #region assignment of 40 order items  
            new OrderItem(100001,100001,900,rand.Next(1,5)),
            new OrderItem(100002,100001,400,rand.Next(1,5)),
            new OrderItem(100003,100001,20000,rand.Next(1,5)),
            new OrderItem(100004,100002,3000,rand.Next(1,5)),
            new OrderItem(100005,100002,15000,rand.Next(1,5)),
            new OrderItem(100002,100002,400,rand.Next(1,5)),
            new OrderItem(100002,100003,400,rand.Next(1,5)),
            new OrderItem(100006,100003,1500,rand.Next(1,5)),
            new OrderItem(100007,100003,4000,rand.Next(1,5)),
            new OrderItem(100001,100003,900,rand.Next(1,5)),
            new OrderItem(100003,100004,20000,rand.Next(1,5)),
            new OrderItem(100008,100005,2500,rand.Next(1,5)),
            new OrderItem(100005,100005,15000,rand.Next(1,5)),
            new OrderItem(100003,100006,20000,rand.Next(1,5)),
            new OrderItem(100009,100007,70,rand.Next(1,5)),
            new OrderItem(100001,100007,900,rand.Next(1,5)),
            new OrderItem(100010,100007,300,rand.Next(1,5)),
            new OrderItem(100002,100008,400,rand.Next(1,5)),
            new OrderItem(100009,100009,70,rand.Next(1,5)),
            new OrderItem(100004,100009,3000,rand.Next(1,5)),
            new OrderItem(100010,100010,300,rand.Next(1,5)),
            new OrderItem(100004,100010,3000,rand.Next(1,5)),
            new OrderItem(100001,100011,900,rand.Next(1,5)),
            new OrderItem(100002,100012,400,rand.Next(1,5)),
            new OrderItem(100008,100012,2500,rand.Next(1,5)),
            new OrderItem(100009,100012,70,rand.Next(1,5)),
            new OrderItem(100002,100013,400,rand.Next(1,5)),
            new OrderItem(100005,100014,15000,rand.Next(1,5)),
            new OrderItem(100003,100015,20000,rand.Next(1,5)),
            new OrderItem(100007,100016,4000,rand.Next(1,5)),
            new OrderItem(100009,100016,70,rand.Next(1,5)),
            new OrderItem(100001,100017,900,rand.Next(1,5)),
            new OrderItem(100003,100018,20000,rand.Next(1,5)),
            new OrderItem(100003,100019,20000,rand.Next(1,5)),
            new OrderItem(100006,100019,1500,rand.Next(1,5)),
            new OrderItem(100002,100019,400,rand.Next(1,5)),
            new OrderItem(100010,100019,300,rand.Next(1,5)),
            new OrderItem(100010,100020,300,rand.Next(1,5)),
            new OrderItem(100001,100020,900,rand.Next(1,5)),
            new OrderItem(100005,100020,15000,rand.Next(1,5))
            #endregion
        };
        for (int i = 0; i < 40; i++)
        {
            addToOrderItemList(tmpOrderItemList[i]);
        }
    }
    #endregion

    #region class Config
    internal class Config
    {
        #region properties in config

        private static int _identifyOrderItem = 100000;
        public static int _IdentifyOrderItem
        {
            get { return ++_identifyOrderItem; }
        }

        private static int _identifyOrder = 100000;
        public static int _IdentifyOrder
        {
            get { return ++_identifyOrder; }
        }
        #endregion
    }
    #endregion
}