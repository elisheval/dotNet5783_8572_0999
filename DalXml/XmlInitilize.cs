using Microsoft.VisualBasic;
using System.Xml.Linq;

namespace Dal;
public class XmlInitilize
{

    public XmlInitilize() {
        #region product initilize
        List<DO.Product> tmpProductList = new(){
            #region assignment of 10 products
            new DO.Product(100001,"drums", 900,DO.Enums.Category.percussions, 5),
            new DO.Product(100002,"Salute",400,DO.Enums.Category.percussions,2),
            new DO.Product(100003,"piano",20000,DO.Enums.Category.keyboards,1),
            new DO.Product(100004,"accordion",3000,DO.Enums.Category.keyboards,10),
            new DO.Product(100005,"harp",15000,DO.Enums.Category.strings,0),
            new DO.Product(100006,"violin",1500,DO.Enums.Category.strings,7),
            new DO.Product(100007,"flute",4000,DO.Enums.Category.exhalation,3),
            new DO.Product(100008,"saxophone",2500,DO.Enums.Category.exhalation,1),
            new DO.Product(100009,"guitar_textbook",70,DO.Enums.Category.additional,30),
            new DO.Product(100010,"accordion_case",300,DO.Enums.Category.additional,10)
            #endregion
        };
        XElement ProductData = new XElement("ProductData");
        for (int i = 0; i < 10; i++)
        {
            XElement product = new("product");
            product.Add(
                new XElement("Id", tmpProductList[i].Id),
                new XElement("Name", tmpProductList[i].Name),
                new XElement("Price", tmpProductList[i].Price.ToString()),
                new XElement("Category", tmpProductList[i].Category),
                new XElement("InStock", tmpProductList[i].InStock.ToString())
                );
            ProductData.Add(product);
        }
        XMLTools.SaveListToXMLElement(ProductData,"Product.xml");
        #endregion

        #region config
        XElement identifies = new XElement("Identifies");
        XElement orderItemId = new XElement("OrderItemId","100000");
        XElement orderId = new XElement("OrderId","100000");
        identifies.Add(orderId, orderItemId);
        XMLTools.SaveListToXMLElement(identifies, "config.xml");
        #endregion

        XElement identifyes = XMLTools.LoadListFromXMLElement("Config.xml");
        int oIdentify = int.Parse(identifyes.Elements().ToList()[0].Value);
        int oItemIdentify = int.Parse(identifyes.Elements().ToList()[1].Value);

        List<DO.Order> tmpOrderList = new(){

            new DO.Order() { CustomerName = "elisheva", CustomerEmail = "ee@", CustomerAddress = "ddd76", OrderDate = new DateTime(2022, 11, 09), ShipDate = null, DeliveryDate = null },
            new DO.Order() { CustomerName = "miri", CustomerEmail = "eemiri22", CustomerAddress = "katz19", OrderDate = new DateTime(2022, 10, 05), ShipDate = null, DeliveryDate = null },
            new DO.Order() { CustomerName = "elis", CustomerEmail = "@", CustomerAddress = "agasu45", OrderDate = new DateTime(2022, 11, 02), ShipDate = null, DeliveryDate = null },
            new DO.Order() { CustomerName = "rina", CustomerEmail = "rina", CustomerAddress = "drouk76", OrderDate = new DateTime(2022, 09, 30), ShipDate = null, DeliveryDate = null },
            new DO.Order() { CustomerName = "tamar", CustomerEmail = "tamar@", CustomerAddress = "drook476", OrderDate = new DateTime(2022, 09, 15), ShipDate = new DateTime(2022, 09, 22), DeliveryDate = null },
            new DO.Order() { CustomerName = "nomi", CustomerEmail = "nomi@", CustomerAddress = "hakablan12", OrderDate = new DateTime(2022, 09, 07), ShipDate = new DateTime(2022, 09, 09), DeliveryDate = null },
            new DO.Order() { CustomerName = "shimon", CustomerEmail = "simon@", CustomerAddress = "agasi8", OrderDate = new DateTime(2022, 11, 09), ShipDate = new DateTime(2022, 09, 09), DeliveryDate = null },
            new DO.Order() { CustomerName = "refael", CustomerEmail = "refael@", CustomerAddress = "shaoolzon49", OrderDate = new DateTime(2022, 08, 22), ShipDate = new DateTime(2022, 09, 30), DeliveryDate = null },
            new DO.Order("chaiim", "chaim@", "chai_taib19", new DateTime(2022, 04, 10), new DateTime(2022, 04, 14), new DateTime(2022, 04, 16)),
            new DO.Order("nechama", "nechama@", "brand432", new DateTime(2022, 04, 03), new DateTime(2022, 05, 10), new DateTime(2022 / 05 / 12)),
            new DO.Order("rachel", "rachel@", "katc54", new DateTime(2022, 03, 05), new DateTime(2022, 05, 10), new DateTime(2022, 05, 15)),
            new DO.Order("yael", "yael@", "hantke87", new DateTime(2021, 11, 11), new DateTime(2022, 11, 11), new DateTime(2021, 11, 11)),
            new DO.Order("maayan", "maayan@", "hakablan90", new DateTime(2021, 11, 11), new DateTime(2022, 11, 11), new DateTime(2021, 11, 11)),
            new DO.Order("hadas", "hadas@", "drok76", new DateTime(2021, 09, 02), new DateTime(2022, 11, 11), new DateTime(2021, 11, 20)),
            new DO.Order("efrat", "efrat@", "toravavodah9", new DateTime(2020, 01, 08), new DateTime(2020, 01, 22), new DateTime(2020, 01, 25)),
            new DO.Order("ayala", "ayala@", "agasi7", new DateTime(2020, 01, 07), new DateTime(2020, 01, 22), new DateTime(2020, 01, 26)),
            new DO.Order("tali", "tali@", "chaimchaviv35", new DateTime(2020, 01, 07), new DateTime(2020, 02, 22), new DateTime(2020, 02, 23)),
            new DO.Order("dan", "dan@", "parvshtein89", new DateTime(2021, 10, 01), new DateTime(2020, 10, 10), new DateTime(2021, 10, 12)),
            new DO.Order("gavriel", "gavriel@", "bergman3", new DateTime(2021, 07, 28), new DateTime(2021, 07, 30), new DateTime(2021, 08, 01)),
            new DO.Order("ayala", "qwerty@", "miriam hanevia", new DateTime(2021, 08, 27), new DateTime(2021, 09, 03), new DateTime(2021, 09, 05))
        };
        List<DO.Order> orders = new List<DO.Order>();
        tmpOrderList.ForEach(x => orders.Add(
            new DO.Order() {
                ID = oIdentify++,
                CustomerName = x.CustomerName,
                CustomerAddress = x.CustomerAddress,
                CustomerEmail = x.CustomerEmail,
                DeliveryDate = x.DeliveryDate,
                ShipDate = x.ShipDate,
                OrderDate = x.OrderDate }));
        identifyes.Elements().ToList()[0].Value = oIdentify.ToString();
        XMLTools.SaveListToXMLElement(identifyes,"Config.xml");
        XMLTools.SaveListToXMLSerializer(orders, "Order.xml");
    }
}
