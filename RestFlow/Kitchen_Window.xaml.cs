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
        public Kitchen_Window()
        {
            InitializeComponent();
            Task.Run(() => StartUpdateOrdersAsync());
        }

        private async Task StartUpdateOrdersAsync()
        {
            while (true)
            {
                LoadActiveOrders();

                await Task.Delay(TimeSpan.FromMinutes(1));
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
                ListBox dishBox = new ListBox()
                {
                    ItemsSource = dishes.Select(kvp => ($"{kvp.Key.Name}")).ToList(),
                    Width = 200,
                    BorderThickness = new Thickness(2),
                    BorderBrush = Brushes.Blue,
                    Margin = new Thickness(10)
                };
                List_Kitchen.Items.Add(dishBox);
            }
        }
    }
}
