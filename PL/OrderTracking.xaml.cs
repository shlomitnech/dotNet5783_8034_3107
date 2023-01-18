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
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderTracking.xaml
    /// </summary>
    public partial class OrderTracking : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Order order = new BO.Order();
        public OrderTracking()
        {
            InitializeComponent();
        }

        private void backButtonClick(object sender, RoutedEventArgs e)
        {
            new MainWindow().ShowDialog();
        }

        private void id_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void id_previewmousedown(object sender, RoutedEventArgs e)
        {
            id.clear();
        }

        private void id_PreviewTextInput(object sender, RoutedEventArgs e)
        {
            if(id != null && id.Text != "")
            {
                order.ID = id;
            }
        }
    }
}
