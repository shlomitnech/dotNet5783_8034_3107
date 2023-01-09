using BlApi;
using BlImplementation;
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
using UIAutomationClientsideProviders;
using PL;

namespace PL;

/// <summary>
/// Interaction logic for Window1.xaml
/// </summary>
public partial class Window1 : Window
{
    private IBl bl = new Bl();
    private Product prod = new BO.Product();
    public Window1()// add constructor
    {
        InitializeComponent();
        IBl bl2 = new Bl(); 
        CategoryBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));//set combobox values to enums
      //  updateButton.Visibility = Visibility.Collapsed;//update invisible 
    }
    public Window1(ProductForList ProdForList)// add constructor
    {
        InitializeComponent();
        IBl bl2 = new Bl();
        CategoryBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));//set combobox values to enums
        addButton.Visibility = Visibility.Collapsed;//add invisible
      //  updateButton.Visibility = Visibility.Visible;//show update
      //  tid.Text = productForList.ID.ToString();
        tid.IsReadOnly = true;//cant change id in update 
    }

    private void tid_TextChanged(object sender, TextChangedEventArgs e)
    {
       // e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);//only gets numbers for id

    }

    private void tname_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (tname != null && tname.Text != "")
        {
            prod.Name = tname.Text;
        }
    }

    private void tprice_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (tprice != null && tprice.Text != "")
        {
            if (int.TryParse(tprice.Text, out int val))
            {
                prod.Price = val;
            }
        }
    }
    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        prod.Category = (BO.Enums.Category)CategoryBox.SelectedItem;
    }

    private void tinstock_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (tinstock != null && tinstock.Text != "")
        {
            if (int.TryParse(tinstock.Text, out int val))
            {
                prod.InStock = val;
            }
        }
    }

    private void addButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            bl!.Product.AddProduct(prod);//add product to BO
        }
        catch (BO.IncorrectInput ex)//IncorrectInput error on the screen 
        {
            new ErrorWindow("Add Product Window\n", ex.Message).ShowDialog();
         
        }
        catch (BO.IdExistException ex)//IdExistException error on the screen 
        {
            new ErrorWindow("Add Product Window\n", ex.Message).ShowDialog();
     
        }
       //POP up the messages below
        tid.Text = "Enter ID";
        tname.Text = "Enter Name";
        tprice.Text = "Enter Price";
        tinstock.Text = "Enter Amount";
        Close();
    }

    private void updateButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            bl!.Product.UpdateProduct(prod);//add product to BO
        }
        catch (BO.IncorrectInput ex)//IncorrectInput error on the screen 
        {
            new ErrorWindow("Add Product Window\n", ex.Message).ShowDialog();
        }
        catch (BO.EntityNotFound ex) 
        {
            new ErrorWindow("Add Product Window\n", ex.Message).ShowDialog();

        }
        //POP up the messages below
        tid.Text = "Enter ID";
        tname.Text = "Enter Name";
        tprice.Text = "Enter Price";
        tinstock.Text = "Enter Amount";
        Close();
    }

    private void tid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        tid.Clear();
    }

    private void tname_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        tname.Clear();
    }

    private void tprice_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        tprice.Clear();
    }

    private void tinstock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        tinstock.Clear();
    }

  
}
