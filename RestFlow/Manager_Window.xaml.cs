using System;
using System.Collections;
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
using DB;

namespace RestMenef
{
    /// <summary>
    /// Логика взаимодействия для Manager_Window.xaml
    /// </summary>
    public partial class Manager_Window : Window
    {
        List<RestFlow.Product> products;
        Dictionary<RestFlow.Product, int> warehouse;
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
            List_Products.ItemsSource = products;
        }

        private void LoadWarehouse()
        {
            warehouse = DB.Tables.GetWarehouse().ToDictionary(kvp => new RestFlow.Product(kvp.Key), kvp => kvp.Value);
        }

        private void List_Products_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List_Products.SelectedItem != null)
            {
                RestFlow.Product? selectedProduct = products.FirstOrDefault(e => e.Name == List_Products.SelectedItem.ToString());
                if (selectedProduct != null)
                {
                    Label_ProductName.Content = selectedProduct.Name;
                    if (warehouse.TryGetValue(selectedProduct, out int value))
                    {
                        TextBox_QuantityProducts.Text = Convert.ToString(warehouse.GetValueOrDefault(selectedProduct));
                    }
                    TextBox_TypeProducts.Text = selectedProduct.Type;
                    TextBox_PriceProduct.Text = Convert.ToString(selectedProduct.Price);
                }
                else
                {
                    MessageBox.Show("Возникла ошибка. Данный продукт не найден в базе данных.");
                }
            }
        }
    }
}
