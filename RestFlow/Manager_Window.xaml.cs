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
using RestFlow;

namespace RestMenef
{
    /// <summary>
    /// Логика взаимодействия для Manager_Window.xaml
    /// </summary>
    public partial class Manager_Window : Window
    {
        List<RestFlow.Product> products;
        List<RestFlow.Dish> dishes;
        List<RestFlow.Order> orders;
        Dictionary<RestFlow.Product, int> warehouse;
        RestFlow.Dish? selectedDish;
        RestFlow.Employee currentEmployee;
        public Manager_Window(RestFlow.Employee employee)
        {
            InitializeComponent();
            LoadProducts();
            LoadWarehouse();
            LoadDishes();
            currentEmployee = employee;
            Label_AllInfo.Content = currentEmployee.ToString();
            TextBox_InfoPassword.Text = currentEmployee.Password;
            TextBox_InfoLogin.Text = currentEmployee.Login;
        }


        private void DataGrid_OrdersHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                
        }

        private void LoadProducts()
        {
            products = DB.Tables.GetProducts().Select(e => new RestFlow.Product(e)).ToList();
            List_Products.ItemsSource = products;
            ComboBox_Products.ItemsSource = products;
        }

        private void LoadWarehouse()
        {
            Dictionary<DB.Product, int> _warehouse = DB.Tables.GetWarehouse();
            if (_warehouse != null && _warehouse.Count > 0)
            {
                warehouse = _warehouse.ToDictionary(kvp => new RestFlow.Product(kvp.Key), kvp => kvp.Value);
            }
        }

        private void LoadDishes()
        {
            dishes = DB.Tables.GetDishes().Select(e => new RestFlow.Dish(e)).ToList();
            List_Dishes.ItemsSource = dishes;
        }

        private void LoadOrders()
        {
            orders = DB.Tables.GetOrders().Select(e => new RestFlow.Order(e)).ToList();
            DataGrid_OrdersHistory.ItemsSource = orders;
        }

        #region all interaction with products
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
            if (TextBox_NewProductName.Text != "")
            {
                DB.Product? isHave = DB.Tables.GetProducts().FirstOrDefault(x => x.Name == TextBox_NewProductName.Text);
                if (isHave == null)
                {
                    DB.Product tempProductDB = new DB.Product(TextBox_NewProductName.Text, "неизвестный тип", 0);
                    DB.Tables.AddProduct(tempProductDB);
                    int hisId = DB.Tables.GetProducts().FirstOrDefault(x => x.Name == tempProductDB.Name).Id;
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
            else
            {
                MessageBox.Show("Укажите имя продукта");
            }
        }
        #endregion

        #region all interaction with dishes
        private void CleanTextBoxDish()
        {
            TextBox_DishPrice.Text = string.Empty;
            TextBox_QuantityProductsForDish.Text = string.Empty;
            TextBox_NewDishName.Text = string.Empty;
        }

        private void List_Dishes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List_Dishes.SelectedItem != null)
            {
                string? input = List_Dishes.SelectedItem.ToString();
                if (input != null)
                {
                    int lastDashIndex = input.LastIndexOf('—') - 1;
                    string result = lastDashIndex != -1 ? input.Substring(0, lastDashIndex) : input;
                    DB.Dish? dish = DB.Tables.GetDishes().FirstOrDefault(x => x.Name == result);
                    if (dish != null)
                    {
                        selectedDish = new RestFlow.Dish(dish);
                        ComboBox_Ingredients.ItemsSource = selectedDish.Ingredients.Keys;
                        TextBox_DishPrice.Text = Convert.ToString(selectedDish.Price);
                    }
                }
            }
            else
            {
                selectedDish = null;
            }
        }

        private void Button_ShowIngredients_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDish != null)
            {
                string tempLine = "Состав:";
                foreach (var x in DB.Tables.GetDishIngregients(selectedDish.Id))
                {
                    tempLine += $"\n{x.Key.Name} - {x.Value}";
                }
                MessageBox.Show(tempLine);
            }
            else
            {
                MessageBox.Show("Выберите блюдо из списка");
            }
        }

        // Метод для конвертации
        private static DB.Product ConvertToDbProduct(RestFlow.Product product)
        {
            return new DB.Product
            {
                Id = product.Id,
                Name = product.Name,
                Type = product.Type,
                Price = product.Price
            };
        }

        private void Button_DeleteIngredient_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDish != null)
            {
                if (ComboBox_Ingredients.Text != "")
                {
                    Dictionary<RestFlow.Product, int> tempRecipe = selectedDish.Ingredients;
                    RestFlow.Product? selectedProduct = tempRecipe.Keys.FirstOrDefault(e => e.Name == ComboBox_Ingredients.Text);
                    if (selectedProduct != null)
                    {

                        selectedDish.RemoveIngredient(selectedProduct);
                        DB.Dish tempDishDb = new DB.Dish(selectedDish.Name, selectedDish.PrimeCost, selectedDish.Price);
                        Dictionary<DB.Product, int> tempRecipeDB = tempRecipe.ToDictionary(kvp  => ConvertToDbProduct(kvp.Key), kvp => kvp.Value);
                        DB.Tables.UpdateDish(selectedDish.Id, tempDishDb, tempRecipeDB);
                        LoadDishes();
                        CleanTextBoxDish();
                        MessageBox.Show("Ингредиент успешно удален");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите какой ингредиент вы хотите удалить");
                }
            }
            else
            {
                MessageBox.Show("Выберите блюдо");
            }
        }

        private void Button_AddNewDish_Click(object sender, RoutedEventArgs e)
        {
            string? name = TextBox_NewDishName.Text;
            if (name!= null && name!="")
            {
                if (dishes.FirstOrDefault(e => e.Name == name) == null)
                {
                    DB.Dish tempDish = new DB.Dish(name, 0, 0);
                    DB.Tables.AddDish(name, 0, 0, new Dictionary<DB.Product, int> { });
                    LoadDishes();
                    CleanTextBoxDish();
                    MessageBox.Show("Блюдо успешно добавлено. Отредактируйте его поля");
                }
                else
                {
                    MessageBox.Show("Такое блюдо уже существует");
                }
            }
            else
            {
                MessageBox.Show("Введите имя для блюда");
            }
        }

        private void Button_AddProductForDish_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDish != null)
            {
                string? nameProduct = ComboBox_Products.Text;
                int quantity;
                string? quantityString = TextBox_QuantityProductsForDish.Text;
                bool flag = Int32.TryParse(quantityString, out quantity);
                if (nameProduct != null && quantityString != null)
                {
                    if (flag)
                    {
                        if (products.FirstOrDefault(e => e.Name == nameProduct) != null)
                        {
                            selectedDish.AddIngredient(products.FirstOrDefault(e => e.Name == nameProduct), quantity);
                            DB.Dish tempDishDb = new DB.Dish(selectedDish.Name, selectedDish.PrimeCost, selectedDish.Price);
                            Dictionary<DB.Product, int> tempRecipe = selectedDish.Ingredients.ToDictionary(kvp => ConvertToDbProduct(kvp.Key), kvp => kvp.Value);
                            DB.Tables.UpdateDish(selectedDish.Id, tempDishDb, tempRecipe);
                            MessageBox.Show("Продукт успешно добавлен");
                            LoadDishes();
                            CleanTextBoxDish();
                        }
                        else
                        {
                            MessageBox.Show("Возникла ошибка");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введите корректное значение для количества");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите продукт и выберите количество");
                }
            }
            else
            {
                MessageBox.Show("Выберите блюдо");
            }
        }

        private void Button_DeleteDish_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDish != null)
            {
                MessageBoxResult result = MessageBox.Show("Вы хотите продолжить?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes) 
                {
                    DB.Tables.DeleteDish(selectedDish.Id);
                    MessageBox.Show("Удаление завершено");
                    LoadDishes();
                    CleanTextBoxDish();
                }
            }
            else
            {
                MessageBox.Show("Выберите блюдо для удаления");
            }
        }

        private void Button_Dish_SavePrice_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDish != null)
            {
                string? priceString = TextBox_DishPrice.Text;
                double price;
                if (priceString != null)
                {
                    bool flag = Double.TryParse(priceString, out price);
                    if (flag)
                    {
                        DB.Dish tempDish = new DB.Dish(selectedDish.Name, selectedDish.PrimeCost, price);
                        DB.Tables.UpdateDish(selectedDish.Id, tempDish);
                        LoadDishes();
                        CleanTextBoxDish();
                        MessageBox.Show("Изменения успешно сохранены");
                    }
                    else
                    {
                        MessageBox.Show("Введите корректное значение для цены");
                    }
                }
            }
        }
        #endregion

        private void Button_LogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
