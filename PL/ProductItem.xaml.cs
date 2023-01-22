using System.Collections.ObjectModel;
using System.Windows;

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


    public ProductItem(BO.ProductItem prod, BO.Cart myCart, BlApi.IBl b)    //constructor
    {

        InitializeComponent();
        product.Name = prod.Name; //send the name to the chosen product
 

    }

    private void AddToCart_Click(object sender, RoutedEventArgs e) //add this item to cart
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

    private void Button_Click(object sender, RoutedEventArgs e)   //opens catalog
    {
        new Catalog(myCart, bl!).ShowDialog();//go to catalog window
        Close();//close this window

    }

}
