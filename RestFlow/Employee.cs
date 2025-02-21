using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace RestFlow
{
    public class Employee : Human, INotifyPropertyChanged
    {
        private double _salary;
        private string? _post;

        public double Salary
        {
            get => _salary;
            set
            {
                _salary = value;
                OnPropertyChanged();
            }
        }

        public string? Post
        {
            get => _post;
            set
            {
                _post = value;
                OnPropertyChanged();
            }
        }

        public Employee(
            string login,
            string password,
            string name,
            string surname,
            DateTime birthday,
            bool gender,
            string phone,
            double salary)
            : base(login, password, name, surname, birthday, gender, phone)
        {
            Salary = salary;
        }

        public Employee(DB.Employee employee)
            : base(
                employee.Login,
                employee.Password,
                employee.Name,
                employee.Surname,
                employee.Birthday,
                employee.Gender,
                employee.Phone)
        {
            Salary = employee.Salary;
            Post = employee.Post;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            string line;
            string _gender = Gender ? "мужчина" : "женщина";
            line = @$"Полное имя: {FullName()}
Дата рождения: {Birthday}
Номер телефона: {Phone}
Должность: {Post}
Зарплата: {Salary}
Пол: {_gender}";
            return line;
        }
    }
}
