using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

internal class OrderItem : IOrderItem
{
    string dirConfig = @"config.xml";
    string dirOrderItem = @"OrderItem.xml";
    #region Add
    /// <param name="item">get orderItem to add</param>
    /// <summary>
    /// get the orderItemsList file from the xmlOrderItem file, add item to the list and save the file
    /// </summary>
    /// <returns>the id of the new item</returns>
    public int Add(DO.OrderItem item)
    {
        try
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
        catch (DO.XMLFileLoadCreateException ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Delete
    /// <param name="id">get id of orderItem to delete</param>
    /// <summary>
    /// get the orderItemsList file from the xmlOrderItem file, delete item from the list and save the file
    /// </summary>
    /// <exception cref="DO.NoFoundItemExceptions">if the id doesnt exist</exception>
    public void Delete(int id)
    {
        try
        {
            List<DO.OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(dirOrderItem);
            if (orderItemList.RemoveAll(x => x?.Id == id) == 0)
                throw new DO.NoFoundItemExceptions("no orderItem found to delete with this ID");
            XMLTools.SaveListToXMLSerializer(orderItemList, dirOrderItem);
        }
        catch (DO.XMLFileLoadCreateException ex)
        {
            throw ex;
        }
    }
    #endregion

    #region GetAll
    /// <summary>
    /// get orderItemList from the xmlOrderItem file  
    /// </summary>
    /// <param name="func">filter the orderList by func if func!=null</param>
    /// <returns>the orderItemList</returns>
    public IEnumerable<DO.OrderItem?> GetAll(Predicate<DO.OrderItem?>? func = null)
    {
        try
        {
            List<DO.OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(dirOrderItem).FindAll(x => func == null || func(x));
            return orderItemList;
        }
        catch (DO.XMLFileLoadCreateException ex)
        {
            throw ex;
        }
    }
    #endregion

    #region GetByCondition
    /// <summary>
    /// get the orderItemList from the xmlOrderItem file and looking for 1 orderItem who stands in the condition
    /// </summary>
    /// <param name="func">filtering predicate</param>
    /// <returns>filtering orderItemList</returns>
    /// <exception cref="NoFoundItemExceptions">if no one stands in the condition</exception>
    public DO.OrderItem GetByCondition(Predicate<DO.OrderItem?> func)
    {
        try
        {
            List<DO.OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(dirOrderItem);
            DO.OrderItem? orderItem = orderItemList.FirstOrDefault(x => func(x));
            if (orderItem == null)
            {
                throw new NoFoundItemExceptions("no found order item with this condition");
            }
            return (DO.OrderItem)orderItem;
        }
        catch (DO.XMLFileLoadCreateException ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Update
    /// <param name="item">get item to update</param>
    /// <summary>
    /// get the orderItemsList file from the xmlOrderItem file, update item in the list and save the file
    /// </summary>
    /// <exception cref="NoFoundItemExceptions">if item to update doesnt exist</exception>

    public void Update(DO.OrderItem item)
    {
        try
        {
            List<DO.OrderItem?> orderItemList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(dirOrderItem);
            if (orderItemList.RemoveAll(x => x?.Id == item.Id) == 0)
                throw new NoFoundItemExceptions("no orderItem found to update with this ID");
            orderItemList.Add(item);
            XMLTools.SaveListToXMLSerializer(orderItemList, dirOrderItem);
        }
        catch (DO.XMLFileLoadCreateException ex)
        {
            throw ex;
        }
    }
    #endregion
}
