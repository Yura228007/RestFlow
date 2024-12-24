using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow
{
    internal class Address
    {
        public string Street { get; set; }
        public string House { get; set; }     
        public string Apartment { get; set; }
        public string Comment { get; set; } 

        public Address(string street, string house, string apartment, string comment)
        {
            Street = street;
            House = house;
            Apartment = apartment;
            Comment = comment;
        }

        public override string ToString()
        {
            return $"{Street}, {House}, {Apartment} - {Comment}";
        }
    }
}
