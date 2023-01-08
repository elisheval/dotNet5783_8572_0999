using BlApi;
using System.Diagnostics;

namespace BlImplementation;
internal class Order : IOrder
{
    DalApi.IDal? _dal = DalApi.Factory.Get();

    #region total price
    /// <param name="orderId">get id of order</param>
    /// <summary>
    /// inner method, calculate the total price of the getter order
    /// </summary>
    /// <returns>the total price</returns>
    private double _totalPrice(int orderId)
    {
        double totalPrice = 0;
        if (_dal == null) throw new BO.NoAccessToDataException("no access to data");
        IEnumerable<DO.OrderItem?> orders = _dal.OrderItem.GetAll((x) => x != null && x?.OrderId == orderId);
        totalPrice = orders.Where(oi => oi != null).Sum(x => (x?.Price ?? 0) * (x?.Amount ?? 0));
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
        if (order.DeliveryDate != null) os = BO.Enums.OrderStatus.Supplied;
        else if (order.ShipDate != null) os = BO.Enums.OrderStatus.Sent;
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
        if (_dal == null) throw new BO.NoAccessToDataException("no access to data");
        IEnumerable<DO.Order?> ordersFromDo = _dal.Order.GetAll();
        return from order in ordersFromDo
               where order != null
               select new BO.OrderForList() {

                   Id = order?.ID ?? 0,
                   CustomerName = order?.CustomerName,
                   OrderStatus = _orderStatus(order!.Value),
                   ItemsAmount = _dal.OrderItem.GetAll(x => x != null && order?.ID == x?.OrderId).Count(),
                   TotalPrice = _totalPrice(order?.ID ?? 0)
               };

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
            if (_dal == null) throw new BO.NoAccessToDataException("no access to data");
            DO.Order orderFromDo = _dal.Order.GetByCondition((x) => x != null && orderId == x?.ID);
            IEnumerable<DO.OrderItem?> orderItemsFromDo = _dal.OrderItem.GetAll((x) => x != null && orderId == x?.OrderId);
            List<BO.OrderItem> ordersItems = orderItemsFromDo.Where(order => order != null).Select(orderItem => new BO.OrderItem
            {
                ProductId = orderItem?.Id ?? 0,
                ProductName = _dal.Product.GetByCondition(x => x != null && orderItem?.ProductId == x?.Id).Name,
                Price = orderItem?.Price ?? 0,
                AmountInCart = orderItem?.Amount ?? 0,
                TotalPriceForItem = (orderItem?.Price ??0)*( orderItem?.Amount ?? 0)
            }).ToList();
            BO.Order order = new()
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
        catch (DO.NoFoundItemExceptions ex)
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
        DO.Order dalOrder = new();
        try
        {
            if (_dal == null) throw new BO.NoAccessToDataException("no access to data");
            dalOrder = _dal.Order.GetByCondition(x => x != null && x?.ID == orderId);//get the details order
        }
        catch (DO.NoFoundItemExceptions exe)//check if exist
        {
            throw new BO.NoFoundItemExceptions("no order exist with this ID", exe);
        }
        if (dalOrder.ShipDate != null)// check if did not send
        {
            throw new BO.InvalidDateChange("this order already sent");
        }

        IEnumerable<DO.OrderItem?> dalOrderItems = _dal.OrderItem.GetAll(x => x != null && x?.OrderId == dalOrder.ID);
        List<BO.OrderItem> blOrderItems = dalOrderItems.Where(x => x != null).Select(item =>
            new BO.OrderItem()
            {
                Id = item?.Id ?? 0,
                ProductId = item?.ProductId ?? 0,
                ProductName = _dal.Product.GetByCondition(x => x != null && x?.Id == item?.ProductId).Name,
                Price = item?.Price ?? 0,
                AmountInCart = item?.Amount ?? 0,
                TotalPriceForItem = item?.Price * item?.Amount ?? 0
            }).ToList();
        var totalPrice = blOrderItems.Where(x => x != null).Sum(x => x?.TotalPriceForItem ?? 0);
        dalOrder.ShipDate = DateTime.Now;
        BO.Order blOrder = new() {
            Id = dalOrder.ID,
            CustomerName = dalOrder.CustomerName,
            CustomerEmail = dalOrder.CustomerEmail,
            CustomerAddress = dalOrder.CustomerAddress,
            OrderStatus = BO.Enums.OrderStatus.Sent,
            OrderDate = dalOrder.OrderDate,
            DeliveryDate = dalOrder.DeliveryDate,
            OrderItemList = blOrderItems,
            ShipDate = dalOrder.ShipDate,
            TotalOrderPrice = totalPrice
        };
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
        DO.Order dalOrder = new();
        try
        {

            if (_dal == null) throw new BO.NoAccessToDataException("no access to data");
            dalOrder = _dal.Order.GetByCondition((x) => x != null && orderId == x?.ID);//get the order details
            if (dalOrder.DeliveryDate != null)// 
            {
                throw new BO.InvalidDateChange("this order already delivery");
            }
            if (dalOrder.ShipDate == null)
                throw new BO.InvalidDateChange("the order cannot delivery before send");
            IEnumerable<DO.OrderItem?> dalOrderItems = _dal.OrderItem.GetAll(x => x != null && x?.OrderId == orderId);
            List<BO.OrderItem> blOrderItems = dalOrderItems.Where(x => x != null).Select(item =>
            new BO.OrderItem()
            {
                Id = item?.Id ?? 0,
                ProductId = item?.ProductId ?? 0,
                ProductName = _dal.Product.GetByCondition(x => x != null && x?.Id == item?.ProductId).Name,
                Price = item?.Price ?? 0,
                AmountInCart = item?.Amount ?? 0,
                TotalPriceForItem = item?.Price * item?.Amount ?? 0
            }).ToList();
            var totalPrice = blOrderItems.Where(x => x != null).Sum(x => x?.TotalPriceForItem ?? 0);
            dalOrder.DeliveryDate = DateTime.Now;
            BO.Order blOrder = new()
            {
                Id = dalOrder.ID,
                CustomerName = dalOrder.CustomerName,
                CustomerEmail = dalOrder.CustomerEmail,
                CustomerAddress = dalOrder.CustomerAddress,
                OrderStatus = BO.Enums.OrderStatus.Sent,
                OrderDate = dalOrder.OrderDate,
                ShipDate = dalOrder.ShipDate,
                OrderItemList = blOrderItems,
                DeliveryDate = dalOrder.DeliveryDate,
                TotalOrderPrice = totalPrice
            };
            _dal.Order.Update(dalOrder);
            return blOrder;

        }
        catch (DO.NoFoundItemExceptions exe)//check if exist
        {
            throw new BO.NoFoundItemExceptions("no order exist with this ID", exe);
        }
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
        DO.Order dalOrder = new();
        try
        {
            if (_dal == null) throw new BO.NoAccessToDataException("no access to data");
            dalOrder = _dal.Order.GetByCondition(x => x != null && x?.ID == orderId);//מקבל את פרטי ההזמנה  
        }
        catch (DO.NoFoundItemExceptions exe)//בודק שההזמנה קיימת
        {
            throw new BO.NoFoundItemExceptions("no order exist with this ID", exe);
        }
        BO.OrderTracking orderTracking = new();
        orderTracking.DetailOrderStatuses = new List<(DateTime?, string)?>();
        BO.Enums.OrderStatus tracking = 0;
        if (dalOrder.OrderDate != null)
        {
            orderTracking.DetailOrderStatuses.Add((dalOrder.OrderDate, "order created"));
        }
        if (dalOrder.ShipDate != null)
        {
            tracking = BO.Enums.OrderStatus.Sent;
            orderTracking.DetailOrderStatuses.Add((dalOrder.ShipDate, "order shipped"));
        }
        if (dalOrder.DeliveryDate != null)
        {
            tracking = BO.Enums.OrderStatus.Supplied;
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
            if (_dal == null) throw new BO.NoAccessToDataException("no access to data");
            DO.Order dalOrder = new DO.Order();
            dalOrder = _dal.Order.GetByCondition(x => x != null && x?.ID == orderId);
            if (dalOrder.ShipDate != null)//if the order sent the manager cannot change it
                throw new BO.NoAccessToSentOrder("the order already sent, you can't change the date");
            if (amount < 0)//invalid
                throw new BO.InvalidValueException("invalid amount of product");
            if (amount == 0)//This is a sign that the manager wants to delete
            {
                DO.Product p = _dal.Product.GetByCondition(x => x != null && x?.Id == productId);
                DO.OrderItem orderitem = _dal.OrderItem.GetByCondition(x => x != null && x?.ProductId == productId && x?.OrderId == orderId);
                p.InStock += orderitem.Amount;
                _dal.OrderItem.Delete(orderitem.Id);
                _dal.Product.Update(p);
            }
            else
            {
                IEnumerable<DO.OrderItem?> OrderItems = _dal.OrderItem.GetAll();
                var orderItem = (from oi in OrderItems
                                where oi != null
                                where oi?.ProductId == productId && oi?.OrderId == orderId
                                select oi).FirstOrDefault();
                if (orderItem != null) {
                    if (orderItem?.Amount != amount)
                    {
                        DO.Product p = _dal.Product.GetByCondition(x => x != null && x?.Id == productId);
                        DO.OrderItem OItoChange = new()
                        {
                            OrderId = orderItem?.OrderId ?? 0,
                            Id = orderItem?.Id ?? 0,
                            Price = orderItem?.Price ?? 0,
                            ProductId = orderItem?.ProductId ?? 0
                        };
                        if (p.InStock < amount)
                        {
                            OItoChange.Amount = orderItem?.Amount ?? 0 + p.InStock;
                            p.InStock = 0;
                        }
                        else
                        {
                            OItoChange.Amount = amount;
                            p.InStock -= amount - orderItem?.Amount ?? 0;
                        }
                        _dal.Product.Update(p);
                        _dal.OrderItem.Update(OItoChange);
                    }
                }
                else { 
                    DO.Product p = _dal.Product.GetByCondition(x => x != null && x?.Id == productId);
                    if (p.InStock < amount)
                    {
                        p.InStock = 0;
                        amount = p.InStock;
                    }
                    else
                    {
                        p.InStock -= amount;
                    }
                    DO.OrderItem d1 = new() { ProductId = productId, OrderId = orderId, Price = p.Price, Amount = amount };
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