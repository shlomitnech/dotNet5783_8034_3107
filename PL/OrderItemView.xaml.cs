using BO;
using Dal;
using System;
using System.Collections.Generic;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderItemView.xaml
    /// </summary>
    public partial class OrderItemView : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart myCart = new();


        public OrderItemView(int id, Cart cart, BlApi.IBl? b)
        {
            InitializeComponent();
            bl = b;//new bl
            myCart = cart;
            BO.Order ord = new();
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
    }
}
