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
        int selectedEmployeeId = -1;


        public class MainViewModel : INotifyPropertyChanged
        {
            private ObservableCollection<RestFlow.Employee> _employees;
            public ObservableCollection<RestFlow.Employee> Employees
            {
                get => _employees;
                set
                {
                    _employees = value;
                    OnPropertyChanged();
                }
            }

            public MainViewModel()
            {
                Employees = new ObservableCollection<RestFlow.Employee>();
                Employees.CollectionChanged += Employees_CollectionChanged;
                _ = LoadEmployeesAsync();
            }

            private async void Employees_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                // Добавление
                if (e.NewItems != null)
                {
                    foreach (RestFlow.Employee newEmp in e.NewItems)
                    {
                        DB.Employee newDBEmployee = DB.Tables.GetEmployees().FirstOrDefault(employee => employee.Login == newEmp.Login);
                        DB.Tables.AddEmployee(newDBEmployee);
                        //добавление
                    }
                }

                // Удаление
                if (e.OldItems != null)
                {
                    foreach (RestFlow.Employee oldEmp in e.OldItems)
                    {
                        //удаление
                        DB.Employee oldDBEmployee = DB.Tables.GetEmployees().FirstOrDefault(employee => employee.Login == oldEmp.Login);
                        DB.Tables.DeleteEmployee(oldDBEmployee.Id);
                    }
                }
            }

            private async Task LoadEmployeesAsync()
            {
                try
                {
                    using (var context = new DB.ProjContext())
                    {
                        var employees = await context.Employees
                            .Select(e => new RestFlow.Employee(e))
                            .ToListAsync();
                        Employees = new ObservableCollection<RestFlow.Employee>(employees);
                    }
                }
                catch (Exception ex)
                {
                    // Логирование ошибки или уведомление пользователя
                    MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
                }
            }

            public event PropertyChangedEventHandler? PropertyChanged;
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private void DataGrid_WorkersInfo_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var editedEmployee = e.Row.Item as RestFlow.Employee;
                if (editedEmployee != null)
                {
                    DB.Employee? ed = DB.Tables.GetEmployees().FirstOrDefault(x => x.Login == editedEmployee.Login);
                    if (ed != null)
                    {
                        DB.Tables.UpdateEmployee(ed.Id, 
                            new DB.Employee(
                                editedEmployee.Login, 
                                editedEmployee.Password, 
                                editedEmployee.Name, 
                                editedEmployee.Surname, 
                                editedEmployee.Birthday, 
                                editedEmployee.Gender, 
                                editedEmployee.Phone, 
                                editedEmployee.Salary, 
                                editedEmployee.Post
                                )
                            );
                        MessageBox.Show("Изменения успешно сохранены");
                    }

                }
            }
        }

        public Admin_Window()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            Label_AllInfo.Content = "";
        }

        #region Хуета
        private void DataGrid_WorkersInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedEmployeeRow = DataGrid_WorkersInfo.SelectedItem as DataRowView;



            if (selectedEmployeeRow != null)
            {
                string selectedEmployeeLogin = (string)selectedEmployeeRow[0];

                if (selectedEmployeeLogin != null)
                {
                    DB.Employee? tempEmployee = DB.Tables.GetEmployees().FirstOrDefault(e => e.Login == selectedEmployeeLogin);
                    if (tempEmployee != null)
                    {   
                        selectedEmployee = new RestFlow.Employee(tempEmployee);
                        selectedEmployeeId = tempEmployee.Id;
                    }
                }
            }

            if (selectedEmployee != null)
            {
                FillingEmployeeInformation(selectedEmployee);
            }
        }

        private void FillingEmployeeInformation(RestFlow.Employee selectedEmployee)
        {
            TextBox_WorkerLogin.Text = selectedEmployee.Login;
            TextBox_NewWorkerPassword.Text = selectedEmployee.Password;
            TextBox_WorkerName.Text = selectedEmployee.Name;
            TextBox_WorkerSurname.Text = selectedEmployee.Surname;
            TextBox_WorkerSalary.Text = Convert.ToString(selectedEmployee.Salary);
            TextBox_WorkerTelephoneNumber.Text = selectedEmployee.Phone;
            if (selectedEmployee.Gender == true)
            {
                RadioButton_WorkerMale.IsChecked = true;
            }
            else
            {
                RadioButton_WorkerFemale.IsChecked = true;
            }
            DatePicker_WorkerBirthday.SelectedDate = selectedEmployee.Birthday;
        }

        private void CleanFieldsWorker()
        {
            TextBox_WorkerLogin.Text = "";
            TextBox_NewWorkerPassword.Text = "";
            TextBox_WorkerName.Text = "";
            TextBox_WorkerSurname.Text = "";
            TextBox_WorkerSalary.Text = "";
            TextBox_WorkerTelephoneNumber.Text = "";
            RadioButton_WorkerMale.IsChecked = false;
            RadioButton_WorkerFemale.IsChecked = false;
            DatePicker_WorkerBirthday.SelectedDate = null;
        }

        private void Button_SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            string? name = TextBox_WorkerName.Text;
            string? surname = TextBox_WorkerSurname.Text;
            string? login = TextBox_WorkerLogin.Text;
            string? password = TextBox_WorkerPassword.Text;
            string? phone = TextBox_NewWorkerTelephoneNumber.Text;
            string? salaryString = TextBox_NewWorkerSalary.Text;
            DateTime? birthday = DatePicker_WorkerBirthday.SelectedDate;
            int salaryInt;
            bool flag = Int32.TryParse(salaryString, out salaryInt);
            bool? gender = null;
            if (RadioButton_WorkerFemale.IsChecked == true || RadioButton_WorkerMale.IsChecked == true) 
            {
                if (RadioButton_WorkerMale.IsChecked == true)
                {
                    gender = true;
                }
                else
                {
                    gender = false;
                }
            }
            string? post = ComboBox_WorkerPost.Text;

            if (name != null && surname != null && login != null && password != null && phone != null && birthday != null && salaryString != null && gender != null && post != null) 
            {
                if (flag)
                {
                    DB.Tables.UpdateEmployee(selectedEmployeeId, new DB.Employee(login, password, name, surname, (DateTime)birthday, (bool)gender, phone, salaryInt, post));
                    MessageBox.Show("Изменения успешно сохранены!");
                    CleanFieldsWorker();
                }
                else
                {
                    MessageBox.Show("Для поля 'зарплата' используйте только цифры");
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
                    CleanFieldsWorker();
                }
            }
            else
            {
                MessageBox.Show("Выберите работника из списка");
            }
        }
        #endregion
    }
}
