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
            LoadWarehouse();
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

        private void Button_SaveProductChanges_Click(object sender, RoutedEventArgs e)
        {
            if (List_Products.SelectedItem != null)
            {
                RestFlow.Product? selectedProduct = products.FirstOrDefault(e => e.Name == List_Products.SelectedItem.ToString());
                if (selectedProduct != null)
                {
                    string? name = selectedProduct.Name;
                    string? type = TextBox_TypeProducts.Text;
                    string? priceString = TextBox_PriceProduct.Text;
                    double price;

                    //получение склада и изменение его количества

                    bool flag = Double.TryParse(priceString, out price);
                    if (name != null && type != null && priceString != null)
                    {
                        if (flag)
                        {
                            DB.Product tempProduct = new DB.Product(name, type, price);
                            MessageBox.Show($"{selectedProduct.Id}");
                            DB.Tables.UpdateProduct(selectedProduct.Id, tempProduct);
                            MessageBox.Show("Изменения успешно сохранены");
                            LoadProducts();
                            CleanTextBoxProduct();
                        }
                        else
                        {
                            MessageBox.Show("Введите корректное значение для цены");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Заполните все поля");
                    }
                }
                else
                {
                    MessageBox.Show("Возникла ошибка. Данный продукт не найден в базе данных.");
                }
            }
        }

        private void CleanTextBoxProduct()
        {
            Label_ProductName.Content = "выберите продукт";
            TextBox_QuantityProducts.Text = string.Empty;
            TextBox_PriceProduct.Text = string.Empty;
            TextBox_TypeProducts.Text = string.Empty;
        }

        private void Button_DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (List_Products.SelectedItem != null)
            {
                RestFlow.Product? selectedProduct = products.FirstOrDefault(e => e.Name == List_Products.SelectedItem.ToString());
                if (selectedProduct != null)
                {
                    DB.Tables.DeleteProduct(selectedProduct.Id);
                    MessageBox.Show("Продукт успешно удален");
                    LoadProducts();
                    CleanTextBoxProduct();
                }
            }
            else
            {
                MessageBox.Show("Выберите продукт из списка");
            }
        }

        private void Button_AddNewProduct_Click(object sender, RoutedEventArgs e)
        {
            DB.Product? isHave = DB.Tables.GetProducts().FirstOrDefault(x => x.Name == TextBox_NewProductName.Text);
            if (isHave == null)
            {
                DB.Product tempProductDB = new DB.Product(TextBox_NewProductName.Text, "неизвестный тип", 0);
                DB.Tables.AddProduct(tempProductDB);
                MessageBox.Show("Продукт успешно добавлен");
                LoadProducts();
                TextBox_NewProductName.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Данный продукт уже существует");
            }
        }
    }
}
