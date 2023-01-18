using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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

        public OrderTracking(int id, BO.Cart cart, BlApi.IBl? b)
        {
            InitializeComponent();
            bl = Factory.Get();
            myCart = cart;
            BO.OrderTracking ordtrack = new();
            try
            {
                ordtrack = bl?.Order.GetOrderTracking(id)!;
            }
            catch (BO.EntityNotFound ex)
            {
                new ErrorWindow("Order Tracking Window\n", ex.Message).ShowDialog();
                Close();
                new OrderTracking();
               

            }
            DataContext = ordtrack;
            id_enter.Text = ordtrack.ID.ToString();
            r_status.Text = ordtrack.Status.ToString();
       //     r_tracking.Text = ordtrack.Tracking?.ToString();
        }

        public OrderTracking() //to open up the screen
        {
            InitializeComponent();
            bl = BlApi.Factory.Get();
            DataContext = orderTracking;
            r_status.Visibility = Visibility.Collapsed;
        }

        private void backButtonClick(object sender, RoutedEventArgs e)
        {
            new MainWindow().ShowDialog();
            Close();
        }

        private void id_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void id_previewmousedown(object sender, RoutedEventArgs e)
        {
            id_enter.Clear();
        }
        private void id_PreviewTextInput(object sender, RoutedEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(id_enter.Text);//only gets numbers for id
        }

        private void searchClick(object sender, RoutedEventArgs e)
        {
            int id = 0;
            try
            {
                id = int.Parse(id_enter.Text);//save the entered id
            }
            catch (System.FormatException)
            {
                new ErrorWindow("Enter Order ID Window", "Wrong id number entered").ShowDialog();
            }
            Close();//close current window
            new OrderTracking(id, myCart, bl!).ShowDialog();//open order tracking window with entered id
   

        }
    }
}
