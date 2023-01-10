using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using BlApi;
using BlImplementation;
using BO;


namespace PL
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private IBl bl = new Bl();
        private BO.Product product = new BO.Product();

        public ProductWindow()
        {
            InitializeComponent();
          //  CategoryBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));

            ProductsListView.ItemsSource = bl.Product.GetProductForList();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Product.AddProduct(product);
            }
            catch(BO.Exceptions ex) 
            { }
            Close();
        }

        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            bl.Product.UpdateProduct(product);
            Close();
        }
        private void name1_PreviewMouseDown(object sender, RoutedEventArgs e) //clear the text that is written
        {
            name1.Clear();
        }
        private void instock1_PreviewMouseDown(object sender, RoutedEventArgs e) //clear the text that is written
        {
            instock1.Clear();
        }
        private void price_PreviewMouseDown(object sender, RoutedEventArgs e) //clear the text that is written
        {
            price.Clear();
        }
        private void name1_previewtextinput(object sender, RoutedEventArgs e)
        {

        }
        private void instock1_previewtextinput(object sender, RoutedEventArgs e)
        {

        }
        private void name1_TextChanged(object sender, RoutedEventArgs e) //to add/update name
        {
            if (name1 != null && name1.Text !="")
            {
                product.Name = name1.Text;
            }
        }
        private void price_TextChanged(object sender, RoutedEventArgs e) //to add/update price
        {
            if (price != null && price.Text != "")
            {
                if (int.TryParse(price.Text, out int val))
                {
                    product.Price = val;
                }
            }
        }
        private void instock1_TextChanged(object sender, RoutedEventArgs e) //to add/update Instock
        {
            if (instock1 != null && instock1.Text != "")
            {
                if (int.TryParse(instock1.Text, out int val))
                {
                    product.InStock = val;
                }
            }
        }

        private void SelectCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Enums.Category category = (BO.Enums.Category)CategoryBox.SelectedItem;
            product.Category = category;
        }

        private void ID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
