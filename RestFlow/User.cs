using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow
{
    internal class User : Human
    {

        public User(string login, string password, string name, string surname, DateTime birthday, bool gender, string phone)
            : base(login, password, name, surname, birthday, gender, phone)
        {
        }
    }
}
