using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BO;
using BlApi;
using BlImplementation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Dal;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        DataSource ds = new(); //to enable DataSource to call its constructors
        private IBl bl = new Bl();
        public ProductListWindow()
        {
            InitializeComponent();
            try { ProductsListView.ItemsSource = bl.Product.GetProductForList(); }
            catch (BO.Exceptions ex)//List is empty
            {
                new ErrorWindow("ERROR in List View Window\n", ex.Message).ShowDialog();
            }

                CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Enums.Category productCategory = (BO.Enums.Category)CategorySelector.SelectedItem; // saves the selected category
            try
            {
                if (productCategory == BO.Enums.Category.NoCategory) // Show all the products without a filter
                {
                    ProductsListView.ItemsSource = bl.Product.GetProductForList();
                    CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
                    return;
                }
                if (productCategory is BO.Enums.Category cat) //show the items in that category
                {
                    ProductsListView.ItemsSource = bl?.Product.GetProductForList()?.Select(x => x!.Category == cat);
                }

                ProductsListView.ItemsSource = from product in bl?.Product.GetProductForList()
                                               where product.Category == productCategory
                                               select product;
            }
            catch (BO.Exceptions ex)
            {
                new ErrorWindow("ERROR in List View Window\n", ex.Message).ShowDialog();
            }
            }

        private void DoubleClickEvent(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ProductsListView.SelectedItem is BO.ProductForList productForList)
            {
                BO.Product prod = new BO.Product();
                prod = bl.Product.ManagerProduct(productForList.ID);
                new ProductWindow(prod).ShowDialog();
            }
            ProductsListView.ItemsSource = bl?.Product.GetProductForList(); // update list view after add
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow().ShowDialog();
            try
            {
                ProductsListView.ItemsSource = bl.Product.GetProductForList();
            }
            catch (BO.Exceptions ex) //if list is empty
            {
                new ErrorWindow("ERROR in List View Window\n", ex.Message).ShowDialog();

            }
        }

            private void ProductsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            BO.Enums.Category productCategory = (BO.Enums.Category)CategorySelector.SelectedItem; // saves the selected category
            if (productCategory == BO.Enums.Category.NoCategory) // Show all the products without a filter
            {
                ProductsListView.ItemsSource = bl.Product.GetProductForList();
                CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
                return;
            }
            if (productCategory is BO.Enums.Category cat) //show the items in that category
            {
                ProductsListView.ItemsSource = bl?.Product.GetProductForList()?.Select(x => x!.Category == cat);
            }
            ProductsListView.ItemsSource = from product in bl?.Product.GetProductForList()
                                           where product.Category == productCategory
                                           select product;
            */
        }
    }
}


