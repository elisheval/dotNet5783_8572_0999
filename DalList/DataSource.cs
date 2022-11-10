using DO;
using static DO.Enums;
namespace Dal;

public static class DataSource
{
    #region StartDataSource
    /// <summary>
    /// there is a problem with starting data source class
    /// so we create this method to call her in the main in the begining of the program
    /// </summary>
    public static void StartDataSource()
    {
        return;
    }
    #endregion
    internal readonly static Random _rnd = new Random();
    /// <summary>
    /// data array of order items
    /// </summary>
    internal static OrderItem[] orderItemsArr = new OrderItem[200];
    #region addToOrderItemArr
    private static void addToOrderItemArr(OrderItem myOrderItem)
    {
        myOrderItem.Id = Config._IdentifyOrderItem;
        orderItemsArr[Config._IndexOrderItem] = myOrderItem;
        Config._IndexOrderItem++;
    }
    #endregion


    /// <summary>
    /// data array of products
    /// </summary>
    internal static Product[] productArr = new Product[50];
    #region addToProductArr
    private static void addToProductArr(Product myProduct)
    {
        myProduct.Id = Config._IdentifyProduct;
        productArr[Config._IndexProduct] = myProduct;
        Config._IndexProduct++;
    }
    #endregion


    /// <summary>
    /// data array of order
    /// </summary>
    internal static Order[] orderArr = new Order[100];
    #region addToOrderArr
    private static void addToOrderArr(Order myOrder)
    {
        myOrder.ID = Config._IdentifyOrder;
        orderArr[Config._IndexOrder] = myOrder;
        Config._IndexOrder++;
    }
    #endregion

    static DataSource()
    {
        s_Initialize();
    }
    #region s_Initialize
    private static void s_Initialize()
    {
        Category percussions = new Category();
        Category keyboards = new Category();
        Category exhalation = new Category();
        Category strings = new Category();
        Category additional = new Category();
        Enum.TryParse("0", out percussions);
        Enum.TryParse("1", out keyboards);
        Enum.TryParse("2", out exhalation);
        Enum.TryParse("3", out strings);
        Enum.TryParse("4", out additional);

        Product[] tmpProductArr = {
            #region assignment of 10 products
            new Product("drums", 900, percussions, 5),
            new Product("Salute",400,percussions,2),
            new Product("piano",20000,keyboards,1),
            new Product("accordion",3000,keyboards,10),
            new Product("harp",15000,strings,0),
            new Product("violin",1500,strings,7),
            new Product("flute",4000,exhalation,3),
            new Product("saxophone",2500,exhalation,1),
            new Product("guitar_textbook",70,additional,30),
            new Product("accordion_case",300,additional,10)
            #endregion
        };
        for (int i = 0; i < 10; i++)
        {
            addToProductArr(tmpProductArr[i]);
        }

        Order[] tmpOrderArr ={
            #region assignment of 20 orders
            new Order("elisheva","elisheva22@","katz34", new DateTime(2022,11,09) , DateTime.MinValue, DateTime.MinValue),
            new Order("miri","miri22@","agasi44", new DateTime(2022,11,02) , DateTime.MinValue, DateTime.MinValue),
            new Order("simi","simi22@","katz19", new DateTime(2022,10,05) , DateTime.MinValue, DateTime.MinValue),
            new Order("rina","rina@","drook2", new DateTime(2022,09,30) , DateTime.MinValue, DateTime.MinValue),
            new Order("tamar","tamar@","drook45", new DateTime(2022,09,15) , new DateTime(2022,09,22), DateTime.MinValue),
            new Order("nomi","nomi@","hakablan12", new DateTime(2022,09,07), new DateTime(2022, 09,09), DateTime.MinValue),
            new Order("shimon","simon@","agasi8", new DateTime(2022,09,07), new DateTime(2022, 09,09), DateTime.MinValue),
            new Order("refael","refael@","shaoolzon49",new DateTime(2022,08,22),new DateTime(2022,09,30), DateTime.MinValue),
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
            addToOrderArr(tmpOrderArr[i]);
        }

        Random rand = new Random();
        
        OrderItem[] tmpOrderItemArr ={
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
            new OrderItem(100010,1000010,300,rand.Next(1,5)),
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
            addToOrderItemArr(tmpOrderItemArr[i]);
        }
    }
    #endregion
    internal class Config
    {
        #region properties in config
        internal static int _IndexOrderItem { get; set; } = 0;
        internal static int _IndexOrder { get; set; } = 0;
        internal static int _IndexProduct { get; set; } = 0;

        private static int _identifyProduct = 100000;
        public static int _IdentifyProduct
        {
            get { return ++_identifyProduct; }
        }

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
}