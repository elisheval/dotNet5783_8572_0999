using DO;
using System.ComponentModel;

namespace Dal;

internal static class DataSource
{

    internal readonly static Random rnd = new Random();

    internal static OrderItem[] orderItemsArr = new OrderItem[200];
    private static void addToOrderItemArr(OrderItem myOrderItem)
    {
        myOrderItem.Id = Config.IdentifyOrderItem;
        orderItemsArr[Config.IndexOrderItem] = myOrderItem;
        Config.IndexOrderItem++;
    }

    internal static Product[] productArr = new Product[50];
    private static void addToProductArr(Product myProduct)
    {
        myProduct.Id = Config.IdentifyProduct;
        productArr[Config.IndexProduct] = myProduct;
        Config.IndexProduct++;
    }

    internal static Order[] orderArr = new Order[100];
    private static void addToOrderArr(Order myOrder)
    {
        myOrder.ID = Config.IdentifyOrder;
        orderArr[Config.IndexOrder] = myOrder;
        Config.IndexOrder++;
    }

    static DataSource()
    {
        s_Initialize();
    }
    private static void s_Initialize()
    {
        Product[] tmpProductArr{("drums", 900, 5) };
        for(int i=0; i<10; i++)
        {                                                             
            addToProductArr(tmpProductArr[i]);    
        }

        Order[] tmpOrderArr = new Order[20];
        for (int i = 0; i < 20; i++)
        {
            addToOrderArr(tmpOrderArr[i]);
        }

        OrderItem[] tmpOrderItemArr = new OrderItem[40];
        for (int i = 0; i < 40; i++)
        {
            addToOrderItemArr(tmpOrderItemArr[i]);
        }
    }

    internal class Config
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