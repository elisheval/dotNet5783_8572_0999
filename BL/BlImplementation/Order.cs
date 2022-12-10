using BlApi;
using BO;
using Dal;
using System.ComponentModel;
using System.Linq.Expressions;
using static BO.Enums;

namespace BlImplementation;
internal class Order : IOrder
{
    private DalApi.IDal _dal = new DalList();
    #region total price
    /// <param name="orderId">get id of order</param>
    /// <summary>
    /// inner method, calculate the total price of the getter order
    /// </summary>
    /// <returns>the total price</returns>
    private double _totalPrice(int orderId)
    {
        double totalPrice = 0;
        IEnumerable<DO.OrderItem> orders = _dal.OrderItem.getOrderItemsArrWithSpecificOrderId(orderId);
        foreach (DO.OrderItem orderItem in orders)
        {
            totalPrice += orderItem.Price * orderItem.Amount;
        }
        return totalPrice;
    }
    #endregion

    #region order status
    /// <param name="order">get order object</param>
    /// <summary>
    /// check the stage that the order in
    /// </summary>
    /// <returns>return the status according to the stage</returns>
    private BO.Enums.OrderStatus _orderStatus(DO.Order order)
    {
        BO.Enums.OrderStatus os;
        if (order.DeliveryDate != DateTime.MinValue) os = BO.Enums.OrderStatus.Supplied;
        else if (order.ShipDate != DateTime.MinValue) os = BO.Enums.OrderStatus.Sent;
        else os = BO.Enums.OrderStatus.Approved;
        return os;
    }
    #endregion

    #region GetAllOrders

    /// <summary>
    /// get list of all the orders and convert the orders to logic entitie objects 
    /// </summary>
    /// <returns>return list with the orders</returns>
    public IEnumerable<BO.OrderForList> GetAllOrders()
    {
        IEnumerable<DO.Order> ordersFromDo = _dal.Order.GetAll();
        List<BO.OrderForList> ordersForList = new List<BO.OrderForList>();
        foreach (DO.Order order in ordersFromDo)
        {
            BO.OrderForList orderForListToAdd = new BO.OrderForList() {
                Id = order.ID,
                CustomerName = order.CustomerName,
                OrderStatus = _orderStatus(order),
                ItemsAmount = _dal.OrderItem.getOrderItemsArrWithSpecificOrderId(order.ID).Count(),
                TotalPrice = _totalPrice(order.ID)
            };
            ordersForList.Add(orderForListToAdd);
        }
        return ordersForList;
    }
    #endregion

    #region GetOrderById
    /// <param name="orderId">get id of exising order</param>
    /// <summary>
    /// take order from the data entity according to the orderId and convert to logic entity object
    /// </summary>
    /// <returns>the convert object</returns>
    /// <exception cref="BO.InvalidValueException">if the orderId not valid</exception>
    /// <exception cref="BO.NoFoundItemExceptions">if there is no order with that id</exception>
    public BO.Order GetOrderById(int orderId)
    {
        if (orderId <= 0)
        {
            throw new BO.InvalidValueException("invalid order id");
        }
        try
        {
            DO.Order orderFromDo = _dal.Order.Get(orderId);
            IEnumerable<DO.OrderItem> orderItemsFromDo = _dal.OrderItem.getOrderItemsArrWithSpecificOrderId(orderId);
            List<BO.OrderItem> ordersItems = new List<BO.OrderItem>();
            foreach (DO.OrderItem orderItem in orderItemsFromDo)
            {
                BO.OrderItem orderToAdd = new BO.OrderItem()
                {
                    ProductId = orderItem.Id,
                    ProductName = _dal.Product.Get(orderItem.ProductId).Name,
                    Price =orderItem.Price,
                    AmountInCart=orderItem.Amount,
                    TotalPriceForItem= orderItem.Price* orderItem.Amount
                };
                ordersItems.Add(orderToAdd);
            }

            BO.Order order = new BO.Order()
            {
                Id = orderId,
                CustomerName = orderFromDo.CustomerName,
                CustomerEmail = orderFromDo.CustomerEmail,
                CustomerAddress = orderFromDo.CustomerAddress,
                OrderStatus = _orderStatus(orderFromDo),
                OrderDate = orderFromDo.OrderDate,
                ShipDate = orderFromDo.ShipDate,
                DeliveryDate = orderFromDo.DeliveryDate,
                OrderItemList = ordersItems,
                TotalOrderPrice = _totalPrice(orderId)
            };
            return order;
        }
        catch(DO.NoFoundItemExceptions ex)
        {
            throw new BO.NoFoundItemExceptions("no found order with this id", ex);
        }
    }
    #endregion

    #region OrderShippingUpdate
    /// <param name="orderId">get id of order</param>
    /// <summary>
    /// update the sentDate to be now
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.NoFoundItemExceptions"></exception>
    /// <exception cref="BO.OrderAlreadySend"></exception>
    public BO.Order OrderShippingUpdate(int orderId)
    {
        DO.Order dalOrder = new DO.Order();
        try
        {
            dalOrder = _dal.Order.Get(orderId);//get the details order
        }
        catch (DO.NoFoundItemExceptions exe)//check if exist
        {
            throw new BO.NoFoundItemExceptions("no order exist with this ID", exe);
        }
        if (dalOrder.ShipDate != DateTime.MinValue)// check if did not send
        {
            throw new BO.InvalidDateChange("this order already sent");
        }

        List<DO.OrderItem> dalOrderItems = (List<DO.OrderItem>)_dal.OrderItem.getOrderItemsArrWithSpecificOrderId(orderId);
        List<BO.OrderItem> blOrderItems = new List<BO.OrderItem>();
        double totalPrice = 0;
        foreach (DO.OrderItem item in dalOrderItems)
        {
            BO.OrderItem OrderItemToPush = new BO.OrderItem();
            OrderItemToPush.Id = item.Id;
            OrderItemToPush.ProductId = item.ProductId;
            OrderItemToPush.ProductName = _dal.Product.Get(item.ProductId).Name;
            OrderItemToPush.Price = item.Price;
            OrderItemToPush.AmountInCart = item.Amount;
            OrderItemToPush.TotalPriceForItem = item.Price * item.Amount;
            totalPrice += OrderItemToPush.TotalPriceForItem;
            blOrderItems.Add(OrderItemToPush);
        }

        BO.Order blOrder = new BO.Order();
        blOrder.Id = dalOrder.ID;
        blOrder.CustomerName = dalOrder.CustomerName;
        blOrder.CustomerEmail = dalOrder.CustomerEmail;
        blOrder.CustomerAddress = dalOrder.CustomerAddress;
        blOrder.OrderStatus = BO.Enums.OrderStatus.Sent;
        blOrder.OrderDate = dalOrder.OrderDate;
        blOrder.DeliveryDate = dalOrder.DeliveryDate;
        blOrder.OrderItemList = blOrderItems;
        dalOrder.ShipDate = DateTime.Now;
        blOrder.ShipDate = dalOrder.ShipDate;
        blOrder.TotalOrderPrice= totalPrice;
        _dal.Order.Update(dalOrder);
        return blOrder;
    }
    #endregion

    #region OrderDeliveryUpdate
    /// <summary>
    /// update the suplies date to be now
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="BO.NoFoundItemExceptions"></exception>
    /// <exception cref="BO.OrderAlreadyDelivery"></exception>
    public BO.Order OrderDeliveryUpdate(int orderId)
    {
        DO.Order dalOrder = new DO.Order();
        try
        {
            dalOrder = _dal.Order.Get(orderId);//get the order details
        }
        catch (DO.NoFoundItemExceptions exe)//check if exist
        {
            throw new BO.NoFoundItemExceptions("no order exist with this ID", exe);
        }
        if (dalOrder.DeliveryDate != DateTime.MinValue)// 
        {
            throw new BO.InvalidDateChange("this order already delivery");
        }
        if (dalOrder.ShipDate == DateTime.MinValue)
            throw new BO.InvalidDateChange("the order cannot delivery before send");
        IEnumerable<DO.OrderItem> dalOrderItems = _dal.OrderItem.getOrderItemsArrWithSpecificOrderId(orderId);
        List<BO.OrderItem> blOrderItems = new List<BO.OrderItem>();
        double totalPrice = 0;
        foreach (DO.OrderItem item in dalOrderItems)
        {
            BO.OrderItem OrderItemToPush = new BO.OrderItem();
            OrderItemToPush.Id = item.Id;
            OrderItemToPush.ProductId = item.ProductId;
            OrderItemToPush.ProductName = _dal.Product.Get(item.ProductId).Name;
            OrderItemToPush.Price = item.Price;
            OrderItemToPush.AmountInCart = item.Amount;
            OrderItemToPush.TotalPriceForItem = item.Price * item.Amount;
            totalPrice += OrderItemToPush.TotalPriceForItem;
            blOrderItems.Add(OrderItemToPush);
        }

        BO.Order blOrder = new BO.Order();
        blOrder.Id = dalOrder.ID;
        blOrder.CustomerName = dalOrder.CustomerName;
        blOrder.CustomerEmail = dalOrder.CustomerEmail;
        blOrder.CustomerAddress = dalOrder.CustomerAddress;
        blOrder.OrderStatus = BO.Enums.OrderStatus.Sent;
        blOrder.OrderDate = dalOrder.OrderDate;
        blOrder.ShipDate = dalOrder.ShipDate;
        blOrder.OrderItemList = blOrderItems;
        dalOrder.DeliveryDate = DateTime.Now;
        blOrder.DeliveryDate = dalOrder.DeliveryDate;
        blOrder.TotalOrderPrice=totalPrice;
        _dal.Order.Update(dalOrder);
        return blOrder;
    }
    #endregion

    #region Ordertracking
    /// <summary>
    /// check what is the stage of the sent order
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="BO.InvalidValueException"></exception>
    /// <exception cref="BO.NoFoundItemExceptions"></exception>
    public BO.OrderTracking Ordertracking(int orderId)
    {
        if (orderId <= 0)
            throw new BO.InvalidValueException("invalid order id");
        DO.Order dalOrder = new DO.Order();
        try
        {
            dalOrder = _dal.Order.Get(orderId);//מקבל את פרטי ההזמנה  
        }
        catch (DO.NoFoundItemExceptions exe)//בודק שההזמנה קיימת
        {
            throw new BO.NoFoundItemExceptions("no order exist with this ID", exe);
        }
        BO.OrderTracking orderTracking = new BO.OrderTracking { };
        orderTracking.DetailOrderStatuses = new List<(DateTime, string)> { };
        OrderStatus tracking = 0;
        if (dalOrder.OrderDate != DateTime.MinValue)
        {
            orderTracking.DetailOrderStatuses.Add((dalOrder.OrderDate, "order created"));
        }
        if (dalOrder.ShipDate != DateTime.MinValue)
        {
            tracking = OrderStatus.Sent;
            orderTracking.DetailOrderStatuses.Add((dalOrder.ShipDate, "order shipped"));
        }
        if (dalOrder.DeliveryDate != DateTime.MinValue)
        {
            tracking=OrderStatus.Supplied;
            orderTracking.DetailOrderStatuses.Add((dalOrder.DeliveryDate, "order delivred"));
        }
        orderTracking.OrderStatus = tracking;
        orderTracking.Id = dalOrder.ID;
        return orderTracking;
    }
    #endregion

    #region UpdateOrder bonus
    /// <param name="orderId">get order of id</param>
    /// <param name="productId">get id of product to add, delete or change amount</param>
    /// <param name="amount">get amount</param>
    /// <summary>
    /// the manager can add, delete or change amount of product in confirm order
    /// </summary>
    /// <exception cref="BO.InvalidValueException"></exception>
    /// <exception cref="BO.NoAccessToSentOrder"></exception>
    /// <exception cref="BO.NoFoundItemExceptions"></exception>
    public void UpdateOrder(int orderId, int productId, int amount)
    {
        try
        {
            if (orderId <= 0)
                throw new BO.InvalidValueException("invalid order id");
            DO.Order dalOrder = new DO.Order();
            dalOrder = _dal.Order.Get(orderId);
            if (dalOrder.ShipDate != DateTime.MinValue)//if the order sent the manager cannot change it
                throw new BO.NoAccessToSentOrder("the order already sent, you can't change the date");
            if (amount < 0)//invalid
                throw new BO.InvalidValueException("invalid amount of product");
            if (amount == 0)//This is a sign that the manager wants to delete
            {
                DO.Product p=_dal.Product.Get(productId);
                DO.OrderItem orderitem= _dal.OrderItem.GetByProductAndOrderIds(productId, orderId);
                p.InStock += orderitem.Amount;
                _dal.OrderItem.Delete(orderitem.Id);
                _dal.Product.Update(p);

            }
            else             
            {
                IEnumerable<DO.OrderItem> OrderItems = _dal.OrderItem.GetAll();
                bool flag = false;
                foreach (DO.OrderItem oi in OrderItems)//look for the order item,
                {
                    if (oi.ProductId == productId && oi.OrderId==orderId)//if the product exist- change the amount to be the getter amount
                    {
                        flag = true;
                        if (oi.Amount != amount)
                        {
                            DO.Product p = _dal.Product.Get(productId);
                            DO.OrderItem OItoChange = new DO.OrderItem()
                            {
                                OrderId = oi.OrderId,
                                Id = oi.Id,
                                Price = oi.Price,
                                ProductId = oi.ProductId
                            };
                            if (p.InStock < amount)
                            {
                                OItoChange.Amount =oi.Amount+ p.InStock;
                                p.InStock = 0;
                            }
                            else
                            {
                                OItoChange.Amount = amount;
                                p.InStock -= amount - oi.Amount;
                            }
                            _dal.Product.Update(p);
                            _dal.OrderItem.Update(OItoChange);
                        }
                    }
                }
                if (!flag)//if not exist enter the product to the order
                {
                    DO.Product p = _dal.Product.Get(productId);
                    if (p.InStock < amount)
                    {
                        p.InStock = 0;
                        amount=p.InStock;
                    }
                    else
                    {
                        p.InStock -= amount;
                    }
                    DO.OrderItem d1 = new DO.OrderItem(productId, orderId, _dal.Product.Get(productId).Price, amount);
                    _dal.OrderItem.Add(d1);
                    _dal.Product.Update(p);

                }
            }
        }
        catch (DO.NoFoundItemExceptions exe)
        {
            throw new BO.NoFoundItemExceptions(exe.Message, exe);
        }

    }
    #endregion
}