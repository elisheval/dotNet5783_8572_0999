using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using System.Xml.Linq;

internal class Order : IOrder
{
    string dirConfig = "config.xml";
    string dirOrder = "order.xml";

    public int Add(DO.Order item)
    {
        XElement identifies = XMLTools.LoadListFromXMLElement(dirConfig);
        int orderId = (int.Parse(identifies.Elements().ToList()[0].Value))+1;
        item.ID=orderId;
        List<DO.Order> orders = XMLTools.LoadListFromXMLSerializer<DO.Order>(dirOrder);
        orders.Add(item);
        identifies.Elements().ToList()[0].Value = orderId.ToString();
        XMLTools.SaveListToXMLElement(identifies, dirConfig);
        XMLTools.SaveListToXMLSerializer(orders, dirOrder);
        return orderId;
    }

    public void Delete(int id)
    {
        try
        {
            List<DO.Order?> orderList = XMLTools.LoadListFromXMLSerializer<DO.Order?>(dirOrder);
            orderList.RemoveAll(x => x?.ID == id);
            XMLTools.SaveListToXMLSerializer(orderList, dirOrder);
        }
        catch (DO.XMLFileLoadCreateException ex)
        {
            throw new();
        }
    }

    public IEnumerable<DO.Order?> GetAll(Predicate<DO.Order?>? func = null)
    {
        List<DO.Order?> orderList = XMLTools.LoadListFromXMLSerializer<DO.Order?>(dirOrder).FindAll(x => func == null || func(x));
        return orderList;
    }

    public DO.Order GetByCondition(Predicate<DO.Order?> func)
    {
        List<DO.Order> orderList = XMLTools.LoadListFromXMLSerializer<DO.Order>(dirOrder);
        DO.Order? order = orderList.FirstOrDefault(x => func(x));
        if (order == null)
        {
            throw new();
        }
        return (DO.Order)order;
    }

    public void Update(DO.Order item)
    {
        try
        {
            List<DO.Order?> orderList = XMLTools.LoadListFromXMLSerializer<DO.Order?>(dirOrder);
            orderList.RemoveAll(x => x?.ID == item.ID);
            orderList.Add(item);
            XMLTools.SaveListToXMLSerializer(orderList, dirOrder);
        }
        catch (DO.XMLFileLoadCreateException ex)
        {
            throw new();
        }
    }
}