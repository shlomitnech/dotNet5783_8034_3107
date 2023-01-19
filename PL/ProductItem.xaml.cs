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

namespace PL;

/// <summary>
/// Interaction logic for ProductItem.xaml
/// </summary>
public partial class ProductItem : Window
{

    BlApi.IBl? bl = BlApi.Factory.Get();
    ObservableCollection<BO.ProductItem> productList = new();
    BO.Cart myCart = new();
    private BO.Product product = new BO.Product();


    public ProductItem(BO.ProductItem prod, BO.Cart myCart, BlApi.IBl b)
    { 
        InitializeComponent();
    //    product.ID = prod.ID;

    }

    private void AddToCart_Click(object sender, RoutedEventArgs e)
    {
          try
            {
                myCart = bl!.Cart.AddToCart(myCart, product.ID, 1);

                MessageBox.Show("Product successfully added to your cart.");
            }
            catch (BO.EntityNotFound ex)
            {
                new ErrorWindow("Cart Window Window", ex.Message).ShowDialog();
            }
            catch (BO.UnfoundException ex)
            {
                new ErrorWindow("Cart Window Window", ex.Message).ShowDialog();
            }
            catch (BO.Exceptions ex)
            {
                new ErrorWindow("Cart Window Window", ex.Message).ShowDialog();
            }
            catch (BO.IdExistException ex)
            {
                new ErrorWindow("Cart Window Window", ex.Message).ShowDialog();
           }
    }
}
