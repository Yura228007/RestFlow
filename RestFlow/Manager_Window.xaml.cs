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

namespace RestMenef
{
    /// <summary>
    /// Логика взаимодействия для Manager_Window.xaml
    /// </summary>
    public partial class Manager_Window : Window
    {
        List<RestFlow.Product> products;

        public Manager_Window()
        {
            InitializeComponent();
            LoadProducts();
            
        }

        private void DataGrid_OrdersHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void LoadProducts()
        {
            products = DB.Tables.GetProducts().Select(e => new RestFlow.Product(e)).ToList();
            List_Products.ItemsSource = products.ToList();
        }
    }
}
