using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;

public class OrderItem : IOrderItem
{
    string OrderItemPath = @"OrderItem.xml";
    public int Add(DO.OrderItem item)
    {

        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        try
        {
            List<DO.OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(OrderItemPath);
            orderItemList.RemoveAll(x => x?.Id == id);
            XMLTools.SaveListToXMLSerializer(orderItemList, OrderItemPath);
        }
        catch (DO.XMLFileLoadCreateException ex)
        {
            throw new();
        }
    }

    public IEnumerable<DO.OrderItem?> GetAll(Predicate<DO.OrderItem?>? func = null)
    {
        List<DO.OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(OrderItemPath).FindAll(x => func(x) == null || func(x));
        return orderItemList;
    }

    public DO.OrderItem GetByCondition(Predicate<DO.OrderItem?> func)
    {

        List<DO.OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(OrderItemPath);
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
            List<DO.OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(OrderItemPath);
            orderItemList.RemoveAll(x => x?.Id == item.Id);
            orderItemList.Add(item);
            XMLTools.SaveListToXMLSerializer(orderItemList, OrderItemPath);
        }
        catch (DO.XMLFileLoadCreateException ex)
        {
            throw new();
        }
    }
}
