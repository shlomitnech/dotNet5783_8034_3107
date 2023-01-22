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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderUpdate.xaml
    /// </summary>
    public partial class OrderUpdate : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        private BO.OrderForList ord = new();
        public OrderUpdate(BlApi.IBl? blay)
        {
            InitializeComponent();
            bl = blay; //create a new BL
            DataContext = ord;
            ord_Status.ItemsSource = Enum.GetValues(typeof(BO.Enums.OrderStatus));//set combobox values to enums
            updateShip.Visibility = Visibility.Collapsed;
            updateDelivery.Visibility = Visibility.Collapsed;
        }
        public OrderUpdate(BO.OrderForList orderForList, BlApi.IBl? b)
        {
            InitializeComponent();
            bl = b;//new bl
            ord = orderForList;
            DataContext = ord;
            ord_Status.ItemsSource = Enum.GetValues(typeof(BO.Enums.OrderStatus));
            updateShip.Visibility = Visibility.Visible;
            updateDelivery.Visibility = Visibility.Visible;
            ord_ID.IsReadOnly = true;
        }
  

        private void updateShip_Click(object sender, RoutedEventArgs e)
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
        private void updateDelivery_Click(object sender, RoutedEventArgs e)
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

        private void ord_Status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Enums.OrderStatus orderStat = (BO.Enums.OrderStatus)ord_Status.SelectedItem;
            ord.Status = orderStat;
        }






        private void ___GoBack__Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow(bl!).ShowDialog();
            Close();
        }

        private void ord_Name_PreviewMouseDown_1(object sender, MouseButtonEventArgs e)
        {
            ord_Name.Clear();
        }

        private void ord_Name_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (ord_Name != null && ord_Name.Text != "")
            {
                ord.CustomerName = ord_Name.Text;
            }
        }

        private void ord_Name_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            try
            {
                e.Handled = new Regex("[^A-Z]+[^a-z]+[^A-Z]+[^a-z]+").IsMatch(ord_Name.Text);
            }
            catch (BO.EntityNotFound exc)
            {
                new ErrorWindow("ERROR in Product List View Window\n", exc.Message).ShowDialog();
            }
        }

        private void ord_amount_PreviewMouseDown_1(object sender, MouseButtonEventArgs e)
        {
            ord_amount.Clear();


        }

        private void ord_amount_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(ord_price.Text);

        }



        private void ord_price_PreviewMouseDown_1(object sender, MouseButtonEventArgs e)
        {
            ord_price.Clear();
        }

        private void ord_amount_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (ord_amount != null && ord_amount.Text != "")
            {
                if (int.TryParse(ord_amount.Text, out int val))
                {
                    ord.AmountOfItems = val;
                }
            }
        }

        private void ord_price_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (ord_price != null && ord_price.Text != "")
            {
                if (int.TryParse(ord_price.Text, out int val))
                {
                    ord.TotalPrice = val;
                }
            }
        }

        private void ord_price_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(ord_price.Text);

        }
    }


}
