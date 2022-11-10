﻿using DO;
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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="myProduct"></param>
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

        Random rand = new Random();
        
        OrderItem[] tmpOrderItemArr ={new OrderItem(100001,100001,900,rand.Next(1,5)),
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
        };
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