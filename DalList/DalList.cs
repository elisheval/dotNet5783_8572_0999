
using DalApi;
namespace Dal;

internal sealed class DalList : IDal
{

    public IOrder Order => new DalOrder();
    public IOrderItem OrderItem => new DalOrderItem();
    public IProduct Product => new DalProduct();

    class Nested
    {
        static Nested() { }
        internal static readonly DalList s_instance= new ();
    }

    static DalList() { }
    private DalList() { }

    private static readonly object ThreadLock = new();
    public static IDal Instance{get {lock(ThreadLock){return Nested.s_instance; }}}
    
    
    //public static IDal Instance { get; } = new DalList();
}
