using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow
{
    public class Manager : Employee
    {
        public Manager(string login, string password, string name, string surname, DateTime birthday, bool gender, string phone, int salary)
            : base(login, password, name, surname, birthday, gender, phone, salary)
        {
            Post = "Менеджер";
        }
        public Manager(Employee employee) : base(employee.Login, employee.Password, employee.Name, employee.Surname, employee.Birthday, employee.Gender, employee.Phone, employee.Salary)
        {
            Post = "Менеджер";
        }
    }
}
