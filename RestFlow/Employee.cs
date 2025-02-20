using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow
{
    public class Employee : Human
    {
        public double Salary {  get; set; }
        public string? Post {  get; set; }

        public Employee(string login, string password, string name, string surname, DateTime birthday, bool gender, string phone, double salary)
            : base(login, password, name, surname, birthday, gender, phone)
        {
            Salary = salary;
        }

        public Employee(DB.Employee employee) : base(employee.Login, employee.Password, employee.Name, employee.Surname, employee.Birthday, employee.Gender, employee.Phone)
        {
            Salary = employee.Salary;
            Post = employee.Post;
        }
    }
}
