﻿using System;
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


        public Catalog(BO.Cart cart, BlApi.IBl b)
        {
            InitializeComponent();
            bl = b;//new bl
            myCart = cart;
            try
            {
        //        ProductCatalog = (bl!.Product.GetCatalog()!);//put catalog
            }
            catch (BO.EntityNotFound ex)
            {
                new ErrorWindow("Catalog Window\n", ex.Message).ShowDialog();
            }
            MainScreenCatalog.DataContext = productList;//set data context of catalog as the product list
            CategorySelecter.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));//set combobox values to enums
           // AddProductToCart.Visibility = Visibility.Visible;

        }

        private void CategorySelecter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            BO.Enums.Category productCategory = (BO.Enums.Category)CategorySelecter.SelectedItem; // saves the selected category
            try
            {
                if (productCategory == BO.Enums.Category.NoCategory) // Show all the catalog
                {
                  //  CatalogGrid.ItemsSource = bl?.Product.GetCatalog();//original list with no filter
                 //   CategorySelecter.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
                 //   CatalogGrid.DataContext = productList;

                    return;
                }


                if (productCategory is BO.Enums.Category cat) //show the items in that category
                {
                 //   CatalogGrid.ItemsSource = bl?.Product.GetCatalog()?.Select(x => x!.Category == cat);
                }
                /*
                CatalogGrid.ItemsSource = from product in bl?.Product.GetCatalog()
                                              where product.Category == productCategory
                                              select product;
                */
            }
 
            catch (BO.EntityNotFound ex)
             {
              new ErrorWindow("Catalog Window\n", ex.Message).ShowDialog();
             }
               //CatalogGrid.DataContext = productList;

        }
        private void ProductItemView_click(object sender, MouseButtonEventArgs e)
        {
         //   if (catalogGrid.SelectedItem is BO.ProductItem productItem)
            {
                //new ProductItemView(productItem, myCart, bl!).ShowDialog();
            }
        }
        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            if (/*catalogGrid.SelectedItem is  BO.ProductItem productItem*/ true)
            {
                try
                {
                    //myCart = PL.Tools.CastBoCToPo(bl!.Cart.AddToCart(PL.Tools.CastPoCToBo(myCart), productItem.ID));//add the selected product to cart
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
            }//add the product to cart
        }
        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().ShowDialog();//go to home window 
            Close();//close this window
        }
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {

            new MainWindow().ShowDialog();//go to home window 
            Close();//close this window
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility

            /*
            if (Tg_Btn.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_back.Visibility = Visibility.Collapsed;
                tt_cart.Visibility = Visibility.Collapsed;

            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_cart.Visibility = Visibility.Visible;
                tt_back.Visibility = Visibility.Visible;

            }
        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            //img_bg.Opacity = 1;
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
        //    img_bg.Opacity = 0.3;
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
         //   Tg_Btn.IsChecked = false;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
            */
        }

        private void ShoppingCart_Click(object sender, RoutedEventArgs e)
        {
            //new CartWindow(myCart!, bl!).ShowDialog();//go to cart window 
            Close();//close this window
        }
    }
}

