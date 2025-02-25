using DB;
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
        public Dictionary<Dish, int>? List { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalPrice { get; set; }
        public double PrimeCost { get; set; }

        public Order (DateTime orderDate, Address? address = null, int? table = null)
        {
            OrderDate = orderDate;
            Address = address;
            Table = table;
        }

        public void AddIngredient(Dish dish, int quantity)
        {
            if (dish != null)
            {
                if (List.ContainsKey(dish))
                {
                    List[dish] += quantity; // Увеличиваем количество, если продукт уже есть
                }
                else
                {
                    List[dish] = quantity; // Добавляем новый продукт
                }
                CalculatePrimeCost(); // Пересчитываем себестоимость
                CalculateTotalPrice();
            }
        }

        public void RemoveIngredient(Dish dish)
        {
            if (dish != null)
            {
                if (List.ContainsKey(dish))
                {
                    List.Remove(dish); // Удаляем продукт
                    CalculatePrimeCost(); // Пересчитываем себестоимость
                    CalculateTotalPrice();
                }
            }
        }

        private void CalculatePrimeCost()
        {
            PrimeCost = 0; // Инициализация себестоимости
            foreach (var dish in List)
            {
                foreach (var ingredient in dish.Key.Ingredients)
                {
                    PrimeCost += (ingredient.Key.Price * ingredient.Value) * dish.Value;
                }
            }
        }

        private void CalculateTotalPrice()
        {
            PrimeCost = 0; // Инициализация себестоимости
            foreach (var dish in List)
            {
                PrimeCost += (dish.Key.Price * dish.Value);
            }
        }
    }
}
