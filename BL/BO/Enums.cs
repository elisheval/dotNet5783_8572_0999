using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Enums
{
    #region entity
    /// <summary>
    /// The Entity options to navigate
    /// </summary>
    public enum Entity
    {
        exit,
        Product,
        Order,
        Cart
    }
    #endregion

    #region category
    /// <summary>
    /// The item's categories in the store
    /// </summary>
    public enum Category
    {
        percussions,//כלי הקשה
        keyboards,//כלי מקלדת
        exhalation,//כלי נשיפה
        strings,//כלי מיתר
        additional//נלווים 
    }
    #endregion

    #region OrderStatus
    public enum OrderStatus
    {
        Approved,
        Sent,
        Supplied
    }
    #endregion

    #region order method
    public enum OrderMethod
    {
        Exit,
        GetAllOrders,
        GetOrderDetById,
        UpdateOrderStatusToSend,
        UpdateOrderStatusToSupplied,
        OrderTracking
    }
    #endregion

    #region cart method
    public enum CartMethod
    {
        Exit=0,
        AddProduct,
        UpdateAmount,
        OrderConfirmation
    }
    #endregion

    #region product method
    public enum ProductMethods
    {
        Exit,
        GetAll,
        GetById,
        GetProductItemById,
        Add,
        Delete,
        Update
    }
    #endregion
}
