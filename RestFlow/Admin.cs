using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow
{
    internal class Admin : Employee
    {
        public Admin(string login, string password, string name, string surname, DateTime birthday, bool gender, string phone, int salary)
            : base(login, password, name, surname, birthday, gender, phone, salary)
        {
            Post = "Admin";
        }
    }
}
