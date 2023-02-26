//using PL.ProductWindows;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;
//using Simulator;
//using System.Diagnostics;
//using System.Windows.Media.Animation;
//using System.Windows.Threading;

//namespace PL
//{

//    /// <summary>
//    /// Interaction logic for Simulator.xaml
//    /// </summary>
//    public partial class SimulatorWindow : Window
//    {
//        public int timer
//        {
//            get { return (int)GetValue(timerProperty); }
//            set { SetValue(timerProperty, value); }
//        }
//        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
//        public static readonly DependencyProperty timerProperty =
//            DependencyProperty.Register("timer", typeof(int), typeof(SimulatorWindow));
//        string nextStatus;
//        string previousStatus;
//        Tuple<BO.Order, int, string, string> dcT;
//        Stopwatch stopWatch;
//        BackgroundWorker BW;
//        //=======progressBar variables
//        Duration duration;
//        DoubleAnimation doubleanimation;
//        ProgressBar ProgressBar;
//        //=======countdown timer variables
//        DispatcherTimer _timer;
//        TimeSpan _time;
//        //=======
//        private bool isTimerRun;


//        public SimulatorWindow()
//        {
//            InitializeComponent();
//            Simulator.Simulator.StartSimulator();
//            stopWatch = new Stopwatch();
//            timer = 0;
//            BW = new BackgroundWorker();
//            BW.DoWork += Worker_DoWork;
//            BW.ProgressChanged += Worker_ProgressChanged;
//            BW.WorkerReportsProgress = true;
//            BW.WorkerSupportsCancellation = true;
//            BW.RunWorkerAsync();
//        }

//        void countDownTimer(int sec)
//        {
//            _time = TimeSpan.FromSeconds(sec);

//            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
//            {
//                tbTime.Text = _time.ToString("c");
//                if (_time == TimeSpan.Zero) _timer.Stop();
//                _time = _time.Add(TimeSpan.FromSeconds(-1));
//            }, Application.Current.Dispatcher);

//            _timer.Start();
//        }
//        void ProgressBarStart(int sec)
//        {
//            if (ProgressBar != null)
//            {
//                pBar.Items.Remove(ProgressBar);
//            }
//            ProgressBar = new ProgressBar();
//            ProgressBar.IsIndeterminate = false;
//            ProgressBar.Orientation = Orientation.Horizontal;
//            ProgressBar.Width = 500;
//            ProgressBar.Height = 30;
//            duration = new Duration(TimeSpan.FromSeconds(sec * 2));
//            doubleanimation = new DoubleAnimation(200.0, duration);
//            ProgressBar.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);
//            pBar.Items.Add(ProgressBar);
//        }


//        private void FinishSimulator(object sender, EventArgs e)
//        {
//            if (isTimerRun)
//            {
//                stopWatch.Stop();
//                isTimerRun = false;
//            }
//            Simulator.Simulator.StopSimulator();
//            this.Close();
//            BW.CancelAsync();
//        }
//        private void Worker_DoWork(object sender, DoWorkEventArgs e)
//        {
//            Simulator.Simulator.CompleteSimulator+=Stop;
//            Simulator.Simulator.ProgressChange += ChangeOrder;
//            Simulator.Simulator.StartSimulator();
//            while (!BW.CancellationPending)
//            {
//                Thread.Sleep(1000);
//                BW.ReportProgress(1);
//            }
//        }
//        private void ChangeOrder(object sender, EventArgs e)
//        {
//            if (!(e is Details))
//                return;

//            Details details = e as Details;
//            this.previousStatus = (details.order.ShipDate == null) ? BO.Enums.OrderStatus.Approved.ToString() : BO.Enums.OrderStatus.Sent.ToString();
//            this.nextStatus = (details.order.ShipDate == null) ? BO.Enums.OrderStatus.Sent.ToString() : BO.Enums.OrderStatus.Supplied.ToString();
//            dcT = new Tuple<BO.Order, int, string, string>(details.order, details.seconds / 1000, previousStatus, nextStatus);
//            if (!CheckAccess())
//            {
//                Dispatcher.BeginInvoke(ChangeOrder, sender, e);
//            }
//            else
//            {
//                DataContext = dcT;
//                countDownTimer(details.seconds / 1000);

//                ProgressBarStart(details.seconds / 1000);
//            }
//        }
//        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
//        {
//            string timerText = stopWatch.Elapsed.ToString();
//            timerText = timerText.Substring(0, 8);
//            SimulatorTXTB.Text = timerText;
//        }
//        public void Stop(object sender, EventArgs e)
//        {
//            Simulator.Simulator.ProgressChange -= ChangeOrder;
//            Simulator.Simulator.CompleteSimulator -= Stop;

//            if (!CheckAccess())
//            {
//                Dispatcher.BeginInvoke(Stop, sender, e);
//            }
//            else
//            {
//                MessageBox.Show("complete updating");
//                this.Close();
//            }
//        }
//    }
//}

using BlApi;
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
            CurrentOrderHandle = "ID : " + args.Item1.Id +
            "\nCurrent status : " + args.Item1.OrderStatus;

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
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {
        if (backroundWorker.CancellationPending == false && isTimerRun) // Won't allow to cancel the window!!! It is not me!!!
        {
            e.Cancel = true;
            MessageBox.Show(@"DON""T CLOSE ME!!!", "STOP IT!!!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
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
}