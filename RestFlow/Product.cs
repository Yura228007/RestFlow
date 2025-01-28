using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RestFlow
{
    internal class Product
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }

        public Product(string name, DateTime date, double price)
        {
            Name = name;
            Date = date;
            Price = price;
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
