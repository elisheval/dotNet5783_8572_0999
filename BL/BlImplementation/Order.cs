using BlApi;
using Dal;
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
                DeliveryDate = orderFromDo.DeliveryDate,


            }
            return order;
        }
        catch(DalApi.NoFoundItemExceptions ex)
        {
            throw new BO.NoFoundItemExceptions("no found order with this id", ex);
        }
    }
    public Order OrderShippingUpdate(int orderId)
    {
       BO.Order O= GetOrderById(orderId);
    }//עדכון שההזמנה נשלחה
    public Order OrderDeliveryUpdate(int orderId);
    public OrderTracking Ordertracking(int orderId);
    public void UpdateOrder();//בונוס


}
