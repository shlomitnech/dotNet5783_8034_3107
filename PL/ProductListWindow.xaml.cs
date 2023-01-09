using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BlApi;
using BlImplementation;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window // changed from Window
    {
        private IBl bl = new Bl();
        public ProductListWindow()
        {
            InitializeComponent();
            ProductsListView.ItemsSource = bl.Product.GetProductForList();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        }
     //   private void AddButton_Click(object sender, RoutedEventArgs e) => new ProductWindow().Show();

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Enums.Category productCategory = (BO.Enums.Category)CategorySelector.SelectedItem; // saves the selected category
            if (productCategory == BO.Enums.Category.NoCategory) // if the user would like to view all the products
            {
                ProductsListView.ItemsSource = bl.Product.GetProductForList();
                CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
                return;
            }
            if (productCategory is BO.Enums.Category cat)
            {
                //show the filtered list
                ProductsListView.ItemsSource = bl?.Product.GetProductForList()?.Select(x => x!.Category == cat);


            }
            ProductsListView.ItemsSource = from product in bl?.Product.GetProductForList()
                                           where product.Category == productCategory
                                           select product;
        }

        private void ProductsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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

    }
}


