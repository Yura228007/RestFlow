using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using RestFlow;

namespace RestMenef
{
    /// <summary>
    /// Логика взаимодействия для Admin_Window.xaml
    /// </summary>
    public partial class Admin_Window : Window
    {
        private class MainViewModel
        {
            public ObservableCollection<Employee> Employees { get; set; }
            public MainViewModel()
            {
                Employees = new ObservableCollection<Employee>
                {

                };
            }
        }
        public Admin_Window()
        {
            InitializeComponent();
        }
    }
}
