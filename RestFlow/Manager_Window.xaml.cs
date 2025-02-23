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
            Dictionary<DB.Product, int> _warehouse = DB.Tables.GetWarehouse();
            if (_warehouse != null && _warehouse.Count > 0)
            {
                warehouse = _warehouse.ToDictionary(kvp => new RestFlow.Product(kvp.Key), kvp => kvp.Value);
            }
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
                        TextBox_QuantityProducts.Text = $"{warehouse[selectedProduct]}";
                    }
                    else
                    {
                        MessageBox.Show("Не удалось вытащить количество");
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
                    int quantity;
                    string? quantityString = TextBox_QuantityProducts.Text;
                    bool flagQuantity = Int32.TryParse(quantityString, out quantity);

                    bool flagPrice = Double.TryParse(priceString, out price);
                    if (name != null && type != null && priceString != null)
                    {
                        if (flagPrice)
                        {
                            if (flagQuantity)
                            {
                                DB.Product tempProduct = new DB.Product(name, type, price);
                                DB.Tables.UpdateProduct(selectedProduct.Id, tempProduct);
                                Warehouse tempWarehouse = new Warehouse(selectedProduct.Id, quantity);
                                DB.Tables.UpdateProductInWarehouse(selectedProduct.Id, tempWarehouse);
                                MessageBox.Show("Изменения успешно сохранены");
                                LoadProducts();
                                LoadWarehouse();
                                CleanTextBoxProduct();
                            }
                            else
                            {
                                MessageBox.Show("Введите корректное значение для количества");
                            }
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
                    DB.Tables.DeleteProductToWarehouse(selectedProduct.Id);
                    LoadProducts();
                    CleanTextBoxProduct();
                    LoadWarehouse();
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
                int hisId = DB.Tables.GetProducts().FirstOrDefault(x => x.Name == tempProductDB.Name).Id;
                MessageBox.Show($"{hisId}");
                DB.Tables.AddProductToWarehouse(hisId, 0);
                MessageBox.Show("Продукт успешно добавлен");
                TextBox_NewProductName.Text = string.Empty;
                LoadProducts();
                LoadWarehouse();
            }
            else
            {
                MessageBox.Show("Данный продукт уже существует");
            }
        }
    }
}
