using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

internal class Order : IOrder
{
    string dirConfig = "config.xml";
    string dirOrder = "order.xml";

    #region Add
    /// <param name="item">order to add</param>
    /// <summary>
    /// get the orderList from the xmlOrder file, add item to the list and save the xml file
    /// </summary>
    /// <returns>the id of the item</returns>
    /// <exception cref="DO.XMLFileLoadCreateException">if does'nt success to load the xmlConfig file or the xmlOrder file</exception>
    public int Add(DO.Order item)
    {
        try
        {
            XElement identifies = XMLTools.LoadListFromXMLElement(dirConfig);
            int orderId = (int.Parse(identifies.Elements().ToList()[0].Value)) + 1;
            item.ID = orderId;
            List<DO.Order> orders = XMLTools.LoadListFromXMLSerializer<DO.Order>(dirOrder);
            orders.Add(item);
            identifies.Elements().ToList()[0].Value = orderId.ToString();
            XMLTools.SaveListToXMLElement(identifies, dirConfig);
            XMLTools.SaveListToXMLSerializer(orders, dirOrder);
            return orderId;
        }
        catch (DO.XMLFileLoadCreateException ex)
        {
            throw new DO.XMLFileLoadCreateException(ex.Message);
        }
    }
    #endregion

    #region Delete
    /// <param name="id">get id of order to delete</param>
    /// <summary>
    /// get the orderList from the xmlOrder file, delete the item that his id=id from the list and save the xml file
    /// </summary>
    /// <exception cref="DO.NoFoundItemExceptions">if the order does'nt exist </exception>
    public void Delete(int id)
    {
        try
        {
            List<DO.Order?> orderList = XMLTools.LoadListFromXMLSerializer<DO.Order?>(dirOrder);
            if (orderList.RemoveAll(x => x?.ID == id) == 0)
                throw new DO.NoFoundItemExceptions("no order found to delete with this ID");
            XMLTools.SaveListToXMLSerializer(orderList, dirOrder);
        }
        catch (DO.XMLFileLoadCreateException ex)
        {
            throw ex;
        }
    }
    #endregion

    #region GetAll
    /// <summary>
    /// get orderList from the xmlOrder file  
    /// </summary>
    /// <param name="func"> filter the orderList by func if func!=null </param>
    /// <returns>the orderList</returns>
    public IEnumerable<DO.Order?> GetAll(Predicate<DO.Order?>? func = null)
    {
        try
        {
            List<DO.Order?> orderList = XMLTools.LoadListFromXMLSerializer<DO.Order?>(dirOrder).FindAll(x => func == null || func(x));
            return orderList;
        }
        catch (DO.XMLFileLoadCreateException ex)
        {
            throw ex;
        }
    }
    #endregion

    #region GetByCondition
    /// <param name="func"> filtering predicate</param>
    /// <summary>
    /// get the orderList from the xmlOrder file and looking for 1 order who stans in the condition
    /// </summary>
    /// <returns>filtering orderList</returns>
    /// <exception cref="NoFoundItemExceptions"> if no one stands in the condition</exception>
    public DO.Order GetByCondition(Predicate<DO.Order?> func)
    {
        try
        {
            List<DO.Order> orderList = XMLTools.LoadListFromXMLSerializer<DO.Order>(dirOrder);
            DO.Order? order = orderList.FirstOrDefault(x => func(x));
            if (order?.ID == 0)
            {
                throw new NoFoundItemExceptions("no found order with this condition");
            }
            return (DO.Order)order!;

        }
        catch (DO.XMLFileLoadCreateException ex)
        {
            throw ex;
        }

    }
    #endregion

    #region Update
    /// <param name="item">item to update</param>
    /// <summary>
    /// get the orderList from the xmlOrder file, update the place in the list that his id = item's id from the list and save the xml file
    /// </summary>
    /// <exception cref="NoFoundItemExceptions"> if item to update not exist</exception>
    public void Update(DO.Order item)
    {
        try
        {
            List<DO.Order?> orderList = XMLTools.LoadListFromXMLSerializer<DO.Order?>(dirOrder);
            if (orderList.RemoveAll(x => x?.ID == item.ID) == 0)
                throw new NoFoundItemExceptions("no order found to update with this ID");
            orderList.Add(item);
            XMLTools.SaveListToXMLSerializer(orderList, dirOrder);
        }
        catch (DO.XMLFileLoadCreateException ex)
        {
            throw ex;
        }
    }
    #endregion
}