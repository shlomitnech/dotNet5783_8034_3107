using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static System.Net.Mime.MediaTypeNames;

namespace PL
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart cart = new();
        ObservableCollection<BO.OrderItem> orderItems = new();


        public CartWindow(BO.Cart myCart, BlApi.IBl b)
        {
            InitializeComponent();
            bl = b;
            orderItems.Clear();
            cart = myCart;
            if (myCart.orderItems != null)
            {
               // PList.DataContext = orderItems;//set data context of orderItem list as the orderItems
            }
            else
            {
              //  PList.DataContext = null;
            }
          //  Subtotal.DataContext = cart;//set subtotal data context to our cart
        }


        void clickOnHomeBtn(object sender, RoutedEventArgs e)
        {
            new Catalog(cart, bl!).ShowDialog();
            Close();//close this window
        }
        void Back_Click(object sender, RoutedEventArgs e)
        {
            new Catalog(cart, bl!).ShowDialog();
            Close();//close this window
        }
        private void StackPanel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void CheckoutButton_Click(object sender, RoutedEventArgs e)
        {
       //     if (cvv.Text.Length != 3 || expire.Text.Length != 4 || cardname.Text.Length < 12)
            {
                new ErrorWindow("Credit card details window", "Wrong credit card info was inputted");
            }
      //      new RegisterWindow(cart, bl!).ShowDialog();//show succesful order placed window
        }
        private void ViewOrderItem_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
       //     new OrderItemView(cart, bl!).ShowDialog();//open a window to view order item details
        }
        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
      //      BO.OrderItem orderItem = (BO.OrderItem)PList.SelectedItem;//save the selected order item 
            try
            {
            //    cart = PL.Tools.CastBoCToPo(bl!.Cart.UpdateCart(PL.Tools.CastPoCToBo(cart), orderItem.ID, 0));//remove the selected product from cart
            }
            catch (BO.EntityNotFound ex)
            {
                new ErrorWindow("Cart Window", ex.Message).ShowDialog();
            }
          //  orderItems = PL.Tools.IEnumerableToObservable(cart.OrderItems!);//s et data context of orderItem list as the orderItems
         //   Subtotal.DataContext = cart;//set subtotal data context to our cart
        }
        private void ReduceItem_Click(object sender, RoutedEventArgs e)
        {
          //  BO.OrderItem orderItem = (BO.OrderItem)PList.SelectedItem;//save the selected order item 
            try
            {
                //cart = PL.Tools.CastBoCToPo(bl!.Cart.UpdateCart(PL.Tools.CastPoCToBo(cart), orderItem.ID, orderItem.Amount - 1));//remove one of the selected products from cart
            }
            catch (BO.EntityNotFound ex)
            {
                new ErrorWindow("Cart Window", ex.Message).ShowDialog();
            }
           // orderItems = PL.Tools.IEnumerableToObservable(cart.OrderItems!);//save the catalog collection from BO in PO obsv collec
            //PList.DataContext = orderItems;//set data context of orderItem list as the orderItems
         //   Subtotal.DataContext = cart;//set subtotal data context to our cart
        }
        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderItem orderItem = (BO.OrderItem)PList.SelectedItem;//save the selected order item 
            try
            {
               // cart = PL.Tools.CastBoCToPo(bl!.Cart.AddToCart(PL.Tools.CastPoCToBo(cart), orderItem.ID));//add the selected product to cart
            }
            catch (BO.EntityNotFound ex)
            {
                new ErrorWindow("Cart Window", ex.Message).ShowDialog();
            }
            catch (BO.UnfoundException ex)
            {
                new ErrorWindow("Cart Window", ex.Message).ShowDialog();
            }
            catch (BO.Exceptions ex)
            {
                new ErrorWindow("Cart Window", ex.Message).ShowDialog();
            }
            catch (BO.IdExistException ex)
            {
                new ErrorWindow("Cart Window", ex.Message).ShowDialog();
            }
           // orderItems = PL.Tools.IEnumerableToObservable(cart.OrderItems!);//save the catalog collection from BO in PO obsv collec
           // PList.DataContext = orderItems;//set data context of orderItem list as the orderItems
         //   Subtotal.DataContext = cart;//set subtotal data context to our cart
        }
        private void tid_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);//only gets numbers for id
        }
        protected void SelectCurrentItem(Object sender, KeyboardFocusChangedEventArgs e)
        {
            ListViewItem item = (ListViewItem)sender;
            item.IsSelected = true;
        }
    }
}
