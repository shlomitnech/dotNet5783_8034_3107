using BlApi;
using BO;
using System;
using System.Collections.Generic;
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
using BlImplementation;
using System.Diagnostics;


namespace PL
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private IBl bl = new Bl();
        private BO.Product product = new BO.Product();

        public ProductWindow() //if adding a new product
        {
            InitializeComponent();
            bl = new Bl();
            CategoryBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
            updateProduct.Visibility = Visibility.Collapsed;//update invisible 
            ID.Text = bl.Product?.NextID().ToString();
            

        }
        public ProductWindow(Product prodForList) //if updating an existing product
        {
            InitializeComponent();
            bl = new Bl();
            CategoryBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));//set combobox values to enums
            addProduct.Visibility = Visibility.Collapsed;//make the add invisible
            updateProduct.Visibility = Visibility.Visible;//show update button
            instock1.Text = prodForList.InStock.ToString();
            CategoryBox.SelectedItem = prodForList.Category;
            price.Text = prodForList.Price.ToString();
            name1.Text = prodForList.Name;
            ID.Text = prodForList.ID.ToString();

        }
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Product.AddProduct(product);
            }
            catch(BO.Exceptions ex) 
            {
               new ErrorWindow("Add Product Window\n", ex.Message).ShowDialog();
            }
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
            try
            {
                e.Handled = new Regex("[^A-Z]+[^a-z]+[^A-Z]+[^a-z]+").IsMatch(name1.Text);
            }
            catch (BO.EntityNotFound exc)
            {
                new ErrorWindow("Product List View Window\n", exc.Message).ShowDialog();
            }
        }
        private void instock1_previewtextinput(object sender, RoutedEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(instock1.Text);

        }
        private void name1_TextChanged(object sender, TextChangedEventArgs e) //to add/update name
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
        private void id_TextChanged(object sender, RoutedEventArgs e) //to add/update price
        {
            if (price != null && price.Text != "")
            {
                if (int.TryParse(ID.Text, out int val))
                {
                    product.ID = val;
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
