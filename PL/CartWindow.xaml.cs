using BlApi;
using BlImplementation;
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
            if (myCart.Items != null)
            {
              //  orderItems = IEnumerableToObservable(myCart.Items!);//save the catalog collection from BO in PO obsv collec
               // PList.DataContext = orderItems;//set data context of orderItem list as the orderItems
            }
            else
            {
             //   PList.DataContext = null;
            }
            // Subtotal.DataContext = cart;//set subtotal data context to our cart
        }
    }
}
