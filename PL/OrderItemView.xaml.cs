using BO;
using Dal;
using DO;
using System;
using System.Collections.Generic;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderItemView.xaml
    /// </summary>
    public partial class OrderItemView : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart myCart = new();
        BO.Order ord = new BO.Order();

        public OrderItemView(int id, Cart cart, BlApi.IBl? b)
        {
            InitializeComponent();
            bl = b;//new bl
            myCart = cart;
            try
            {
                ord = bl?.Order.GetBOOrder(id)!;//get the order from BO with matching id

            }
            catch (BO.EntityNotFound ex)
            {
                new ErrorWindow("Order View Window\n", ex.Message).ShowDialog();
            }
            catch (BO.Exceptions ex)
            {
                new ErrorWindow("Order View Window\n", ex.Message).ShowDialog();
            }
            DataContext = ord;
            OrderItemGrid.ItemsSource = bl?.Order.GetItemsForOrder(id); //calls get of DO order list, gets items for each order, and build BO orderItem list


        }

        private void OrderItemGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void tname_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (tname != null && tname.Text != "")
            {
                ord.CustomerName = tname.Text;
            }
        }

        private void temail_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (temail != null && temail.Text != "")
            {
                ord.CustomerEmail = temail.Text;
            }
        }

        private void taddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (taddress != null && taddress.Text != "")
            {
                ord.CustomerAddress = taddress.Text;
            }
        }

        private void taddress_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                e.Handled = new Regex("[^A-Z]+[^a-z]+[^A-Z]+[^a-z]+").IsMatch(taddress.Text);
            }
            catch (BO.EntityNotFound exc)
            {
                new ErrorWindow("ERROR in Product List View Window\n", exc.Message).ShowDialog();
            }
        }

        private void temail_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                e.Handled = new Regex("[^A-Z]+[^a-z]+[^A-Z]+[^a-z]+").IsMatch(temail.Text);
            }
            catch (BO.EntityNotFound exc)
            {
                new ErrorWindow("ERROR in Product List View Window\n", exc.Message).ShowDialog();
            }
        }

        private void tname_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                e.Handled = new Regex("[^A-Z]+[^a-z]+[^A-Z]+[^a-z]+").IsMatch(tname.Text);
            }
            catch (BO.EntityNotFound exc)
            {
                new ErrorWindow("ERROR in Product List View Window\n", exc.Message).ShowDialog();
            }
        }

        private void updateorder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Order.UpdateOrder(ord);
            }
            catch (BO.IncorrectInput ex) { new ErrorWindow("ERROR in Add Product Window\n", ex.Message).ShowDialog(); }
            catch (BO.IdExistException ex) { new ErrorWindow("ERROR in Add Product Window\n", ex.Message).ShowDialog(); }
            Close();
        }
    }
}
