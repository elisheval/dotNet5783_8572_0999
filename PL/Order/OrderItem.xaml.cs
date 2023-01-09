using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderItem.xaml
    /// </summary>
    public partial class OrderItem : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        int orderId;

        public BO.OrderItem selectedOrderItem
        {
            get { return (BO.OrderItem)GetValue(OIProperty); }
            set { SetValue(OIProperty, value); }
        }
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OIProperty =
            DependencyProperty.Register("selectedOrderItem", typeof(BO.OrderItem), typeof(OrderItem));


        public bool ReadOnly
        {
            get { return (bool)GetValue(ReadOnlyProp); }
            set { SetValue(ReadOnlyProp, value); }
        }
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReadOnlyProp =
            DependencyProperty.Register("selectedOrderItem", typeof(bool), typeof(OrderItem));


        public OrderItem(BO.OrderItem selectedOrderItem, int orderId)
        {
            //MessageBox.Show(selectedOrderItem.ToString());
            this.selectedOrderItem = selectedOrderItem;
            this.orderId = orderId;
            ReadOnly = true;
            InitializeComponent();

        }

        public OrderItem(int orderId, bool isAdd)
        {
            selectedOrderItem = new();
            //this.selectedOrderItem.ProductName = "";
            ReadOnly = false;
            InitializeComponent();
        }
        private void confirmDelete(object sender, RoutedEventArgs e)
        {
            if (bl != null)
            {
                bl.Order.UpdateOrder(orderId, selectedOrderItem.ProductId, 0);
                this.Close();
            }
        }
        private void confirmUpdate(object sender, RoutedEventArgs e)
        {
            if (bl != null)
            {
                bl.Order.UpdateOrder(orderId, selectedOrderItem.ProductId, selectedOrderItem.AmountInCart);
                this.Close();
            }
        }

    }
}
