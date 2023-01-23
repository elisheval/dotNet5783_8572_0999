using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;

internal class Order : IOrder
{
    string OrderPath = @"Order.xml";
    public int Add(DO.Order item)
    {
        return 1;
    }

    public void Delete(int id)
    {
        try
        {
            List<DO.Order?> orderList = XMLTools.LoadListFromXMLSerializer<DO.Order?>(OrderPath);
            orderList.RemoveAll(x => x?.ID == id);
            XMLTools.SaveListToXMLSerializer(orderList, OrderPath);
        }
        catch (DO.XMLFileLoadCreateException ex)
        {
            throw new();
        }
    }

    public IEnumerable<DO.Order?> GetAll(Predicate<DO.Order?>? func = null)
    {
        List<DO.Order?> orderList = XMLTools.LoadListFromXMLSerializer<DO.Order?>(OrderPath).FindAll(x => func == null||func(x));
        return orderList;
    }

    public DO.Order GetByCondition(Predicate<DO.Order?> func)
    {
        List<DO.Order> orderList = XMLTools.LoadListFromXMLSerializer<DO.Order>(OrderPath);
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
            List<DO.Order?> orderList = XMLTools.LoadListFromXMLSerializer<DO.Order?>(OrderPath);
            orderList.RemoveAll(x => x?.ID == item.ID);
            orderList.Add(item);
            XMLTools.SaveListToXMLSerializer(orderList, OrderPath);
        }
        catch (DO.XMLFileLoadCreateException ex)
        {
            throw new();
        }
    }
}
