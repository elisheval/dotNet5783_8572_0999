using DalApi;

namespace Dal;

sealed internal class DalXml: IDal
{

    private static readonly Lazy<IDal> s_instance = new(() => new DalXml());
    public static IDal Instance { get { return s_instance.Value; } }
    public IOrder Order { get; } = new Dal.Order();
    public IOrderItem OrderItem { get; } = new Dal.OrderItem();
    public IProduct Product { get; } = new Dal.Product();
}
