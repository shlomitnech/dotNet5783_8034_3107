using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ShoppingCartWindow.xaml
    /// </summary>
    public partial class ShoppingCartWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        ObservableCollection<BO.OrderItem> orderList = new();
        BO.Cart myCart = new();

        public ShoppingCartWindow(Cart cart)
        {
            InitializeComponent();
            ShoppingCartGrid.ItemsSource = bl!.Cart.getCart(cart);
            ShoppingCartGrid.DataContext = orderList;//set data context of catalog as the product list
            TotalPrice.Text = cart.TotalPrice.ToString();
            myCart = cart;
        }
        private void ShoppingCartGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CheckoutButton_Click(object sender, RoutedEventArgs e)
        {
            new CheckoutWindow(myCart).Show();
            Close();
         
        }
        private void ReturnToCatalog_Click(object sender, RoutedEventArgs e)
        {
            new Catalog(myCart,bl!).Show();
            Close();
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
