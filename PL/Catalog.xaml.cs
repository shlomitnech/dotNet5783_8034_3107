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
using DO;
using BO;
using Dal;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.Windows.Automation;

namespace PL
{
    /// <summary>
    /// Interaction logic for Catalog.xaml
    /// </summary>
    public partial class Catalog : Window
    {

        BlApi.IBl? bl = BlApi.Factory.Get();
        ObservableCollection<BO.ProductItem> productList = new();
        BO.Cart myCart = new();


        public Catalog(BO.Cart cart, BlApi.IBl b) //constructor
        {
            InitializeComponent();
            bl = b;//new bl
            myCart = cart;
            try
            {
              ProductGrid.ItemsSource = bl?.Product.GetCatalog();
            }
            catch (BO.EntityNotFound ex)
            {
                new ErrorWindow("Catalog Window\n", ex.Message).ShowDialog();
            }
            catch (BO.IncorrectInput ex)
            {
                new ErrorWindow("Catalog Window\n", ex.Message).ShowDialog();
            }
            MainScreenCatalog.DataContext = productList;//set data context of catalog as the product list
            CategorySelecter.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));//set combobox values to enums

        }

        /// <summary>
        /// combobox for the categories
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategorySelecter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            BO.Enums.Category productCategory = (BO.Enums.Category)CategorySelecter.SelectedItem; // saves the selected category
            try
            {
                if (productCategory == BO.Enums.Category.NoCategory) // Show all the catalog
                {
                  ProductGrid.ItemsSource = bl?.Product.GetCatalog();//original list with no filter
                  CategorySelecter.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
                  ProductGrid.DataContext = productList;
                    return;
                }

                if (productCategory is BO.Enums.Category cat) //show the items in that category
                {
                  ProductGrid.ItemsSource = bl?.Product.GetCatalog()?.Select(x => x!.Category == cat);
                }
                
                ProductGrid.ItemsSource = from product in bl?.Product.GetCatalog()
                                              where product.Category == productCategory
                                              select product;
               
            }
 
            catch (BO.EntityNotFound ex)
             {
              new ErrorWindow("Catalog Window\n", ex.Message).ShowDialog();
             }

        }
        private void ProductItemView_click(object sender, MouseButtonEventArgs e)
        {
          // if (ProductGrid.SelectedItem is BO.ProductItem prod)
            {
             //   new ProductItem(prod!, myCart, bl!).ShowDialog();
             //   MessageBox.Show("Product successfully added to your cart.");

            }
        }
        private void HomeBtn_Click(object sender, RoutedEventArgs e)  //goes back to main window
        {
            new MainWindow().ShowDialog();//go to home window 
            Close();//close this window
        }
        private void BackBtn_Click(object sender, RoutedEventArgs e)  //goes back to main window
        {

            new MainWindow().ShowDialog();//go to home window 
            Close();//close this window
        }


        private void ShoppingCart_Click(object sender, RoutedEventArgs e)  //goes to shopping cart
        {
            new ShoppingCartWindow(myCart).ShowDialog();//go to cart window 
            Close();//close this window
        }

        private void ProductGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// adds the selected item to the cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddToCart(object sender, RoutedEventArgs e)
        {
            if (ProductGrid.SelectedItem is BO.ProductItem productItem)
            { 
                try
                {
                    myCart = bl!.Cart.AddToCart(myCart, productItem.ID, 1);                   
                    
                    MessageBox.Show("Product successfully added to your cart.");
                }
                catch (BO.EntityNotFound ex)
                {
                    new ErrorWindow("Cart Window ", ex.Message).ShowDialog();
                }
                catch (BO.UnfoundException ex)
                {
                    new ErrorWindow("Cart Window ", ex.Message).ShowDialog();
                }
                catch (BO.Exceptions ex)
                {
                    new ErrorWindow("Cart Window ", ex.Message).ShowDialog();
                }
                catch (BO.IdExistException ex)
                {
                    new ErrorWindow("Cart Window ", ex.Message).ShowDialog();
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)  //goes back to main window
        {
            new MainWindow().ShowDialog();
            Close();
        }
    }
}

