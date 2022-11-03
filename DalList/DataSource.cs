using DO;
using System.ComponentModel;

namespace Dal;

internal static class DataSource
{
    internal static OrderItem[] orderItemsArr = new OrderItem[200];
    internal static Product[] productArr = new Product[50];
    internal static Order[] orderArr = new Order[100];

    internal readonly static Random rnd=new Random();
    static DataSource()
    {
        s_Initialize();
    }
    internal static void s_Initialize()
    {

           OrderItem[] tmpOrderItemsArr = new OrderItem { new OrderItem("לשלוח כאן ערכים ולבנות בנאי במחלקות")}
           OrderItem[] tmpOrderArr = new OrderItem[20];
           OrderItem[] tmpProductArr = new OrderItem[10];

    }
   
    private static void addToOrderArr(Order myOrder)
    {
        orderArr[Config.IndexOrder] = myOrder;
        Config.IndexOrder++;
    }
    private static void addToOrderItemArr(OrderItem myOrderItem)
    {
        orderItemsArr[Config.IndexOrderItem] = myOrderItem;
        Config.IndexOrderItem++;
    }
    private static void addToProductArr(Product myProduct)
    {

        productArr[Config.IndexProduct] = myProduct;
        Config.IndexProduct++;
    }
    internal struct Config
    {
        internal static int IndexOrderItem { get; set; } = 0;
        internal static int IndexOrder { get; set; } = 0;
        internal static int IndexProduct { get; set; } = 0;

        private static int identifyProduct = 100000;
        public static int IdentifyProduct
        {
            get { return identifyProduct++; }
        }

        private static int identifyOrderItem = 100000;
        public static int IdentifyOrderItem
        {
            get { return identifyOrderItem++; }
        }

        private static int identifyOrder = 100000;
        public static int IdentifyOrder
        {
            get { return identifyOrder++; }
        }
       

    }
}