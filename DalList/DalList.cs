
using DalApi;
namespace Dal;

public sealed class DalList : IDal
{

    public IOrder Order { get; } = new DalOrder();
    public IOrderItem OrderItem { get; } = new DalOrderItem();
    public IProduct Product { get; } = new DalProduct();

    class Nested
    {
        static Nested() { }
        internal static readonly DalList s_instance= new ();
    }

    static DalList() { }
    private DalList() { }

    private static readonly object ThreadLock = new();
    public static IDal Instance{get {lock(ThreadLock){return Nested.s_instance; }}}
    
    
    //we can also use this code:
    //private static readonly Lazy<IDal> s_instance=new (()=>new DalList());
    //public static IDal Instance { get{ return s_instance.Value; } }
}
