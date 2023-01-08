using BlApi;
using BlImplementation;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private IBl bl = new Bl();
        private BO.Product prod = new BO.Product();
        public Window1()// add constructor
        {
            InitializeComponent();
            IBl bl2 = new Bl(); 
            CategoryBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));//set combobox values to enums
            updateButton.Visibility = Visibility.Collapsed;//update invisible 
        }
        public Window1(ProductForList ProdForList)// add constructor
        {
            InitializeComponent();
            IBl bl2 = new Bl();
            CategoryBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));//set combobox values to enums
            addButton.Visibility = Visibility.Collapsed;//add invisible
            updateButton.Visibility = Visibility.Visible;//show update
            tid.Text = productForList.ID.ToString();
            tid.IsReadOnly = true;//cant change id in update 
        }

        private void tid_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tname_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tprice_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void tinstock_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
