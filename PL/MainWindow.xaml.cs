﻿using BlApi;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
       static IBl bl = BlApi.Factory.Get()!; 
        BO.Cart myCart = new();
       
        public MainWindow()  //constructor
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) //Admin View
        {
            new ProductListWindow(bl).Show();
            Close();
        }

        private void NewOrderButton_Click_1(object sender, RoutedEventArgs e)   //opens catalog
        {
            new Catalog(myCart, bl!).Show();
            Close();
        }

        private void OrderTrackingButton_Click_2(object sender, RoutedEventArgs e)   //goes to order tracking window
        {
            new OrderTracking().Show();
        }
    }
}
