//Insert, delete,update: 
//    products(warehouse),dish,order, employee
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;

public class Address
{
    public int? Id { get; set; }
    public string Street { get; set; }
    public string Building { get; set; }
    public string Apartment { get; set; }
    public Address() { }
    public Address(string street, string building, string apartment)
    {
        Street = street;
        Building = building;
        Apartment = apartment;
    }
    public override string ToString()
    {
        return $"Ул.{this.Street}, д.{this.Building}, кв.{this.Apartment}";
    }
}
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public Product() { }
    public Product(string name, string type) { Name = name; Type = type; }
    public override string ToString()
    {
        return $"ID: {this.Id}; Name: {this.Name}; Type: {this.Type};\n";
    }
}
public class Dish
{
    private Dictionary<int, int> Recipe { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Primecost { get; set; }
    public double Price { get; set; }
    public Dish() { }
    //public Dish(Dictionary<int, int> recipe) { Recipe = recipe; }
    public Dish(string name, string description, double primecost, double price) { Name = name; Description = description; Primecost = primecost; Price = price; }
}
public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public Address? Address { get; set; }
    public Order() { }
    public Order(int user_id, DateTime date, Address address) { UserId = user_id; OrderDate = date; Address = address; }
    public Order(int user_id, DateTime date) { UserId = user_id; OrderDate = date;}

}
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public double Salary { get; set; }
    public string Post { get; set; }
    public Employee() { }
    public Employee(string name, string surname, double salary, string post) { Name = name; Surname = surname; Post = post; Salary = salary; }
}
public class BDUser
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public BDUser() { }
    public BDUser(string name, string surname, string phone) { Name = name; Surname = surname; Phone = phone; }
}
public class Warehouse
{
    public int? Id { get; set; }
    public int ProductId { get; set; }
    public int ProductQuantity { get; set; }
    public Warehouse() { }
    public Warehouse(int product_id, int product_quan) { ProductId = product_id; ProductQuantity = product_quan; }
}
public class Ingredient
{
    public int? Id { get; set; }
    public int ProductId { get; set; }
    public int ProductQuantity { get; set; }
    public int DishId { get; set; }
    public Ingredient() { }
    public Ingredient(int product_id, int product_quantity, int dish_id) { ProductId = product_id; ProductQuantity = product_quantity; DishId = dish_id; }
}

public class OrdersDishes
{
    public int Id { get; set; }
    public int DishId { get; set; }
    public int Quantity { get; set; }
    public OrdersDishes() { }
    public OrdersDishes(int dish_id, int quantity)
    {
        DishId = dish_id;
        Quantity = quantity;
    }

}

public class ProjContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<BDUser> BDUsers { get; set; }
    public DbSet<Warehouse> Warehouse { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<OrdersDishes> Compound { get; set; }
    public ProjContext()
    {

        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-1QJA7UJ\SQLEXPRESS;Database=Restaraunt_Flow;Trusted_Connection=True;TrustServerCertificate=True");

        }
    }

}
public class Program
{
    static void Main(string[] args)
    {
        //AddProduct("Beef", "Meat and meat food");
        //UpdateProduct(4, new Product("Juice", "Drinks"));
        //DeleteProduct(5);
        
        Dictionary<int, int> recipe = new Dictionary<int, int> { { 1, 19 }, { 2, 16 } };
        //AddDish("burger", "american food", 50.35, 150.35, recipe);
        Dictionary<int, int> compound = new Dictionary<int, int> { {1,4 } };
        //AddOrder(1, DateTime.Now, compound);
        //DeleteDish(2);
        //DeleteDish(5);
        //DeleteDish(6);
        //DeleteDish(7);
        //GetDishIngregients(4);
        //AddEmployee("Jorge", "Cambridge", 1500.50, "teamlead");
        GetEmployees();
        Console.ReadKey();
    }
    //// CRUD for Product
    static void AddProduct(string name, string type)
    {
        using (ProjContext db = new ProjContext())
        {
            Product product = new Product { Name = name, Type = type };
            db.Products.Add(product);
            db.SaveChanges();
        }
    }
    static void GetProducts()
    {
        using (ProjContext db = new ProjContext())
        {
            foreach (Product product in db.Products)
            {
                Console.WriteLine($"ID: {product.Id} | Name: {product.Name} | Type: {product.Type}");
            }
        }
    }
    static string GetProduct(int id)
    {
        using (ProjContext db = new ProjContext())
        {
            Product product = db.Products.ElementAt(id - 1);
            return $"Name: {product.Name} | Type: {product.Type}";
        }
    }
    static void UpdateProduct(int id, Product product)
    {
        using (ProjContext db = new ProjContext())
        {
            db.Products.Find(id).Name = product.Name;
            db.Products.Find(id).Type = product.Type;
            db.SaveChanges();
        }
    }
    static void DeleteProduct(int id)
    {
        using (ProjContext db = new ProjContext())
        {
            Product product = db.Products.ElementAt(id);
            db.Products.Remove(product);
            db.SaveChanges();
        }
    }


    // CRUD for Dish

    static void GetDish()
    {
        using (ProjContext db = new ProjContext())
        {
            foreach (Dish dish in db.Dishes)
            {
                Console.WriteLine($"ID: {dish.Id} | Name: {dish.Name} | Primecost: {dish.Primecost} | Description:  {dish.Description} | Price:  {dish.Price}");
                Console.WriteLine("Ingredients:");
                GetDishIngregients(dish.Id);
            }


        }
    }
    static string GetDish(int id)
    {
        using (ProjContext db = new ProjContext())
        {
            Dish dish = db.Dishes.ElementAt(id - 1);
            return $"ID: {dish.Id} | Name: {dish.Name} | Primecost: {dish.Primecost} | Description:  {dish.Description} | Price:  {dish.Price}\nIngredients:\n{GetDishComp(id)}";
        }
    }
    static void GetDishIngregients(int id)
    {
        using (ProjContext db = new ProjContext())
        {
            foreach (Ingredient ing in db.Ingredients)
            {
                if (ing.DishId == id)
                {
                    Console.WriteLine($"Product: {GetProduct(ing.ProductId)} | Quantity: {ing.ProductQuantity}");
                }
            }
        }
    }
    static string GetDishComp(int id)
    {
        string str = "";
        using (ProjContext db = new ProjContext())
        {
            foreach (Ingredient ing in db.Ingredients)
            {
                if (ing.DishId == id)
                {
                    str += $"Product: {GetProduct(ing.ProductId)} | Quantity: {ing.ProductQuantity}\n";
                }
            }
        }
        return str;
    }
    static void AddDish(string name, string description, double primecost, double price, Dictionary<int, int> Recipe)
    {
        using (ProjContext db = new ProjContext())
        {
            Dish dish = new Dish(name, description, primecost, price);
            db.Dishes.Add(dish);
            db.SaveChanges();
            foreach (KeyValuePair<int, int> kvp in Recipe)
            {
                //int temp = db.Dishes.ElementAt(db.Dishes.Count() - 1).Id;
                //temp = kvp.Key;
                //temp = kvp.Value;
                Ingredient ing = new Ingredient(kvp.Key, kvp.Value, db.Dishes.ElementAt(db.Dishes.Count() - 1).Id);
                db.Ingredients.Add(ing);
                db.SaveChanges();
                //db.Ingredients.Add(new Ingredient( kvp.Key, kvp.Value, db.Dishes.ElementAt(db.Dishes.Count() - 1).Id));

            }

        }

    }
    static void UpdateDish(int id, Dish dish)
    {
        using (ProjContext db = new ProjContext())
        {
            db.Dishes.Find(id).Name = dish.Name;
            db.Dishes.Find(id).Description = dish.Description;
            db.Dishes.Find(id).Primecost = dish.Primecost;
            db.Dishes.Find(id).Price = dish.Price;
            db.SaveChanges();
        }
    }
    //static void UpdateRecipe(int id, Product product)
    //{
    //    using (ProjContext db = new ProjContext())
    //    {
    //        db.Products.Find(id + 1000).Name = product.Name;
    //        db.Products.Find(id + 1000).Type = product.Type;
    //        db.SaveChanges();
    //    }
    //}
    static void DeleteDish(int id)
    {
        using (ProjContext db = new ProjContext())
        {
            Dish dish = db.Dishes.ElementAt(id - 1);
            foreach (Ingredient ing in db.Ingredients)
            {
                if (ing.DishId == id)
                {
                    db.Ingredients.Remove(ing);
                }
            }
            db.Dishes.Remove(dish);
            db.SaveChanges();
        }
    }

    //// CRUD for Order

    static void GetOrderCompound(int id)
    {
        using (ProjContext db = new ProjContext())
        {
            foreach (OrdersDishes comp in db.Compound)
            {
                if (comp.Id == id)
                {
                    Console.WriteLine(GetDish(comp.DishId));
                }
            }


        }
    }
    static void GetOrders()
    {
        using (ProjContext db = new ProjContext())
        {
            foreach (Order ord in db.Orders)
            {
                Console.WriteLine($"ID: {ord.Id} | Client: {ord.UserId} | Date: {ord.OrderDate}");
                GetOrderCompound(ord.Id);
            }
        }
    }
    static void AddOrder(int userId, DateTime date, Dictionary<int, int> compound, Address address)
    {
        using (ProjContext db = new ProjContext())
        {
            Order ord = new Order(userId, date, address);
            db.Orders.Add(ord);
            db.SaveChanges();
            foreach (KeyValuePair<int, int> kvp in compound)
            {
                OrdersDishes dish = new OrdersDishes(kvp.Key, kvp.Value);
                db.Compound.Add(dish);
            }
            db.SaveChanges();
        }
    }
    static void AddOrder(int userId, DateTime date, Dictionary<int, int> compound)
    {
        using (ProjContext db = new ProjContext())
        {
            Order ord = new Order(userId, date);
            db.Orders.Add(ord);
            db.SaveChanges();
            foreach (KeyValuePair<int, int> kvp in compound)
            {
                OrdersDishes dish = new OrdersDishes(kvp.Key, kvp.Value);
                db.Compound.Add(dish);
            }
            db.SaveChanges();
        }
    }
    static void DeleteOrder(int id)
    {
        using (ProjContext db = new ProjContext())
        {
            Order ord = db.Orders.Find(id);
            db.Orders.Remove(ord);
            db.SaveChanges();
        }
    }


    //CRUD for User

    static void GetUsers()
    {
        using (ProjContext db = new ProjContext())
        {
            foreach (BDUser user in db.BDUsers)
            {
                Console.WriteLine($"ID: {user.Id} | Name: {user.Name} | Surname: {user.Surname} | Phone: {user.Phone}\n");
            }
        }
    }
    static void AddUser(string name, string surname, string phone)
    {
        using (ProjContext db = new ProjContext())
        {
            BDUser user = new BDUser(name, surname, phone);
            db.BDUsers.Add(user);
            db.SaveChanges();
        }
    }
    static void DeleteUser(int id)
    {
        using (ProjContext db = new ProjContext())
        {
            BDUser user = db.BDUsers.Find(id);
            db.BDUsers.Remove(user);
            db.SaveChanges();
        }
    }
    static void UpdateUser(int id, BDUser user)
    {
        using (ProjContext db = new ProjContext())
        {
            db.BDUsers.Find(id).Name = user.Name;
            db.BDUsers.Find(id).Surname = user.Surname;
            db.BDUsers.Find(id).Phone = user.Phone;
        }
    }

    //CRUD for Employee

    static void GetEmployees()
    {
        using (ProjContext db = new ProjContext())
        {
            foreach (Employee emp in db.Employees)
            {
                Console.WriteLine($"ID: {emp.Id} | Name: {emp.Name} | Surname: {emp.Surname} | Salary: {emp.Salary} | Post: {emp.Post}");
            }
        }
    }
    static void AddEmployee(string name, string surname, double salary, string post)
    {
        using (ProjContext db = new ProjContext())
        {
            Employee emp = new Employee(name, surname, salary, post);
            db.Employees.Add(emp);
            db.SaveChanges();
        }
    }
    static void DeleteEmployee(int id)
    {
        using (ProjContext db = new ProjContext())
        {
            Employee emp = db.Employees.Find(id);
            db.Employees.Remove(emp);
            db.SaveChanges();
        }

    }
    static void UpdateEmployee(int id, Employee employee)
    {
        using (ProjContext db = new ProjContext())
        {
            db.Employees.Find(id).Name = employee.Name;
            db.Employees.Find(id).Surname = employee.Surname;
            db.Employees.Find(id).Post = employee.Post;
            db.Employees.Find(id).Salary = employee.Salary;
        }
    }
}
