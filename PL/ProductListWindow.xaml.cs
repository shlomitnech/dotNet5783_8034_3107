using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BO;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : System.Windows.Window
    {
        DataSource ds = new(); //to enable DataSource to call its constructors
        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.OrderForList ords = new();
        ObservableCollection<BO.ProductForList> productsForList = new();
        ObservableCollection<BO.OrderForList> ordersForList = new();
        public ProductListWindow(BlApi.IBl? Mainbl)
        {
            InitializeComponent();
            try { ProductItemGrid.ItemsSource = bl?.Product.GetProductForList(); }

            catch (BO.Exceptions ex)//List is empty
            {
                new ErrorWindow("ERROR in List View Window\n", ex.Message).ShowDialog();
            }
            try { OrderItemGrid.ItemsSource = bl?.Order.GetAllOrderForList(); }
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
                    ProductItemGrid.ItemsSource = bl?.Product.GetProductForList();//original list with no filter
                    CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));//show all of combobox options
                    ProductItemGrid.DataContext = productsForList;

                    return;
                }


                if (productCategory is BO.Enums.Category cat) //show the items in that category
                {
                    ProductItemGrid.ItemsSource = bl?.Product.GetProductForList()?.Select(x => x!.Category == cat);
                }

                ProductItemGrid.ItemsSource = from product in bl?.Product.GetProductForList()
                                              where product.Category == productCategory
                                              select product;
            }
            catch (BO.Exceptions ex)
            {
                new ErrorWindow("ERROR in List View Window\n", ex.Message).ShowDialog();
            }
            ProductItemGrid.DataContext = productsForList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow().ShowDialog();
            try
            {
                ProductItemGrid.ItemsSource = bl.Product.GetProductForList();
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
        private void Product_updates(object sender, MouseButtonEventArgs e)
        {
            if (ProductItemGrid.SelectedItem is BO.ProductForList productForList)
            {
                BO.Product prod = new BO.Product();
                prod = bl.Product.ManagerProduct(productForList.ID);
                new ProductWindow(prod).ShowDialog();
            }
            ProductItemGrid.ItemsSource = bl?.Product.GetProductForList(); // update list view after add
        }
        private void Orders_updates(object sender, MouseButtonEventArgs e)
        {
            new OrderUpdate(ords, bl).ShowDialog(); //call the update Order button

        }
    }
}


