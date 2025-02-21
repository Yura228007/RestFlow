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
                DB.Employee emp = new DB.Employee("admin", "admin", "admin", "admin", DateTime.Now, true, "71234567890", 150000, "Admin");
                DB.Tables.AddEmployee(emp);
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

                List<DB.Employee> employees = DB.Tables.GetEmployees();

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

                List<DB.User> users = DB.Tables.GetUsers();

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
                    Accountant_Window window_accountant = new Accountant_Window();
                    return window_accountant;
                case "Admin":
                    Admin_Window window_admin = new Admin_Window();
                    return window_admin;
                case "Kitchen":
                    Kitchen_Window window_kitchen = new Kitchen_Window();
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
            string? surname = TextBox_RegistUserSurename.Text;
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
            if (login != null && password != null && name != null && surname != null && dateTime != null && gender != null && phoneNumber != null)
            {
                DB.User tempUser = new DB.User(login, password, name, surname, Convert.ToDateTime(dateTime), (bool)gender, phoneNumber);

                DB.Tables.AddUser(tempUser);

                MessageBox.Show("Вы успешно зарегестрировались");
            }
            else
            {
                MessageBox.Show("Заполните все поля!");
            }
        }

        private void EnterAsAdmin_Click(object sender, RoutedEventArgs e)
        {
            TextBox_LoginUser.Text = "admin";
            TextBox_PasswordUser.Text = "1234";
            Button_LoginUser_Click(sender, e);
        }
    }
}
