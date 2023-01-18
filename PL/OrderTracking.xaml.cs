using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
using BlApi;
using BO;
using DO;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderTracking.xaml
    /// </summary>
    public partial class OrderTracking : Window
    {

        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Order order = new BO.Order();
        BO.OrderTracking orderTracking = new();
        BO.Cart myCart = new();

        public OrderTracking(BO.Cart cart, BlApi.IBl? b)//empty ctor
        {
            InitializeComponent();
            bl = b;//new bl
            myCart = cart;
            DataContext = orderTracking;
        }

        private void backButtonClick(object sender, RoutedEventArgs e)
        {
            new MainWindow().ShowDialog();
        }

        private void id_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void id_previewmousedown(object sender, RoutedEventArgs e)
        {
            id.Clear();
        }

        private void id_PreviewTextInput(object sender, RoutedEventArgs e)
        {
            if (id != null && id.Text != "")
            {
                if (int.TryParse(id.Text, out int val))
                {
                    order.ID = val;
                }
            }
        }

        private void searchClick(object sender, RoutedEventArgs e)
        {
            BO.OrderTracking order = bl!.Order.GetOrderTracking(id);
            r_status.Text = order.Status.ToString();
            r_tracking.Text = order.Tracking?.ToString();
        }
    }
}
