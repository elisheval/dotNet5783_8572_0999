//using System.ComponentModel;

//namespace Simulator;

//public static class Simulator
//{
//    static BlApi.IBl? bl = BlApi.Factory.Get();
//    private static string? previousState;
//    private static string? afterState;
//    public static event EventHandler CompleteSimulator;
//    public static event EventHandler ProgressChange;


//    static bool cuntinue;
//    public static void StartSimulator()
//    {
//        cuntinue = true;
//        Thread thread = new (new ThreadStart(GetNextOrder));
//        thread.Start();
//        return;
//    }
//    public static void GetNextOrder()
//    {
//        int? id = bl!.Order.GetNextOrderToHandle();
//        while (cuntinue)
//        {
//            if (id == null)
//                StopSimulator();
//            else
//            {
//                BO.Order order = bl.Order.GetOrderById((int)id);
//                Random rand = new();
//                var time = rand.Next(1000, 15000);
//                Details d = new Details(order, time);
//                if (ProgressChange != null)
//                {
//                    ProgressChange(null, d);
//                }
//                Thread.Sleep(time);
//                afterState = (previousState == "Approved" ? bl.Order.OrderShippingUpdate((int)id): bl.Order.OrderDeliveryUpdate((int)id)).OrderStatus.ToString();

//            }

//        }
//    }
//    public static void StopSimulator()
//    {
//        cuntinue = false;
//        CompleteSimulator("",EventArgs.Empty);
//    }
//}
//public class Details : EventArgs
//{
//    public BO.Order order;
//    public int seconds;
//    public Details(BO.Order ord, int sec)
//    {
//        order = ord;
//        seconds = sec;
//    }
//}

using System.ComponentModel;
using System.Linq.Expressions;



namespace Simulator;
/// <summary>
/// simulator stimulat the store dayli working 
/// Action func start the simulator,
/// and update the window if any change needed by events
/// </summary>
public static class Simulator
{
    private static readonly BlApi.IBl? bl = BlApi.Factory.Get();

    private static readonly Random rand = new(DateTime.Now.Millisecond);

    private static volatile bool isActive = false;

    public delegate void updateDel(int x, int time, BO.Order o);
    public static event updateDel? ScreenUpdate;

    public delegate void noMoreOrders();
    public static event noMoreOrders? Wating;

    public static void Activate()
    {
        isActive = true;
        new Thread(() =>
        {
            while (isActive)
            {
                int? id = bl?.Order.GetNextOrderToHandle();

                if (id != null)
                {
                    BO.Order order = bl!.Order.GetOrderById((int)id);
                    int time = rand.Next(3, 10);
                    ScreenUpdate!(((int)id), time * 1000, order);
                    Thread.Sleep(1000 * time);
                    if (order.ShipDate == null)
                    {
                        bl?.Order.OrderShippingUpdate((int)id);
                    }
                    else if (order.DeliveryDate == null)
                    {
                        bl?.Order.OrderDeliveryUpdate((int)id);
                    }
                }
                else//return null - no more order to work on, shout down simulator
                {
                    Wating!();
                    Thread.Sleep(1000);
                }

            }
        }).Start();
    }

    public static void DeAcitavet() => isActive = false;
}