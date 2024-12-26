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
        private static int _lastUid = 0; 
        public int Uid { get; private set; } 
        public Address Address { get; set; }
        public int Table { get; set; }
        public Dictionary<Dish, int> List { get; set; }
        public int HumanAccountable { get; set; }

        public Order(Address address, Dictionary<Dish, int> list, int human)
        {
            Address = address;
            List = list;
            HumanAccountable = human;
            Uid = ++_lastUid;
        }
        public Order(int table, Dictionary<Dish, int> list, int human)
        {
            Table = table;
            List = list;
            HumanAccountable = human;
            Uid = ++_lastUid;
        }
    }
}
