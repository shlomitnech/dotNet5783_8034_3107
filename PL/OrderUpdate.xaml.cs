using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderUpdate.xaml
    /// </summary>
    public partial class OrderUpdate : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        private BO.OrderForList ord = new();
        public OrderUpdate(BlApi.IBl? blay)    //constructor
        {
            InitializeComponent();
            bl = blay; //create a new BL
            DataContext = ord;
            ord_Status.ItemsSource = Enum.GetValues(typeof(BO.Enums.OrderStatus));//set combobox values to enums
            updateShip.Visibility = Visibility.Collapsed;
            updateDelivery.Visibility = Visibility.Collapsed;
        }
        public OrderUpdate(BO.OrderForList orderForList, BlApi.IBl? b)  //constructor
        {
            InitializeComponent();
            bl = b;//new bl
            ord = orderForList;
            DataContext = ord;
            ord_Status.ItemsSource = Enum.GetValues(typeof(BO.Enums.OrderStatus));
            updateShip.Visibility = Visibility.Visible;
            updateDelivery.Visibility = Visibility.Visible;
            ord_ID.IsReadOnly = true;
            ord_Name.IsReadOnly = true;
            ord_amount.IsReadOnly = true;
            ord_price.IsReadOnly = true;
        }
  

        private void updateShip_Click(object sender, RoutedEventArgs e)   //updates the ship date
        {
            try
            {
                bl!.Order.ShipUpdate(ord.ID);
                Close();
            }
            catch (DalApi.EntityNotFound ex)
            {
                new ErrorWindow("Update Orders For Admin\n", ex.Message).ShowDialog();

            }
        }
        private void updateDelivery_Click(object sender, RoutedEventArgs e)   //updates the delivery date
        {
            try
            {
                bl!.Order.DeliveryUpdate(ord.ID);
                Close();
            }
            catch (DalApi.EntityNotFound ex)
            {
                new ErrorWindow("Update Orders For Admin\n", ex.Message).ShowDialog();
            }

        }

        private void ord_Status_SelectionChanged(object sender, SelectionChangedEventArgs e)    //updates the order status
        {
            BO.Enums.OrderStatus orderStat = (BO.Enums.OrderStatus)ord_Status.SelectedItem;
            ord.Status = orderStat;
        }

        private void ___GoBack__Click(object sender, RoutedEventArgs e)    //goes back to the product list window
        {
            new ProductListWindow(bl!).ShowDialog();
            Close();
        }
    }


}
