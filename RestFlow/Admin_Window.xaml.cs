using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
/*        public class MainViewModel
        {
            public ObservableCollection<Employee> Employees { get; set; }
            public MainViewModel()
            {
                Employees = new ObservableCollection<Employee> (DB.Tables.GetEmployees().Select(e => new Employee(e)).ToList());
            }
        }*/
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
                _ = LoadEmployeesAsync(); // Запуск асинхронной загрузки
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

        public Admin_Window()
        {
            InitializeComponent();
            DataContext = new MainViewModel();

        }
    }
}
