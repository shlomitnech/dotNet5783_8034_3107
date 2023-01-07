using BlApi;
using BlImplementation;
using BO;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for ViewTheList.xaml
    /// </summary>
    public partial class ViewTheList : Window
    {
        private IBl bl = new Bl();
        public ViewTheList()
        {
            InitializeComponent();
            try
            {
                ItemListView.ItemsSource = bl.Product.GetProductForList(); //return the product list from BO
            }
            catch(BO.Exceptions ex)
            {
                new Exception(ex.Message); //Change this!!!
            }
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
        }
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Enums.Category cat = (BO.Enums.Category)ItemListView.SelectedItem; //save the category that was picked
            if (cat == BO.Enums.Category.NoCategory)
            {
                ItemListView.ItemsSource = bl.Product.GetProductForList();//get the whole list 

            }
            if (cat is BO.Enums.Category c)//get the category selected
            {
                ItemListView.ItemsSource = bl.Product.GetProductForList().Select(x => x!.Category == c);//print the list of the filtered items

            }
            ItemListView.ItemsSource = from pro in bl.Product.GetProductForList()
                                       where pro.Category == cat
                                       select pro;
        }

        private void ItemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ItemListView.SelectedItems is ProductForList prod)
            {
                //newWindow....show dialog 
            }
            ItemListView.ItemsSource = bl.Product.GetProductForList();

        }

        private void AddButton(object sender, RoutedEventArgs e)
        {
            //show dialog for the window
            ItemListView.ItemsSource = bl.Product.GetProductForList(); //print all the products

        }


    }
}
