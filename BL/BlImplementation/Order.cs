using BlApi;
using BO;
using Dal;
using System.Linq.Expressions;
using static BO.Enums;

namespace BlImplementation;
internal class Order:IOrder
{
    private DalApi.IDal _dal = new DalList();
    private double _totalPrice(int orderId)
    {
        double totalPrice = 0;
        IEnumerable<DO.OrderItem> orders= _dal.OrderItem.getOrderItemsArrWithSpecificOrderId(orderId);
        foreach(DO.OrderItem orderItem in orders)
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
        foreach(DO.Order order in ordersFromDo)
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
    public List<OrderItem> OrderItemList { get; set; }
    public int TotalOrderPrice { get; set; }
    public BO.Order GetOrderById(int orderId)
    {
        if (orderId <= 0)
        {
            throw new BO.InvalidValueException("invalid order id");
        }
        try
        {
            DO.Order orderFromDo=_dal.Order.Get(orderId);
            IEnumerable<DO.OrderItem> orderItems = _dal.OrderItem.getOrderItemsArrWithSpecificOrderId(orderId);
            BO.Order order = new BO.Order()
            {
                Id = orderId,
                CustomerName = orderFromDo.CustomerName,
                CustomerEmail = orderFromDo.CustomerEmail,
                CustomerAddress = orderFromDo.CustomerAddress,
                OrderStatus = _orderStatus(orderFromDo),
                OrderDate = orderFromDo.OrderDate,
                ShipDate = orderFromDo.ShipDate,
                DeliveryDate = orderFromDo.DeliveryDate
            }
            return order;
        }
        catch(DalApi.NoFoundItemExceptions ex)
        {
            throw new BO.NoFoundItemExceptions("no found order with this id", ex);
        }
    }
    public BO.Order OrderShippingUpdate(int orderId)
    {
        DO.Order dalOrder=new DO.Order();
        try
        {
            dalOrder = _dal.Order.Get(orderId);//מקבל את פרטי ההזמנה  
        }
        catch (BO.NoFoundItemExceptions exe)//בודק שההזמנה קיימת
        {
            throw new BO.NoFoundItemExceptions("no order exist with this ID", exe);
        }
        if(dalOrder.ShipDate != DateTime.MinValue)// בודק אם עדיין לא נשלחה
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
    }//עדכון שההזמנה נשלחה
   
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
        DO.Order dalOrder = new DO.Order();
        try
        {
            dalOrder = _dal.Order.Get(orderId);//מקבל את פרטי ההזמנה  
        }
        catch (BO.NoFoundItemExceptions exe)//בודק שההזמנה קיימת
        {
            throw new BO.NoFoundItemExceptions("no order exist with this ID", exe);
        }
        BO.OrderTracking orderTrackingToCopy = new BO.OrderTracking { };
        List<BO.DetailOrderStatus> blStatus = new List<DetailOrderStatus> { };
        int tracking=0;
        if (dalOrder.OrderDate != DateTime.MinValue)
        {
            BO.DetailOrderStatus orderApproved = new BO.DetailOrderStatus { };
            orderApproved.Date = dalOrder.OrderDate;
            orderApproved.OrderStatus = BO.Enums.OrderStatus.Approved;
            blStatus.Add(orderApproved);
            tracking = 1;
            blStatus.Add(orderApproved);
        }
        if(dalOrder.ShipDate != DateTime.MinValue)
        {
            BO.DetailOrderStatus orderSent = new BO.DetailOrderStatus { };
            orderSent.Date = dalOrder.ShipDate;
            orderSent.OrderStatus = BO.Enums.OrderStatus.Sent;
            blStatus.Add(orderSent);
            tracking = 2;
            blStatus.Add(orderSent);
        }
        if (dalOrder.DeliveryDate != DateTime.MinValue)
        {
            BO.DetailOrderStatus orderDelivery = new BO.DetailOrderStatus { };
            orderDelivery.Date = dalOrder.DeliveryDate;
            orderDelivery.OrderStatus = BO.Enums.OrderStatus.Supplied;
            blStatus.Add(orderDelivery);
            tracking = 3;
            blStatus.Add(orderDelivery);
        }
        switch(tracking){
            case 1:
                orderTrackingToCopy.OrderStatus = BO.Enums.OrderStatus.Approved;
                break;
            case 2:
                orderTrackingToCopy.OrderStatus = BO.Enums.OrderStatus.Sent;
                break;
            case 3:
                orderTrackingToCopy.OrderStatus = BO.Enums.OrderStatus.Supplied;
                break;
            default: 
                break;
        }
        orderTrackingToCopy.Id = dalOrder.ID;
        return orderTrackingToCopy;
    }
    
    //public void UpdateOrder();//בונוס


}
