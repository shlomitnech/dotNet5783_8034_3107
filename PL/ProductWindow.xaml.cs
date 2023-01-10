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
using BO;


namespace PL
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        public ProductWindow()
        {
            InitializeComponent();
        }

        private void name_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void name_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {

        }
        private void name1_PreviewMouseDown(object sender, RoutedEventArgs e)
        {
            name1.Clear();
        }
        private void instock1_PreviewMouseDown(object sender, RoutedEventArgs e)
        {
            instock1.Clear();
        }
        private void price_PreviewMouseDown(object sender, RoutedEventArgs e)
        {
            price.Clear();
        }
        private void name1_TextChanged(object sender, RoutedEventArgs e)
        {

        }
        private void name1_previewtextinput(object sender, RoutedEventArgs e)
        {

        }
        private void instock1_previewtextinput(object sender, RoutedEventArgs e)
        {

        }

        private void SelectCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
