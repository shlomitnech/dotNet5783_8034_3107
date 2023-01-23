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
using BlApi;

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

        public ShoppingCartWindow(Cart cart)    //constructor
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

        /// <summary>
        /// removes the amount of items in cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Decrease_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ShoppingCartGrid.SelectedItem is BO.OrderItem orderItem)
                {
                    myCart = bl.Cart.RemoveFromCart(myCart, orderItem.ProductID,1);
                    new ShoppingCartWindow(myCart).Show();
                    Close();
                }

            }
            catch (BO.EntityNotFound exc)
            {
                MessageBox.Show(exc.Message, "Cart Window", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        /// <summary>
        /// adds an item to cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Increase_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ShoppingCartGrid.SelectedItem is BO.OrderItem orderItem)
                {
                    myCart = bl!.Cart.AddToCart(myCart, orderItem.ProductID, 1);
                    new ShoppingCartWindow(myCart).Show();
                    Close();
                }
            }
            catch (BO.Exceptions exc)
            {
                MessageBox.Show(exc.Message, "Cart Window", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.EntityNotFound exc)
            {
                MessageBox.Show(exc.Message, "Cart Window", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.IncorrectInput exc)
            {
                MessageBox.Show(exc.Message, "Cart Window", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// deletes the whole cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clear_cart_Click(object sender, RoutedEventArgs e)
        {
            bl.Cart.DeleteCart(myCart!);
            new ShoppingCartWindow(myCart).Show();
            Close();
            
        }

        /// <summary>
        /// deleted product from cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Product(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ShoppingCartGrid.SelectedItem is BO.OrderItem orderItem)
                {
                    int amount = orderItem.Amount; //how many products of this kind exist
                    myCart = bl.Cart.RemoveFromCart(myCart, orderItem.ProductID, amount);
                    new ShoppingCartWindow(myCart).Show();
                    Close();
                }

            }
            catch (BO.EntityNotFound exc)
            {
                MessageBox.Show(exc.Message, "Cart Window", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
    }
}
