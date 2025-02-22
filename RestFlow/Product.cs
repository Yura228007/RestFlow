using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Identity.Client;

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

        public Product(DB.Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Type = product.Type;
        }
    }
}
