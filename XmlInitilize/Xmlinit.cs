using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualBasic;
namespace Dal;
public class XmlInitilize
{
    public XmlInitilize()
    {
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
        XMLTools.SaveListToXMLElement(ProductData, "Product.xml");
        XElement identifies = new XElement("Identifies");
        XElement orderItemId = new XElement("OrderItemId", "100000");
        XElement orderId = new XElement("OrderId", "100000");
        identifies.Add(orderId, orderItemId);
        XMLTools.SaveListToXMLElement(identifies, "config.xml");
    }
}
