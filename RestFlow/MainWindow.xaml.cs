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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RestMenef;
using DB;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace RestFlow
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            bool hasAdmin = DB.Tables.GetEmployees().Any(emp => emp.Login == "admin");

            if (!hasAdmin)
            {
                DB.Tables.AddEmployee("admin", "admin", "admin", "admin", DateTime.Now, true, "+7 (123) 456 78 90", 150000, "Admin");
            }

            InitializeComponent();
        }
        private void Button_EnterAsWorker_Click(object sender, RoutedEventArgs e)
        {
            // Получение введённых данных
            string login = TextBox_LoginUser.Text.Trim();     // Логин (без пробелов)
            string password = TextBox_PasswordUser.Text.Trim(); // Пароль

            // Проверка на пустые поля
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль");
                return;  // Прерывание выполнения
            }

            try
            {
                //получение массива Users

                List<DB.Employee> employees = new List<DB.Employee> { };

                DB.Employee? employee = employees.FirstOrDefault(u => u.Login == login);

                if (employee == null)  // Если пользователь не найден
                {
                    MessageBox.Show("Пользователь не найден");
                    return;
                }

                if (employee.Password != password)
                {
                    MessageBox.Show("Неверный пароль");
                    return;
                }

                // Успешная авторизация
                MessageBox.Show("Авторизация успешна!");
                OpenEmployeeWindow(employee.Post);  // Открытие основного окна
            }
            catch (Exception ex)  // Обработка ошибок
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void Button_EnterUser_Click(object sender, RoutedEventArgs e)
        {
            // Получение введённых данных
            string login = TextBox_LoginUser.Text.Trim();     // Логин (без пробелов)
            string password = TextBox_PasswordUser.Text.Trim(); // Пароль

            // Проверка на пустые поля
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль");
                return;  // Прерывание выполнения
            }

            try
            {
                //получение массива Users

                List<DB.User> users = new List<DB.User> { };

                DB.User? user = users.FirstOrDefault(u => u.Login == login);

                if (user == null)  // Если пользователь не найден
                {
                    MessageBox.Show("Пользователь не найден");
                    return;
                }

                if (user.Password != password)
                {
                    MessageBox.Show("Неверный пароль");
                    return;
                }

                // Успешная авторизация
                MessageBox.Show("Авторизация успешна!");
                OpenUserWindow();  // Открытие основного окна
            }
            catch (Exception ex)  // Обработка ошибок
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private static Window? IdentifyTheEmployee(string post)
        {
            switch (post)
            {
                case "Manager":
                    Manager_Window window_manager = new Manager_Window();
                    return window_manager;
                case "Waiter":
                    Waiter_Window window_waiter = new Waiter_Window();
                    return window_waiter;
                case "Accountant":
                    Manager_Window window_accountant = new Manager_Window();
                    return window_accountant;
                case "Admin":
                    Waiter_Window window_admin = new Waiter_Window();
                    return window_admin;
                case "Kitchen":
                    Manager_Window window_kitchen = new Manager_Window();
                    return window_kitchen;
            }
            return null;
        }

        private void OpenEmployeeWindow(string post)
        {
            var window = IdentifyTheEmployee(post);
            if (window != null)
            {
                window.Show();
                this.Visibility = Visibility.Hidden;
            }
            else
            {
                this.Close();             
            }
        }

        private void OpenUserWindow()
        {
            var WindowUser = new User_Window();
            WindowUser.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void Button_LoginUser_Click(object sender, RoutedEventArgs e)
        {
            Panel_RegistUser.Visibility = Visibility.Hidden;
            Panel_Login.Visibility = Visibility.Visible;
        }

        private void Button_RegistUser_Click(object sender, RoutedEventArgs e)
        {
            Panel_Login.Visibility = Visibility.Hidden;
            Panel_RegistUser.Visibility = Visibility.Visible;
        }

        private void Button_FinishRegist_Click(object sender, RoutedEventArgs e)
        {
            string? login = TextBox_RegistUserLogin.Text;
            string? password = TextBox_RegistUserPassword.Text; 
            string? name = TextBox_RegistUserName.Text;
            string? surename = TextBox_RegistUserSurename.Text;
            DateTime? dateTime = DatePicker_RegistUserBirthday.SelectedDate;
            bool? gender = null;
            string? phoneNumber = TextBox_RegistUserTelephoneNumber.Text;
            if (RadioButton_RegistUserMale.IsChecked == true)
            {
                gender = true;
            }
            else if (RadioButton_RegistUserFemale.IsChecked == true)
            {
                gender = false;
            }
            if (login != null && password != null && name != null && surename != null && dateTime != null && gender != null && phoneNumber != null)
            {
                DB.Tables.AddUser(login, password, name, surename, Convert.ToDateTime(dateTime), Convert.ToBoolean(gender), phoneNumber);
            }
        }

    }
}
