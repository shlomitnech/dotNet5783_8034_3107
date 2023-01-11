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

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private static IBl bl = new Bl();
        ObservableCollection<ProductForList> productList = new();
        public ProductListWindow()
        {
            InitializeComponent();
            IEnumerableToPL(bl!.Product?.GetProductForList());
            ProductsListView.DataContext = productList;
            ProductsListView.ItemsSource = bl.Product.GetProductForList();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        }

        private void IEnumerableToPL(IEnumerable<ProductForList> list)
        {
            productList.Clear();
            foreach (var temp in list)
                productList.Add(temp);
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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
        }

        private void DoubleClickEvent(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ProductsListView.SelectedItem is BO.ProductForList productForList)
            {
                BO.Product prod = new BO.Product();
                prod = bl.Product.GetProductForList(productForList.ID);
                new ProductWindow(prod).ShowDialog();
            }
            ProductsListView.ItemsSource = bl?.Product.GetProductForList(); // update list view after add
        }

        private void Button_Click(object sender, RoutedEventArgs e) => new ProductWindow().Show(); //Show's the product window

        private void ProductsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (CategorySelector.SelectedItem is BO.Enums.Category.NoCategory)
                IEnumerableToPL(bl.Product.GetProductForList()!);
            else if (CategorySelector.SelectedItem is BO.Enums.Category)
                IEnumerableToPL(bl.Product.FilterProductList(p => p.Category == (BO.Category.Enums)CategorySelector.SelectedItem));
            else if (CategorySelector.SelectedItem is "")
                IEnumerableToPL(bl!.Product.GetProductForList()!);
      
        
        }
    }
}


