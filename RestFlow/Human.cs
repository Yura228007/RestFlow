using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow
{
    public abstract class Human : INotifyPropertyChanged
    {
        private string _login;
        private string _password;
        private string _name;
        private string _surname;
        private DateTime _birthday;
        private bool _gender;
        private string _phone;

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }

        public DateTime Birthday
        {
            get => _birthday;
            set
            {
                _birthday = value;
                OnPropertyChanged();
            }
        }

        public bool Gender
        {
            get => _gender;
            set
            {
                _gender = value;
                OnPropertyChanged();
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }

        public Human(
            string login,
            string password,
            string name,
            string surname,
            DateTime birthday,
            bool gender,
            string phone)
        {
            Login = login;
            Password = password;
            Name = name;
            Surname = surname;
            Birthday = birthday;
            Gender = gender;
            Phone = phone;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string FullName()
        {
            return Surname + " " + Name;
        }
    }
}
