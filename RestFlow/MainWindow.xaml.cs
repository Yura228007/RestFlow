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

namespace RestFlow
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DbContext _context;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_EnterUser_Click(object sender, RoutedEventArgs e)
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

                List<User> users = new List<User> { };

                User user = users.FirstOrDefault(u => u.Login == login);

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
                OpenMainApplicationWindow();  // Открытие основного окна
            }
            catch (Exception ex)  // Обработка ошибок
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void OpenMainApplicationWindow()
        {
            var WindowUser = new User_Window();
            WindowUser.Show();
            this.Visibility = Visibility.Hidden;
        }
    }
}
