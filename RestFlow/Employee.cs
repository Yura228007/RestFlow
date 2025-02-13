using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow
{
    internal class Employee : Human
    {
        public int Salary {  get; set; }
        public string Post {  get; set; }

        public Employee(string login, string password, string name, string surname, DateTime birthday, bool gender, string phone, int salary)
            : base(login, password, name, surname, birthday, gender, phone)
        {
            Salary = salary;
        }
    }
}
