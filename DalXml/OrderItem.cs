using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using System.Xml.Linq;

internal class OrderItem : IOrderItem
{
    string dirConfig = @"config.xml";
    string dirOrderItem = @"OrderItem.xml";
    public int Add(DO.OrderItem item)
    {
        XElement identifies = XMLTools.LoadListFromXMLElement(dirConfig);
        int orderItemId = (int.Parse(identifies.Elements().ToList()[1].Value)) + 1;
        item.Id = orderItemId;
        List<DO.OrderItem> orderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(dirOrderItem);
        orderItems.Add(item);
        identifies.Elements().ToList()[1].Value = orderItemId.ToString();
        XMLTools.SaveListToXMLElement(identifies, dirConfig);
        XMLTools.SaveListToXMLSerializer(orderItems, dirOrderItem);
        return orderItemId;
    }

    public void Delete(int id)
    {
        try
        {
            List<DO.OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(dirOrderItem);
            orderItemList.RemoveAll(x => x?.Id == id);
            XMLTools.SaveListToXMLSerializer(orderItemList, dirOrderItem);
        }
        catch (DO.XMLFileLoadCreateException ex)
        {
            throw new();
        }
    }

    public IEnumerable<DO.OrderItem?> GetAll(Predicate<DO.OrderItem?>? func = null)
    {
        List<DO.OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(dirOrderItem).FindAll(x => func == null || func(x));
        return orderItemList;
    }

    public DO.OrderItem GetByCondition(Predicate<DO.OrderItem?> func)
    {

        List<DO.OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(dirOrderItem);
        DO.OrderItem? orderItem = orderItemList.FirstOrDefault(x => func(x));
        if (orderItem == null)
        {
            throw new();
        }
        return (DO.OrderItem)orderItem;
    }

    public void Update(DO.OrderItem item)
    {
        try
        {
            List<DO.OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(dirOrderItem);
            orderItemList.RemoveAll(x => x?.Id == item.Id);
            orderItemList.Add(item);
            XMLTools.SaveListToXMLSerializer(orderItemList, dirOrderItem);
        }
        catch (DO.XMLFileLoadCreateException ex)
        {
            throw new();
        }
    }
}
