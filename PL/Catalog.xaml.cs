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

namespace PL
{
    /// <summary>
    /// Interaction logic for Catalog.xaml
    /// </summary>
    public partial class Catalog : Window

        DataSource ds = new (); //to enable DataSource to call its constructors
        BlApi.IBl? bl = BlApi.Factory.Get();
    {
        public Catalog()
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

        private void CategorySelecter_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
    }
}
