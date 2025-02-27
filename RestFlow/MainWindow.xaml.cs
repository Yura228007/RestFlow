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

        Employee logEmployee = null;

        public MainWindow()
        {
            bool hasAdmin = DB.Tables.GetEmployees().Any(emp => emp.Login == "admin");

            if (!hasAdmin)
            {
                DB.Employee emp = new DB.Employee("admin", "admin", "admin", "admin", DateTime.Now, true, "71234567890", 150000, "админ");
                DB.Tables.AddEmployee(emp);
            }

            InitializeComponent();
        }
        private void Button_EnterAsWorker_Click(object sender, RoutedEventArgs e)
        {
            string login = TextBox_LoginUser.Text.Trim();   
            string password = TextBox_PasswordUser.Text.Trim(); 

            
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль");
                return;  
            }

            try
            {

                List<DB.Employee> employees = DB.Tables.GetEmployees();

                DB.Employee? employee = employees.FirstOrDefault(u => u.Login == login);

                if (employee == null)  
                {
                    MessageBox.Show("Пользователь не найден");
                    return;
                }

                if (employee.Password != password)
                {
                    MessageBox.Show("Неверный пароль");
                    return;
                }

                MessageBox.Show("Авторизация успешна!");
                logEmployee = new RestFlow.Employee(employee);
                OpenEmployeeWindow(employee.Post); 
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private Window? IdentifyTheEmployee(string post)
        { 
            switch (post.Trim().ToLower())
            {
                case "менеджер":
                    Manager_Window window_manager = new Manager_Window(logEmployee);
                    return window_manager;
                case "официант":
                    Waiter_Window window_waiter = new Waiter_Window(logEmployee);
                    return window_waiter;
                case "бухгалтер":
                    Accountant_Window window_accountant = new Accountant_Window(logEmployee);
                    return window_accountant;
                case "админ":
                case "admin":
                    Admin_Window window_admin = new Admin_Window(logEmployee);
                    return window_admin;
                case "кухонный работник":
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
    }
}
