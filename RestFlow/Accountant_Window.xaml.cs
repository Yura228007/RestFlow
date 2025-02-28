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
using System.Windows.Shapes;
using Microsoft.Extensions.Primitives;
using RestFlow;

namespace RestMenef
{
    /// <summary>
    /// Логика взаимодействия для Accountant_Window.xaml
    /// </summary>
    public partial class Accountant_Window : Window
    {
        RestFlow.Employee currentEmployee;
        double TotalPrice;
        double PrimeCost;
        List<RestFlow.Order> orders;
        public Accountant_Window(Accountant employee)
        {
            InitializeComponent();
            currentEmployee = employee;
            Label_AllInfo.Content = currentEmployee.ToString();
            TextBox_InfoPassword.Text = currentEmployee.Password;
            TextBox_InfoLogin.Text = currentEmployee.Login;
        }

        private void LoadOrders(DateTime startDate, DateTime endDate)
        {
            List <DB.Order> ordersDB = DB.Tables.GetOrders(startDate, endDate);
            orders = ordersDB.Select(e => new RestFlow.Order(e)).ToList();
            DataGrid_Acountant.ItemsSource = orders;
        }

        private void Button_GetReport_Click(object sender, RoutedEventArgs e)
        {
            DateTime? startDate = DatePicker_StartData.SelectedDate;
            DateTime? endDate = DatePicker_FinishData.SelectedDate;
            if (startDate != null && endDate != null)
            {
                if (startDate < endDate)
                {
                    LoadOrders((DateTime)startDate, (DateTime)endDate);
                    Calculating(orders);
                    Label_TotalPrice.Content = $"{TotalPrice}";
                    Label_PrimeCost.Content = $"{PrimeCost}";
                }
                else
                {
                    MessageBox.Show("Дата конечная должна быть больше даты начальной");
                }
            }
            else
            {
                MessageBox.Show("Выберите начальную и конечную дату");
            }
        }

        private void Calculating(List <RestFlow.Order> ordersP)
        {
            TotalPrice = 0;
            PrimeCost = 0;
            foreach (var order in ordersP)
            {
                TotalPrice += order.TotalPrice;
                PrimeCost += order.PrimeCost;
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
