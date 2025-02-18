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

namespace RestFlow
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
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

                List<DB.User> users = new List<DB.User> { };

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
            DB.Program.AddUser(TextBox_RegistUserLogin.Text, TextBox_RegistUserPassword.Text, TextBox_RegistUserName.Text, TextBox_RegistUserSurename.Text, TextBox_RegistUserBirthday.Text, TextBox_RegistUserGender.Text, TextBox_RegistUserTelephoneNumber.Text);
        }
    }
}
