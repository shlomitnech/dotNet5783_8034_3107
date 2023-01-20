using BO;
using System;
using System.Collections.Generic;
using System.Linq;
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
using BlImplementation;
using System.Diagnostics;
using DO;
using System.Security.AccessControl;
using System.Collections.ObjectModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class CheckoutWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        private BO.Order order = new BO.Order();
        private BO.Cart thisCart = new BO.Cart();

        public CheckoutWindow(Cart myCart) //if updating an existing product
        {
            InitializeComponent();
            thisCart = myCart;
            order.TotalPrice = myCart.TotalPrice;
            order.Items = myCart.Items;
            placeOrder.Visibility = Visibility.Visible;//show update button

        }

        private void placeOrder_Click(object sender, RoutedEventArgs e)
        {
            int? id = 0;
            try
            {
                try
                {
                    id = bl!.Cart.MakeOrder(thisCart, order.CustomerName!, order.CustomerEmail!, order.CustomerAddress!);
                }
                catch (BO.IncorrectInput ex) { new ErrorWindow("ERROR in Add Product Window\n", ex.Message).ShowDialog(); }
                catch (BO.IdExistException ex) { new ErrorWindow("ERROR in Add Product Window\n", ex.Message).ShowDialog(); }
                MessageBox.Show(string.Format("Order has been placed. Your order ID is: {0}", id));
                new MainWindow().Show();
                Close();
            }
            catch (BO.IncorrectInput ex) { new ErrorWindow("ERROR in Add Product Window\n", ex.Message).ShowDialog(); }
        }
        private void name1_PreviewMouseDown(object sender, RoutedEventArgs e) //clear the text that is written
        {
            name1.Clear();
        }
        private void email1_PreviewMouseDown(object sender, RoutedEventArgs e) //clear the text that is written
        {
            email1.Clear();
        }
        private void address1_PreviewMouseDown(object sender, RoutedEventArgs e) //clear the text that is written
        {
            address1.Clear();
        }

        private void name1_previewtextinput(object sender, RoutedEventArgs e)
        {
            try
            {
                e.Handled = new Regex("[^A-Z]+[^a-z]+[^A-Z]+[^a-z]+").IsMatch(name1.Text);
            }
            catch (BO.EntityNotFound exc)
            {
                new ErrorWindow("ERROR in Checkout Window\n", exc.Message).ShowDialog();
            }
        }
        private void email1_previewtextinput(object sender, RoutedEventArgs e)
        {
            try
            {
                e.Handled = new Regex("[^A-Z]+[^a-z]+[^A-Z]+[^a-z]+[^0-9]+[^.]+[^@]+").IsMatch(name1.Text);
            }
            catch (BO.EntityNotFound exc)
            {
                new ErrorWindow("ERROR in Checkout Window\n", exc.Message).ShowDialog();
            }
        }
        private void address1_previewtextinput(object sender, RoutedEventArgs e)
        {
            try
            {
                e.Handled = new Regex("[^A-Z]+[^a-z]+[^A-Z]+[^a-z]+[^0-9]+").IsMatch(name1.Text);
            }
            catch (BO.EntityNotFound exc)
            {
                new ErrorWindow("ERROR in Checkout Window\n", exc.Message).ShowDialog();
            }
        }
        private void name1_TextChanged(object sender, TextChangedEventArgs e) //to add/update name
        {
            if (name1 != null && name1.Text != "")
            {
                order.CustomerName = name1.Text;
            }
        }
        private void email1_TextChanged(object sender, TextChangedEventArgs e) //to add/update name
        {
            if (name1 != null && name1.Text != "")
            {
                order.CustomerEmail = email1.Text;
            }
        }
        private void address1_TextChanged(object sender, TextChangedEventArgs e) //to add/update name
        {
            if (name1 != null && name1.Text != "")
            {
                order.CustomerAddress = address1.Text;
            }
        }

    }
}
