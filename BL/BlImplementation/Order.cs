using BlApi;
using System.Security.Cryptography.X509Certificates;

namespace BlImplementation;
internal class Order : IOrder
{
    private DalApi.IDal _dal = new Dal.DalList();

    #region total price
    /// <param name="orderId">get id of order</param>
    /// <summary>
    /// inner method, calculate the total price of the getter order
    /// </summary>
    /// <returns>the total price</returns>
    private double _totalPrice(int orderId)
    {
        double totalPrice = 0;
        IEnumerable<DO.OrderItem?> orders = _dal.OrderItem.GetAll((x) => x!=null && x.Value.OrderId == orderId);
        foreach (var orderItem in orders)
        {
            if (orderItem != null)
                totalPrice += orderItem.Value.Price * orderItem.Value.Amount;
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
        IEnumerable<DO.Order?> ordersFromDo = _dal.Order.GetAll();
        List<BO.OrderForList> ordersForList = new();
        foreach (var order in ordersFromDo)
        {
            if (order != null)
            {
                BO.OrderForList orderForListToAdd = new()
                {
                    Id = order.Value.ID,
                    CustomerName = order.Value.CustomerName,
                    OrderStatus = _orderStatus(order.Value),
                    ItemsAmount = _dal.OrderItem.GetAll(x=>x!=null && order.Value.ID==x.Value.OrderId).Count(),
                    TotalPrice = _totalPrice(order.Value.ID)
                };
                ordersForList.Add(orderForListToAdd);
            }
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
            DO.Order orderFromDo = _dal.Order.GetByCondition((x) => x!=null && orderId == x.Value.ID);
            IEnumerable<DO.OrderItem?> orderItemsFromDo = _dal.OrderItem.GetAll((x)=>x!=null&&orderId==x.Value.OrderId);
            List<BO.OrderItem> ordersItems = new();
            foreach (var orderItem in orderItemsFromDo)
            {
                if (orderItem != null)
                {
                    BO.OrderItem orderToAdd = new()
                    {
                        ProductId = orderItem.Value.Id,
                        ProductName = _dal.Product.GetByCondition(x=>x!=null&&orderItem.Value.ProductId==x.Value.Id).Name,
                        Price = orderItem.Value.Price,
                        AmountInCart = orderItem.Value.Amount,
                        TotalPriceForItem = orderItem.Value.Price * orderItem.Value.Amount
                    };
                    ordersItems.Add(orderToAdd);
                }
            }
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
        DO.Order dalOrder = new ();
        try
        {
            dalOrder = _dal.Order.GetByCondition(x=>x!=null&&x.Value.ID==orderId);//get the details order
        }
        catch (DO.NoFoundItemExceptions exe)//check if exist
        {
            throw new BO.NoFoundItemExceptions("no order exist with this ID", exe);
        }
        if (dalOrder.ShipDate != null)// check if did not send
        {
            throw new BO.InvalidDateChange("this order already sent");
        }

        IEnumerable<DO.OrderItem?> dalOrderItems =_dal.OrderItem.GetAll(x=>x!=null && x.Value.OrderId==dalOrder.ID);
        List<BO.OrderItem> blOrderItems = new();
        double totalPrice = 0;
        foreach (var item in dalOrderItems)
        {
            if (item != null)
            {
                BO.OrderItem OrderItemToPush = new();
                OrderItemToPush.Id = item.Value.Id;
                OrderItemToPush.ProductId = item.Value.ProductId;
                OrderItemToPush.ProductName = _dal.Product.GetByCondition(x=>x!=null&&x.Value.Id==item.Value.ProductId).Name;
                OrderItemToPush.Price = item.Value.Price;
                OrderItemToPush.AmountInCart = item.Value.Amount;
                OrderItemToPush.TotalPriceForItem = item.Value.Price * item.Value.Amount;
                totalPrice += OrderItemToPush.TotalPriceForItem;
                blOrderItems.Add(OrderItemToPush);
            }
        }

        BO.Order blOrder = new();
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
        blOrder.TotalOrderPrice = totalPrice;
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
            dalOrder = _dal.Order.GetByCondition((x) => x!=null&&orderId == x.Value.ID);//get the order details
            if (dalOrder.DeliveryDate != null)// 
            {
                throw new BO.InvalidDateChange("this order already delivery");
            }
            if (dalOrder.ShipDate ==null)
                throw new BO.InvalidDateChange("the order cannot delivery before send");
            IEnumerable<DO.OrderItem?> dalOrderItems = _dal.OrderItem.GetAll(x=>x!=null&&x.Value.OrderId==orderId);
            List<BO.OrderItem> blOrderItems = new ();
            double totalPrice = 0;
            foreach (var item in dalOrderItems)
            {
                if (item != null)
                {
                    BO.OrderItem OrderItemToPush = new();
                    OrderItemToPush.Id = item.Value.Id;
                    OrderItemToPush.ProductId = item.Value.ProductId;
                    OrderItemToPush.ProductName = _dal.Product.GetByCondition(x=>x!=null&&x.Value.Id==item.Value.ProductId).Name;
                    OrderItemToPush.Price = item.Value.Price;
                    OrderItemToPush.AmountInCart = item.Value.Amount;
                    OrderItemToPush.TotalPriceForItem = item.Value.Price * item.Value.Amount;
                    totalPrice += OrderItemToPush.TotalPriceForItem;
                    blOrderItems.Add(OrderItemToPush);
                }
            }

            BO.Order blOrder = new();
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
            blOrder.TotalOrderPrice = totalPrice;
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
            dalOrder = _dal.Order.GetByCondition(x=>x!=null&&x.Value.ID==orderId);//מקבל את פרטי ההזמנה  
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
        if (dalOrder.ShipDate !=null)
        {
            tracking = BO.Enums.OrderStatus.Sent;
            orderTracking.DetailOrderStatuses.Add((dalOrder.ShipDate, "order shipped"));
        }
        if (dalOrder.DeliveryDate !=null)
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
            DO.Order dalOrder = new DO.Order();
            dalOrder = _dal.Order.GetByCondition(x=>x!=null&&x.Value.ID==orderId);
            if (dalOrder.ShipDate !=null)//if the order sent the manager cannot change it
                throw new BO.NoAccessToSentOrder("the order already sent, you can't change the date");
            if (amount < 0)//invalid
                throw new BO.InvalidValueException("invalid amount of product");
            if (amount == 0)//This is a sign that the manager wants to delete
            {
                DO.Product p = _dal.Product.GetByCondition(x=>x!=null&&x.Value.Id==productId);
                DO.OrderItem orderitem = _dal.OrderItem.GetByCondition(x=>x!=null&&x.Value.ProductId==productId &&x.Value.OrderId==orderId);
                p.InStock += orderitem.Amount;
                _dal.OrderItem.Delete(orderitem.Id);
                _dal.Product.Update(p);
            }
            else
            {
                IEnumerable<DO.OrderItem?> OrderItems = _dal.OrderItem.GetAll();
                bool flag = false;
                foreach (var oi in OrderItems)//look for the order item,
                {
                    if (oi!=null){
                        if (oi.Value.ProductId == productId && oi.Value.OrderId == orderId)//if the product exist- change the amount to be the getter amount
                        {
                            flag = true;
                            if (oi.Value.Amount != amount)
                            {
                                DO.Product p = _dal.Product.GetByCondition(x=> x != null && x.Value.Id == productId);
                                DO.OrderItem OItoChange = new()
                                {
                                    OrderId = oi.Value.OrderId,
                                    Id = oi.Value.Id,
                                    Price = oi.Value.Price,
                                    ProductId = oi.Value.ProductId
                                };
                                if (p.InStock < amount)
                                {
                                    OItoChange.Amount = oi.Value.Amount + p.InStock;
                                    p.InStock = 0;
                                }
                                else
                                {
                                    OItoChange.Amount = amount;
                                    p.InStock -= amount - oi.Value.Amount;
                                }
                                _dal.Product.Update(p);
                                _dal.OrderItem.Update(OItoChange);
                            }
                        }
                    }
                }
                if (!flag)//if not exist enter the product to the order
                {
                    DO.Product p = _dal.Product.GetByCondition(x=>x != null && x.Value.Id == productId);
                    if (p.InStock < amount)
                    {
                        p.InStock = 0;
                        amount = p.InStock;
                    }
                    else
                    {
                        p.InStock -= amount;
                    }
                    DO.OrderItem d1 = new() { ProductId=productId,OrderId=orderId,Price= p.Price,Amount=amount };
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