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
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }

        public Product(string name, double price, string type)
        {
            Name = name;
            Price = price;
            Type = type;
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
