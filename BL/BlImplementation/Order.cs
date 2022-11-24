using BlApi;
using BO;
using Dal;
using static BO.Enums;

namespace BlImplementation;
internal class Order : IOrder
{
    private DalApi.IDal _dal = new DalList();
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
    private BO.Enums.OrderStatus _orderStatus(DO.Order order)
    {
        BO.Enums.OrderStatus os;
        if (order.DeliveryDate != DateTime.MinValue) os = BO.Enums.OrderStatus.Supplied;
        else if (order.ShipDate != DateTime.MinValue) os = BO.Enums.OrderStatus.Sent;
        else os = BO.Enums.OrderStatus.Approved;
        return os;
    }
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
                    ProductName=_dal.Product.Get(orderItem.ProductId).Name,
                    Price=orderItem.Price,
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
        catch(DalApi.NoFoundItemExceptions ex)
        {
            throw new BO.NoFoundItemExceptions("no found order with this id", ex);
        }
    }
    public BO.Order OrderShippingUpdate(int orderId)
    {
        DO.Order dalOrder = new DO.Order();
        try
        {
            dalOrder = _dal.Order.Get(orderId);//מקבל את פרטי ההזמנה  
        }
        catch (BO.NoFoundItemExceptions exe)//בודק שההזמנה קיימת
        {
            throw new BO.NoFoundItemExceptions("no order exist with this ID", exe);
        }
        if (dalOrder.ShipDate != DateTime.MinValue)// בודק אם עדיין לא נשלחה
        {
            throw new BO.OrderAlreadySend("this order already sent");
        }

        List<DO.OrderItem> dalOrderItems = (List<DO.OrderItem>)_dal.OrderItem.getOrderItemsArrWithSpecificOrderId(orderId);
        List<BO.OrderItem> blOrderItems = new List<BO.OrderItem>();
        foreach (DO.OrderItem item in dalOrderItems)//למלא את הליסט של הפריטים
        {
            BO.OrderItem OrderItemToPush = new BO.OrderItem();
            OrderItemToPush.Id = item.Id;
            OrderItemToPush.ProductId = item.ProductId;
            OrderItemToPush.ProductName = _dal.Product.Get(item.ProductId).Name;
            OrderItemToPush.Price = item.Price;
            OrderItemToPush.AmountInCart = item.Amount;
            OrderItemToPush.TotalPriceForItem = item.Price * item.Amount;
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
        _dal.Order.Update(dalOrder);
        return blOrder;
    }
    public BO.Order OrderDeliveryUpdate(int orderId)
    {
        DO.Order dalOrder = new DO.Order();
        try
        {
            dalOrder = _dal.Order.Get(orderId);//מקבל את פרטי ההזמנה  
        }
        catch (BO.NoFoundItemExceptions exe)//בודק שההזמנה קיימת
        {
            throw new BO.NoFoundItemExceptions("no order exist with this ID", exe);
        }
        if (dalOrder.DeliveryDate != DateTime.MinValue)// בודק אם עדיין לא סופקה
        {
            throw new BO.OrderAlreadyDelivery("this order already delivery");
        }
        List<DO.OrderItem> dalOrderItems = (List<DO.OrderItem>)_dal.OrderItem.getOrderItemsArrWithSpecificOrderId(orderId);
        List<BO.OrderItem> blOrderItems = new List<BO.OrderItem>();
        foreach (DO.OrderItem item in dalOrderItems)//למלא את הליסט של הפריטים
        {
            BO.OrderItem OrderItemToPush = new BO.OrderItem();
            OrderItemToPush.Id = item.Id;
            OrderItemToPush.ProductId = item.ProductId;
            OrderItemToPush.ProductName = _dal.Product.Get(item.ProductId).Name;
            OrderItemToPush.Price = item.Price;
            OrderItemToPush.AmountInCart = item.Amount;
            OrderItemToPush.TotalPriceForItem = item.Price * item.Amount;
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
        blOrder.DeliveryDate = dalOrder.ShipDate;
        _dal.Order.Update(dalOrder);
        return blOrder;
    }
    public BO.OrderTracking Ordertracking(int orderId)
    {
        if (orderId <= 0)
            throw new BO.InvalidValueException("invalid order id");
        DO.Order dalOrder = new DO.Order();
        try
        {
            dalOrder = _dal.Order.Get(orderId);//מקבל את פרטי ההזמנה  
        }
        catch (DalApi.NoFoundItemExceptions exe)//בודק שההזמנה קיימת
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

    //public void UpdateOrder();//בונוס


}
