
using DalApi;
namespace Dal;

internal sealed class DalList : IDal
{
    private DalList() { }

    //public static IDal Instance { get; } = new DalList();
    public IOrder Order => new DalOrder();
    public IOrderItem OrderItem => new DalOrderItem();
    public IProduct Product => new DalProduct();



    private static readonly object ThreadLock = new object();
    private static readonly Lazy<DalList> obj = new Lazy<DalList>(() => new DalList());
    public static IDal Instance{get {lock(ThreadLock){return obj.Value;}}}

}
