﻿using BlApi;
using Simulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Data;

namespace PL;

/// <summary>
/// simulator stimulat the store dayli working 
/// take the oldest order and Send/Provide it to customer
/// do it until orders end or by clicking the stop button 
/// </summary>
public partial class StartSimulatorWindow : Window
{
    private readonly Stopwatch stopWatch = new();
    private bool canCloseWindow=false;
    private volatile bool isTimerRun;
    private volatile bool isWating = false;

    readonly BackgroundWorker backroundWorker = new();

    private int startTime;//hold the working start time of each order - for progresBar
    private int totalWorkTime;//hold the total work secound on each order - for progresBar

    //event for update order list in order window
    internal delegate void ChangesOrders();
    internal static event ChangesOrders? updateOrderList;

    public string ExpectedOrderDetails//show on the screen expected order status & time to end
    {
        get { return (string)GetValue(ExpectedOrderDetailsProperty); }
        set { SetValue(ExpectedOrderDetailsProperty, value); }
    }
    // Using a DependencyProperty as the backing store for ExpectedOrderDetails.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ExpectedOrderDetailsProperty =
        DependencyProperty.Register("ExpectedOrderDetails", typeof(string), typeof(StartSimulatorWindow), new PropertyMetadata(null));

    public string CurrentOrderHandle//show on the screen current order status & the time started 
    {
        get { return (string)GetValue(CurrentOrderHandleProperty); }
        set { SetValue(CurrentOrderHandleProperty, value); }
    }
    // Using a DependencyProperty as the backing store for CurrentOrderHandle.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CurrentOrderHandleProperty =
        DependencyProperty.Register("CurrentOrderHandle", typeof(string), typeof(StartSimulatorWindow), new PropertyMetadata(null));


    public string ClockText
    {
        get { return (string)GetValue(ClockTextProperty); }
        set { SetValue(ClockTextProperty, value); }
    }
    // Using a DependencyProperty as the backing store for ClockText.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ClockTextProperty =
        DependencyProperty.Register("ClockText", typeof(string), typeof(StartSimulatorWindow), new PropertyMetadata(null));



    public double PrecentegeUpdate //save the cuurent precentege of the work on order until finshed
    {
        get { return (double)GetValue(PrecentegeUpdateProperty); }
        set { SetValue(PrecentegeUpdateProperty, value); }
    }

    // Using a DependencyProperty as the backing store for PrecentegeUpdate.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty PrecentegeUpdateProperty =
        DependencyProperty.Register("PrecentegeUpdate", typeof(double), typeof(StartSimulatorWindow), new PropertyMetadata(null));


    public StartSimulatorWindow()
    {
        InitializeComponent();
        ClockText = "00:00:00";

        backroundWorker.DoWork += Work;
        backroundWorker.ProgressChanged += UpdateScreen;
        backroundWorker.RunWorkerCompleted += CloseHandler;

        Simulator.Simulator.ScreenUpdate += Simulator_ScreenUpdate;//regester update screen and stop function to Simulator class event
        Simulator.Simulator.Wating += WaitForOrders;

        backroundWorker.WorkerReportsProgress = true;
        backroundWorker.WorkerSupportsCancellation = true;

        stopWatch.Start();
        isTimerRun = true;
        backroundWorker.RunWorkerAsync();
    }


    private void WaitForOrders()//shot down - event rise from simulator, becouse there is no more order to handel
    {
        isWating = true;
        backroundWorker.ReportProgress(2);
    }


    private void Simulator_ScreenUpdate(int x, int time, BO.Order order)//handel the event the wake in Simulator class
    {
        Tuple<BO.Order, int> t = new(order, time);//Tuple order and random time, then send it to ReportProgress 
        isWating = false;
        backroundWorker.ReportProgress(x, t);
    }

    private void Work(object? sender, DoWorkEventArgs? e)//regester to do work - do all the work that need to be done
    {
        Simulator.Simulator.Activate();
        while (!backroundWorker.CancellationPending)//handle clock
        {
            backroundWorker.ReportProgress(1);
            Thread.Sleep(50);
        }
    }



    private void UpdateScreen(Object? sender, ProgressChangedEventArgs? e)
    {
        //update the screen - call from ReportProgress - if want to update order details send order id, for clock update send 1
        if (e?.ProgressPercentage >=100000)//want to update order details
        {
            var args = (Tuple<BO.Order, int>)e.UserState!;//extract the Tuple that contain Random time to end and the order details

            //update current order handle details 
            CurrentOrderHandle = "order ID : " + args.Item1.Id +
            "\nOrder status : " + args.Item1.OrderStatus;

            //extract start time and end time
            string timerText = stopWatch.Elapsed.ToString();
            timerText = timerText[..8];
            string endTime = (stopWatch.Elapsed + TimeSpan.FromSeconds(args.Item2 / 1000)).ToString()[..8];

            //save the start time and the secound that need for the work to be done - for progres bar update
            startTime = (int)stopWatch.Elapsed.TotalSeconds;
            totalWorkTime = (args.Item2 / 1000);

            //update expected time & status of order after work will done
            ExpectedOrderDetails = "Started at : " + timerText.ToString() +
                "\nExpected processing end time : " + endTime +
                "\nWill be " + (args.Item1.OrderStatus == BO.Enums.OrderStatus.Sent ? "Provide." : "Sent.");

            updateOrderList?.Invoke();//UPDATE THE LIST IN ORDER WINDOW - RISE THE EVENT
        }
        else if (e?.ProgressPercentage == 1)//clock update and precentege progres bar
        {
            string timerText = stopWatch.Elapsed.ToString();
            ClockText = timerText[..8];
            //update the progrece bar
            if (!isWating)//we dont wait for order to come and the simulator working!
            {
                if (totalWorkTime != 0)
                    PrecentegeUpdate = ((stopWatch.Elapsed.TotalSeconds - startTime) / totalWorkTime) * 100;//duration time span since started / random expected working time
                else
                    PrecentegeUpdate = 0;//cant div by 0
            }
            else
            {
                PrecentegeUpdate = 0;
            }
        }
        else if (e?.ProgressPercentage == 2)//wait for order to show up
        {
            CurrentOrderHandle = "No more orders to work on...";
            ExpectedOrderDetails = "Wating for the next order";
            PrecentegeUpdate = 0;
        }
    }

    private void StopTimerButton_Click(object sender, RoutedEventArgs e)
    {
        backroundWorker.CancelAsync();
        canCloseWindow = true;
    }

    private void CloseHandler(object? sender, RunWorkerCompletedEventArgs? e)//close the window with soft thread cancel
    {
        Simulator.Simulator.DeAcitavet();
        isTimerRun = false;
        backroundWorker.DoWork -= Work;
        backroundWorker.ProgressChanged -= UpdateScreen;
        backroundWorker.RunWorkerCompleted -= CloseHandler;
        Simulator.Simulator.ScreenUpdate -= Simulator_ScreenUpdate;
        Simulator.Simulator.Wating -= WaitForOrders;
    }

    private void OnClosing(object sender, CancelEventArgs e)
    {
        if(!canCloseWindow)
        e.Cancel = true;
    }
}