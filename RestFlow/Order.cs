using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow
{
    internal class Order
    {
        public Address? Address { get; set; }
        public int? Table { get; set; }
        public Dictionary<Dish, int> List { get; set; }
        public DateTime OrderDate { get; set; }

        public Order (DateTime orderDate, Address? address = null, int? table = null)
        {
            OrderDate = orderDate;
            Address = address;
            Table = table;
        }
    }
}
