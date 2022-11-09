using DO;
using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using static DO.Enums;

namespace Dal;

public static class DataSource
{
    public static void startDataSource()
    {
        return;
    }
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

        Product[] tmpProductArr = { new Product("drums", 900, percussions, 5),
                                    new Product("Salute",400,percussions,2),
                                    new Product("piano",20000,keyboards,1),
                                    new Product("accordion",3000,keyboards,10),
                                    new Product("harp",15000,strings,0),
                                    new Product("violin",1500,strings,7),
                                    new Product("flute",4000,exhalation,3),
                                    new Product("saxophone",2500,exhalation,1),
                                    new Product("guitar_textbook",70,additional,30),
                                    new Product("accordion_case",300,additional,10),
                                       };
        for (int i = 0; i < 10; i++)
        {
            addToProductArr(tmpProductArr[i]);
        }

        Order[] tmpOrderArr ={
            new Order("elisheva","elisheva22@","katz34"),
            new Order("miri","miri22@","agasi44"),
            new Order("simi","simi22@","katz19"),
            new Order("rina","rina@","drook2"),
            new Order("tamar","tamar@","drook45"),
            new Order("nomi","nomi@","hakablan12"),
            new Order("shimon","simon@","agasi8"),
            new Order("refael","refael@","shaoolzon49"),
            new Order("chaiim","chaim@","chai_taib19"),
            new Order("nechama","nechama@","brand432"),
            new Order("rachel","rachel@","katc54"),
            new Order("yael","yael@","hantke87"),
            new Order("maayan","maayan@","hakablan90"),
            new Order("hadas","hadas@","drok76"),
            new Order("shira","shira@","apisga1"),
            new Order("efrat","efrat@","toravavodah9"),
            new Order("ayala","ayala@","agasi7"),
            new Order("tali","tali@","chaimchaviv35"),
            new Order("dan","dan@","parvshtein89"),
            new Order("gavriel","gavriel@","bergman3")
        };
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
            get { return ++identifyProduct; }
        }

        private static int identifyOrderItem = 100000;
        public static int IdentifyOrderItem
        {
            get { return ++identifyOrderItem; }
        }

        private static int identifyOrder = 100000;
        public static int IdentifyOrder
        {
            get { return ++identifyOrder; }
        }
       

    }
}