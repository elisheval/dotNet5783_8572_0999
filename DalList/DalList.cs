
using DalApi;

namespace Dal;

 internal sealed class DalList:IDal
{
    private DalList() { }
    public static IDal Instance { get; } = new DalList();
    public IOrder Order => new DalOrder();
    public IOrderItem OrderItem => new DalOrderItem();
    public IProduct Product => new DalProduct();
}
