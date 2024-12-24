using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFlow
{
    internal class Dish
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double PrimeCost { get; private set; }
        public Dictionary<Product, int> Ingredients { get; private set; }

        public Dish(string name, double price, Dictionary<Product, int> ingredients)
        {
            Name = name;
            Price = price;
            Ingredients = ingredients ?? new Dictionary<Product, int>();
            CalculatePrimeCost();
        }

        public Dish(string name, double price) // Конструктор без ингредиентов
        {
            Name = name;
            Price = price;
            Ingredients = new Dictionary<Product, int>();
            PrimeCost = 0;
        }

        public void AddIngredient(Product product, int quantity)
        {
            if (product != null)
            {
                if (Ingredients.ContainsKey(product))
                {
                    Ingredients[product] += quantity; // Увеличиваем количество, если продукт уже есть
                }
                else
                {
                    Ingredients[product] = quantity; // Добавляем новый продукт
                }
                CalculatePrimeCost(); // Пересчитываем себестоимость
            }
        }

        public void RemoveIngredient(Product product)
        {
            if (product != null)
            {
                if (Ingredients.ContainsKey(product))
                {
                    Ingredients.Remove(product); // Удаляем продукт
                    CalculatePrimeCost(); // Пересчитываем себестоимость
                }
            }
        }

        private void CalculatePrimeCost()
        {
            PrimeCost = 0; // Инициализация себестоимости
            foreach (var ingredient in Ingredients)
            {
                PrimeCost += (ingredient.Key.Price * ingredient.Value);
            }
        }
    }
}
