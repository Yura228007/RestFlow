using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace RestFlow
{
    internal class Order
    {
        public int Id { get; set; }
        public int? Table { get; set; }
        public Dictionary<Dish, int>? List { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsActive { get; set; }
        public double TotalPrice { get; set; }
        public double PrimeCost { get; set; }

        public Order (DateTime orderDate, bool isActive, Address? address = null, int? table = null)
        {
            OrderDate = orderDate;
            IsActive = isActive;
            Table = table;
        }

        public Order (DB.Order order)
        {
            Id = order.Id;
            OrderDate = order.OrderDate;
            Table = order.Table;
            IsActive = order.IsActive;
            List = DB.Tables.GetOrderCompound(order.Id).ToDictionary
                (
                kvp => new RestFlow.Dish(kvp.Key), 
                kvp => kvp.Value
                );
            CalculateTotalPrice();
            CalculatePrimeCost();
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

        public override string ToString()
        {
            return $"{Table} столик";
        }
    }
}
