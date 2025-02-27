using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
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
using DB;
using Microsoft.EntityFrameworkCore;
using RestFlow;

namespace RestMenef
{
    /// <summary>
    /// Логика взаимодействия для Admin_Window.xaml
    /// </summary>
    public partial class Admin_Window : Window
    {
        RestFlow.Employee? selectedEmployee = null;
        List<RestFlow.Employee> employees;
        int selectedEmployeeId = -1;
        RestFlow.Employee currentEmployee;
        List <string> Posts = new List<string> {"админ", "менеджер", "официант", "бухгалтер", "кухонный работник"};

        public Admin_Window(RestFlow.Employee employee)
        {
            InitializeComponent();
            currentEmployee = employee;
            Label_AllInfo.Content = currentEmployee.ToString();
            TextBox_InfoPassword.Text = currentEmployee.Password;
            TextBox_InfoLogin.Text = currentEmployee.Login;
            ComboBox_NewWorkerPost.ItemsSource = Posts;
            LoadEmployees();
        }


        #region добавление работника
        private void Button_AddWorker_Click(object sender, RoutedEventArgs e)
        {
            string? login = TextBox_NewWorkerLogin.Text;
            string? password = TextBox_NewWorkerPassword.Text;
            string? name = TextBox_NewWorkerName.Text;
            string? surname = TextBox_NewWorkerSurname.Text;
            string? phone = TextBox_NewWorkerTelephoneNumber.Text;
            DateTime? date = DatePicker_NewWorkerBirthday.SelectedDate;
            string salaryString = TextBox_NewWorkerSalary.Text;
            double salaryDouble;
            bool flag = Double.TryParse(salaryString, out salaryDouble);
            bool? gender = null;
            if (RadioButton_NewWorkerMale.IsChecked == true)
            {
                gender = true;
            }
            else if (RadioButton_NewWorkerFemale.IsChecked == true)
            {
                gender = false;
            }
            string? post = ComboBox_NewWorkerPost.Text;

            if (login != null && password != null && name != null && surname != null && phone != null && salaryString != null && date != null && gender != null && post != null)
            {
                if (flag == true)
                {
                    DB.Employee tempEmployee = new DB.Employee(login, password, name, surname, (DateTime)date, (bool)gender, phone, salaryDouble, post); 
                    DB.Tables.AddEmployee(tempEmployee);
                    MessageBox.Show("Работник  успешно добавлен");
                    CleanNewWorker();
                    LoadEmployees();
                }
                else
                {
                    MessageBox.Show("введите корректное значение для зарплаты!");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }

        private void CleanNewWorker()
        {
            TextBox_NewWorkerLogin.Text = "";
            TextBox_NewWorkerPassword.Text = "";
            TextBox_NewWorkerName.Text = "";
            TextBox_NewWorkerSurname.Text = "";
            TextBox_NewWorkerTelephoneNumber.Text = "";
            TextBox_NewWorkerSalary.Text = "";
            RadioButton_NewWorkerMale.IsChecked = false;
            RadioButton_NewWorkerFemale.IsChecked = false;
            ComboBox_NewWorkerPost.Text = "";
            DatePicker_NewWorkerBirthday.SelectedDate = null;
        }

        #endregion

        #region взаимодействие с DataGrid
        private void LoadEmployees()
        {
            employees = DB.Tables.GetEmployees().Select(e => new RestFlow.Employee(e)).ToList();
            DataGrid_WorkersInfo.ItemsSource = employees;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.S)
            {
                bool flag = true;
                foreach (var item in DataGrid_WorkersInfo.Items)
                {
                    if (item != null)
                    {
                        DB.Employee? tempBdEmp = DB.Tables.GetEmployees().FirstOrDefault(e => e.Login == (item as RestFlow.Employee).Login);
                        if (tempBdEmp != null)
                        {
                            var tempEmp = item as RestFlow.Employee;
                            if (Posts.Contains(tempEmp.Post))
                            {
                                DB.Employee emp = new DB.Employee(tempEmp.Login, tempEmp.Password, tempEmp.Name, tempEmp.Surname, tempEmp.Birthday, tempEmp.Gender, tempEmp.Phone, tempEmp.Salary, tempEmp.Post);
                                DB.Tables.UpdateEmployee(tempBdEmp.Id, emp);
                                LoadEmployees();
                            }
                            else
                            {
                                flag = false;
                                MessageBox.Show($"Введите у пользователя {tempEmp.FullName()} существующую должность");
                            }
                        }
                    }
                }
                if (flag)
                {
                    MessageBox.Show("Изменения успешно сохранены");
                }
            }
        }
        private void DataGrid_WorkersInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid_WorkersInfo.SelectedItem as RestFlow.Employee != null)
            {
                selectedEmployee = DataGrid_WorkersInfo.SelectedItem as RestFlow.Employee;

                if (selectedEmployee != null)
                {
                    DB.Employee? tempEmployee = DB.Tables.GetEmployees().FirstOrDefault(e => e.Login == selectedEmployee.Login);
                    if (tempEmployee != null)
                    {
                        selectedEmployeeId = tempEmployee.Id;
                    }
                }
            }
        }

        private void Button_Dismiss_Click(object sender, RoutedEventArgs e)
        {
            if (selectedEmployee != null)
            {
                MessageBoxResult result = MessageBox.Show(
                "Вы хотите продолжить?", // Сообщение
                "Подтверждение",         // Заголовок окна
                MessageBoxButton.YesNo,  // Кнопки "Да" и "Нет"
                MessageBoxImage.Question // Иконка вопроса
                );

                if (result == MessageBoxResult.Yes)
                {
                    DB.Tables.DeleteEmployee(selectedEmployeeId);
                    MessageBox.Show("Работник успешно уволен");
                    selectedEmployee = null;
                    LoadEmployees();
                }
            }
            else
            {
                MessageBox.Show("Выберите работника из списка");
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
