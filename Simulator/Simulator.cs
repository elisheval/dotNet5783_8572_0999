﻿using System.ComponentModel;
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