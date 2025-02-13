using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow
{
    internal abstract class Human
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public bool Gender { get; set; }
        public string Phone { get; set; }

        protected Human(string login, string password, string name, string surname, DateTime birthday, bool gender, string phone)
        {
            Login = login;
            Password = password;
            Name = name;
            Surname = surname;
            Birthday = birthday;
            Gender = gender;
            Phone = phone;
        }
    }
}
