using RestFlow;
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

namespace RestMenef
{
    /// <summary>
    /// Логика взаимодействия для Kitchen_Window.xaml
    /// </summary>
    public partial class Kitchen_Window : Window
    {
        List<RestFlow.Order> orders; 
        RestFlow.Employee currentEmployee;

        public Kitchen_Window(Kitchen employee)
        {
            InitializeComponent();
            currentEmployee = employee;
            Label_AllInfo.Content = currentEmployee.ToString();
            TextBox_InfoPassword.Text = currentEmployee.Password;
            TextBox_InfoLogin.Text = currentEmployee.Login;
            Task.Run(() => StartUpdateOrdersAsync());
        }

        private async Task StartUpdateOrdersAsync()
        {
            while (true)
            {
                await Dispatcher.InvokeAsync(() => LoadActiveOrders());

                await Task.Delay(TimeSpan.FromSeconds(30));
            }
        }

        private void LoadActiveOrders()
        {
            List_Kitchen.Items.Clear();
            List<DB.Order> ordersDB = DB.Tables.GetActiveOrderCollection();
            orders = ordersDB.Select(e => new RestFlow.Order(e)).ToList();
            foreach (var order in orders)
            {
                Dictionary<RestFlow.Dish, int> dishes = order.List;
                foreach (var dish in dishes)
                {
                    List_Kitchen.Items.Add($"{dish.Key.Name} - {dish.Value}");
                }
            }
            List_Kitchen.Items.Refresh();
        }

        private void Button_LogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
