//Insert, delete,update: 
//    products(warehouse),dish,order, employee
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
namespace DataBase
{
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
        public double Price { get; set; }
        public Product() { }
        public Product(string name, string type, double price) { Name = name; Type = type; }
        public override string ToString()
        {
            return $"ID: {this.Id}; Name: {this.Name}; Type: {this.Type};\n";
        }
    }
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Primecost { get; set; }
        public double Price { get; set; }
        public Dish() { }
        //public Dish(Dictionary<int, int> recipe) { Recipe = recipe; }
        public Dish(string name, double primecost, double price) { Name = name; Primecost = primecost; Price = price; }
    }
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Address? Address { get; set; }
        public int? Table {  get; set; }
        public Order() { }
        public Order(DateTime _date, Address? _address = null, int? _table = null)
        {
            
            OrderDate = _date;
            Address = _address;
            Table = _table;
        }
        

    }
    public class Employee
    {
        public int Id { get; set; }
        public string Login {  get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public double Salary { get; set; }
        public string Post { get; set; }
        public Employee() { }
        public Employee(string login, string password,string name, string surname, string phone, double salary, string post) { Login = login; Password = password; Name = name; Surname = surname; Post = post; Salary = salary; }
    }
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public User() { }
        public User(string login, string password, string name, string surname, string phone) { Login = login; Password = password; Name = name; Surname = surname; Phone = phone; }
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
        public DbSet<User> BDUsers { get; set; }
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
            Dictionary<int, int> compound = new Dictionary<int, int> { { 1, 4 } };
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
        static void AddProduct(string name, string type,double price)
        {
            using (ProjContext db = new ProjContext())
            {
                Product product = new Product { Name = name, Type = type, Price = price};
                db.Products.Add(product);
                db.SaveChanges();
            }
        }
        static List<Product> GetProducts()
        {
            List<Product> _products = new List<Product>();
            using (ProjContext db = new ProjContext())
            {
                foreach (Product product in db.Products)
                {
                    
                    _products.Add(product);
                    Console.WriteLine($"ID: {product.Id} | Name: {product.Name} | Type: {product.Type}");
                }
            }
            return _products;
        }
        static Product GetProduct(int id)
        {
            using (ProjContext db = new ProjContext())
            {
                Product product = db.Products.ElementAt(id - 1);
                return product;
                //return $"Name: {product.Name} | Type: {product.Type}";
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

        static List<Dish> GetDishes()
        {
            List<Dish> _dishes  = new List<Dish>(); 
            using (ProjContext db = new ProjContext())
            {
                foreach (Dish dish in db.Dishes)
                {
                    
                    _dishes.Add(dish);
                }
                return _dishes;
            }
        }
        static Dish GetDish(int id)
        {
            using (ProjContext db = new ProjContext())
            {
                Dish dish = db.Dishes.ElementAt(id - 1);
                return dish;
            }
        }
        static Dictionary<Product,int> GetDishIngregients(int id)
        {
            Dictionary<Product, int> _ingredients = new Dictionary<Product, int>();
            using (ProjContext db = new ProjContext())
            {
                foreach (Ingredient ing in db.Ingredients)
                {
                    if (ing.DishId == id)
                    {
                        _ingredients.Add(GetProduct(ing.ProductId),ing.ProductQuantity);
                    }
                }
            }
            return _ingredients;
        }
        static void AddDish(string name, string description, double primecost, double price, Dictionary<int, int> Recipe)
        {
            using (ProjContext db = new ProjContext())
            {
                Dish dish = new Dish(name,primecost, price);
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

        static Dictionary<Dish,int> GetOrderCompound(int id)
        {
            Dictionary<Dish,int> _compound = new Dictionary<Dish,int>();
            using (ProjContext db = new ProjContext())
            {
                foreach (OrdersDishes comp in db.Compound)
                {
                    if (comp.Id == id)
                    {
                        _compound.Add(GetDish(comp.Id),comp.Quantity);
                        Console.WriteLine(GetDish(comp.DishId));
                    }
                }


            }
            return _compound;
        }
        static List<Order> GetOrders()
        {
            List<Order> _orders = new List<Order>();
            using (ProjContext db = new ProjContext())
            {
                foreach (Order ord in db.Orders)
                {
                    _orders.Add(ord);
                    Console.WriteLine($"ID: {ord.Id}  | Date: {ord.OrderDate}");
                    GetOrderCompound(ord.Id);
                }
            }
            return _orders;
        }
        static Order GetOrder(int id)
        {
            using(ProjContext db = new ProjContext())
            {
                return db.Orders.ElementAt(id-1);
            }
        }
        static void AddOrder(int userId, DateTime date, Dictionary<int, int> compound, Address? address = null)
        {
            using (ProjContext db = new ProjContext())
            {
                Order ord = new Order(date, address);
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

        static List<User> GetUsers()
        {
            List<User> users = new List<User>();
            using (ProjContext db = new ProjContext())
            {
                foreach (User user in db.BDUsers)
                {
                    users.Add(user);
                    Console.WriteLine($"ID: {user.Id} | Name: {user.Name} | Surname: {user.Surname} | Phone: {user.Phone}\n");
                }
            }
            return users;
        }
        static void AddUser(string login, string password, string name, string surname, string phone)
        {
            using (ProjContext db = new ProjContext())
            {
                User user = new User(login, password, name, surname, phone);
                db.BDUsers.Add(user);
                db.SaveChanges();
            }
        }
        static void DeleteUser(int id)
        {
            using (ProjContext db = new ProjContext())
            {
                User user = db.BDUsers.Find(id);
                db.BDUsers.Remove(user);
                db.SaveChanges();
            }
        }
        static void UpdateUser(int id, User user)
        {
            using (ProjContext db = new ProjContext())
            {
                db.BDUsers.Find(id).Login = user.Login;
                db.BDUsers.Find(id).Password = user.Password;
                db.BDUsers.Find(id).Name = user.Name;
                db.BDUsers.Find(id).Surname = user.Surname;
                db.BDUsers.Find(id).Phone = user.Phone;
            }
        }

        //CRUD for Employee

        static List<Employee> GetEmployees()
        {
            List<Employee> _employees = new List<Employee>();
            using (ProjContext db = new ProjContext())
            {
                foreach (Employee emp in db.Employees)
                {
                    _employees.Add(emp);
                    Console.WriteLine($"ID: {emp.Id} | Name: {emp.Name} | Surname: {emp.Surname} | Salary: {emp.Salary} | Post: {emp.Post}");
                }
            }
            return _employees;

        }
        static void AddEmployee(string login, string password, string name, string phone, string surname, double salary, string post)
        {
            using (ProjContext db = new ProjContext())
            {
                Employee emp = new Employee(login, password, name, surname, phone, salary, post);
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
        static Dictionary<Product, int> GetWarehouse()
        {
            Dictionary<Product,int> _warehouse = new Dictionary<Product, int>();
            using (ProjContext db = new ProjContext())
            {
                foreach(Warehouse warehouse in db.Warehouse)
                {
                    _warehouse.Add(GetProduct(warehouse.ProductId), warehouse.ProductQuantity);
                }
            }
            return _warehouse;
        }
    }
}

