using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using RestFlow;

namespace RestMenef
{
    /// <summary>
    /// Логика взаимодействия для Waiter_Window.xaml
    /// </summary>
    /// 


    public partial class Waiter_Window : Window
    {
        List<RestFlow.Dish> dishes;
        Dictionary<RestFlow.Dish,int> currentCompound = new Dictionary<RestFlow.Dish, int> { };
        List<RestFlow.Order> orders;
        RestFlow.Dish? currentDish = null;
        RestFlow.Employee currentEmployee;
        RestFlow.Order? currentOrder = new RestFlow.Order();

        public Waiter_Window(RestFlow.Employee employee)
        {
            InitializeComponent();
            Task.Run(() => StartUpdateOrdersAsync());
            LoadDishes();
            currentEmployee = employee;
            Label_AllInfo.Content = currentEmployee.ToString();
            TextBox_InfoPassword.Text = currentEmployee.Password;
            TextBox_InfoLogin.Text = currentEmployee.Login;
        }

        #region Loads

        private async Task StartUpdateOrdersAsync()
        {
            while (true)
            {
                await Dispatcher.InvokeAsync(() => LoadActiveOrders());

                await Task.Delay(TimeSpan.FromMinutes(1));
            }
        }

        private void LoadActiveOrders()
        {
            orders = DB.Tables.GetActiveOrderCollection().Select(e => new RestFlow.Order(e)).ToList();
            Application.Current.Dispatcher.Invoke(() =>
            {
                List_ActiveOrders.ItemsSource = orders;
            });
        }

        private void LoadDishes()
        {
            dishes = DB.Tables.GetDishes().Select(e => new RestFlow.Dish(e)).ToList();
            ComboBox_ChooseDish.ItemsSource = dishes.Select(e => e.Name).ToList();
        }

        private void LoadCurrentOrderCompound()
        {
            if (currentOrder != null && currentOrder != new RestFlow.Order())
            {
                currentCompound = currentOrder.List;
                List_CompoundOrder.ItemsSource = currentCompound.Keys.Select(e => e.Name).ToList();
                Label_TotalCost.Content = $"{currentOrder.TotalPrice}";
                if (currentOrder.Table != null)
                {
                    TextBox_TableNumber.Text = $"{currentOrder.Table}";
                }
            }
            else
            {
                currentCompound.Clear();
                List_CompoundOrder.ItemsSource = currentCompound.Keys.Select(e => e.Name).ToList();
                Label_TotalCost.Content = string.Empty;
                currentOrder = new RestFlow.Order();
                currentOrder.List = new Dictionary<RestFlow.Dish, int> { };
            }
        }

        #endregion

        private void List_ActiveOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List_ActiveOrders.SelectedItem != null)
            {
                string? line = List_ActiveOrders.SelectedItem.ToString();
                string? tableString = line.Substring(0,line.IndexOf(" "));
                int currentTable = Int32.Parse($"{tableString}");
                DB.Order? tempBdOrder = DB.Tables.GetOrders().FirstOrDefault(e => (e.IsActive == true && e.OrderTable == currentTable));
                if (tempBdOrder != null)
                {
                    currentOrder = new RestFlow.Order(tempBdOrder);
                }
                else
                {
                    MessageBox.Show("Возникла ошибка");
                }
            }
        }
        
        private void Button_AddDish_Click(object sender, RoutedEventArgs e)
        {
            if (currentOrder != null && currentOrder != new RestFlow.Order() && currentOrder.Id != 0)
            {
                Dictionary<DB.Dish, int> currentCompoundDB = DB.Tables.GetOrderCompound(currentOrder.Id);
                Dictionary<RestFlow.Dish, int> currentCompound = currentCompoundDB.ToDictionary(kvp => new RestFlow.Dish(kvp.Key), kvp => kvp.Value);
                currentOrder.List = currentCompound;
                RestFlow.Dish? tempDish = dishes.FirstOrDefault(e => e.Name == ComboBox_ChooseDish.Text);
                bool isHaveDish = currentOrder.List.Keys.FirstOrDefault(e => e.Name == ComboBox_ChooseDish.Text) != null;
                if (tempDish != null && !isHaveDish)
                {
                    currentOrder.AddDish(tempDish, 1);
                    currentCompound = currentOrder.List;
                    LoadCurrentOrderCompound();
                    TextBox_QuantityDishes.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("Данное блюдо уже есть. Вы можете изменить его количество");
                }
            }
            else
            {
                RestFlow.Dish? tempDish = dishes.FirstOrDefault(e => e.Name == ComboBox_ChooseDish.Text);
                if (tempDish != null)
                {
                    if (currentOrder.List == null) 
                    {
                        currentOrder.List = new Dictionary<RestFlow.Dish, int> { };
                    }
                    currentOrder.AddDish(tempDish, 1);
                    LoadCurrentOrderCompound();
                }
            }
        }
        
        private void Button_EditOrder_Click(object sender, RoutedEventArgs e)
        {
            if (List_ActiveOrders.SelectedItem != null && currentOrder != null)
            {
                LoadCurrentOrderCompound();
                Header_currentOrder.Focusable = true;
                TextBox_TableNumber.IsReadOnly = true;
            }
        }

        private async void Button_SendToKitchen_Click(object sender, RoutedEventArgs e)
        {
            if (currentOrder != null && currentOrder != new RestFlow.Order() && currentOrder.List.Count != 0 && currentOrder.Id != 0)
            {
                Dictionary<DB.Dish, int> tempCompoundDB = currentCompound.ToDictionary(kvp => new DB.Dish(kvp.Key.Name, kvp.Key.PrimeCost, kvp.Key.Price), kvp => kvp.Value);
                foreach (var item in tempCompoundDB.Keys)
                {
                    item.Id = DB.Tables.GetDishes().FirstOrDefault(e => e.Name == item.Name).Id;
                }
                DB.Tables.UpdateOrder(currentOrder.Id, new DB.Order(currentOrder.OrderDate, currentOrder.IsActive, currentOrder.Table, currentOrder.TotalPrice, currentOrder.PrimeCost), tempCompoundDB, currentOrder.TotalPrice, currentOrder.PrimeCost);
                LoadActiveOrders();
                LoadCurrentOrderCompound();
                ResettingCurrentOrderValues();
            }
            else
            {
                string tableString = TextBox_TableNumber.Text;
                int table;
                bool flag = Int32.TryParse(tableString, out table);
                if (flag)
                {
                    DB.Order tempOrderDB = new DB.Order(DateTime.Now, true, table);
                    double totalPrice = currentOrder.TotalPrice;
                    double primeCost = currentOrder.PrimeCost;
                    DB.Tables.AddOrder(DateTime.Now, table, totalPrice, primeCost);
                    int tempId = DB.Tables.GetOrders().Last().Id;
                    Dictionary<DB.Dish, int> tempCompound = currentCompound.ToDictionary(kvp => new DB.Dish(kvp.Key.Name, kvp.Key.PrimeCost, kvp.Key.Price), kvp => kvp.Value);
                    foreach (var item in tempCompound.Keys)
                    {
                        item.Id = DB.Tables.GetDishes().FirstOrDefault(e => e.Name == item.Name).Id;
                    }
                    DB.Tables.AddCompound(tempId, tempCompound, currentOrder.TotalPrice, currentOrder.PrimeCost);
                    LoadActiveOrders();
                    ResettingCurrentOrderValues();
                    LoadCurrentOrderCompound();
                }
            }
        }

        private void Button_FinishOrder_Click(object sender, RoutedEventArgs e)
        {
            if (List_ActiveOrders.SelectedItem != null && currentOrder != null)
            {
                DB.Tables.FinishOrder(currentOrder.Id);
                ResettingCurrentOrderValues();
                LoadActiveOrders();
                LoadCurrentOrderCompound();
                MessageBox.Show("Заказ завершен");
            }
        }

        private void Button_CreateNewOrder_Click(object sender, RoutedEventArgs e)
        {
            ResettingCurrentOrderValues();
        }

        private void ResettingCurrentOrderValues()
        {
            // Сбрасываем текущий заказ
            currentOrder = new RestFlow.Order();
            currentOrder.List = new Dictionary<RestFlow.Dish, int> { };

            // Очищаем текущий состав заказа
            currentCompound = new Dictionary<RestFlow.Dish, int> { };

            // Сбрасываем текущее блюдо
            currentDish = null;

            // Очищаем номер стола
            TextBox_TableNumber.Text = string.Empty;
            TextBox_TableNumber.IsReadOnly = false;

            // Сбрасываем общую стоимость
            Label_TotalCost.Content = string.Empty;

            // Обновляем список блюд в интерфейсе
            List_CompoundOrder.ItemsSource = null;
            List_CompoundOrder.ItemsSource = currentCompound.Keys.Select(e => e.Name).ToList();

            //Изменяем поля текущего блюда
            Label_CurrentDishName.Content = string.Empty;
            TextBox_QuantityDishes.Text = string.Empty;
        }

        private async void List_CompoundOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List_CompoundOrder.SelectedItem != null)
            {
                currentDish = dishes.FirstOrDefault(e => e.Name == List_CompoundOrder.SelectedItem.ToString());
                if (currentDish != null)
                {
                    RestFlow.Dish tempDish = currentOrder.List.Keys.FirstOrDefault(e => e.Name == currentDish.Name);
                    if (tempDish != null)
                    {
                        TextBox_QuantityDishes.Text = $"{currentOrder.List[tempDish]}";
                        Label_CurrentDishName.Content = $"{currentDish.Name}";
                    }
                }
            }
        }

        private void Button_SaveQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (List_CompoundOrder.SelectedItem != null && currentOrder != null)
            {
                string quantityString = TextBox_QuantityDishes.Text;
                int quantity;
                bool flag = Int32.TryParse(quantityString, out quantity);
                if (flag)
                {
                    RestFlow.Dish tempDish = currentOrder.List.Keys.FirstOrDefault(e => e.Name == currentDish.Name);
                    if (tempDish != null)
                    {
                        currentOrder.List[tempDish] = quantity;
                        currentOrder.CalculateTotalPrice();
                        currentOrder.CalculatePrimeCost();
                        TextBox_QuantityDishes.Text = string.Empty;
                        currentDish = null;
                        LoadCurrentOrderCompound();
                    }
                }
                else
                {
                    MessageBox.Show("Введите корректное значение для количества");
                }
            }
        }

        private void Button_LogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
