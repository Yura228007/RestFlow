using Microsoft.EntityFrameworkCore;
namespace DB
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Apartment { get; set; }
        public Address() { }
        public Address(string street, string building, string apartment)
        {
            Street = street;
            House = building;
            Apartment = apartment;
        }
        public override string ToString()
        {
            return $"Ул.{this.Street}, д.{this.House}, кв.{this.Apartment}";
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
        public Product(Product product)
        {
            this.Name = product.Name;
            this.Type = product.Type;
            this.Price = product.Price;
        }
        public override string ToString()
        {
            return $"ID: {this.Id}; Name: {this.Name}; Type: {this.Type}; Price: {this.Price}\n";
        }
    }
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Primecost { get; set; }
        public double Price { get; set; }
        public Dish() { }
        public Dish(Dish dish)
        {
            this.Name= dish.Name;
            this.Price = dish.Price;
            this.Primecost = dish.Primecost;
        }
        public Dish(string name, double primecost, double price) { Name = name; Primecost = primecost; Price = price; }
        public override string ToString()
        {
            return $"ID: {this.Id}; Name: {this.Name}; Primecost: {this.Primecost}; Price: {this.Price};\n";
        }
    }
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsActive { get; set; }
        public Address? Address { get; set; }
        public int? Table {  get; set; }
        public Order() { }
        public Order(Order order)
        {
            this.OrderDate = order.OrderDate;
            this.Address = order.Address;
            this.Table = order.Table;
        }
        public Order(DateTime _date, bool _isActive, Address? _address = null, int? _table = null)
        {
            
            OrderDate = _date;
            IsActive = _isActive;
            Address = _address;
            Table = _table;
        }
        public override string ToString()
        {
            if(Address!=null)
            {
                return $"ID: {this.Id}; Date: {this.OrderDate}; IsActive: {this.IsActive}; Address: {this.Address};\n";
            }
            else if (Table !=null)
            {
                return $"ID: {this.Id}; Date: {this.OrderDate}; IsActive: {this.IsActive}; Table: {this.Table};\n";
            }
            return $"ID: {this.Id}; Date: {this.OrderDate}; IsActive: {this.IsActive};\n";
        }
    }
    public class Employee
    {
        public int Id { get; set; }
        public string Login {  get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public bool Gender { get; set; }
        public string Phone { get; set; }
        public double Salary { get; set; }
        public string Post { get; set; }
        public Employee() { }
        public Employee(Employee employee)
        {
            this.Login = employee.Login;
            this.Password = employee.Password;
            this.Name = employee.Name;
            this.Surname = employee.Surname;
            this.Birthday = employee.Birthday;
            this.Gender = employee.Gender;
            this.Phone = employee.Phone;
            this.Salary = employee.Salary;
            this.Post = employee.Post;
        }
        public Employee(string login, string password,string name, string surname, DateTime birthday, bool gender, string phone, double salary, string post) 
        { 
            Login = login; 
            Password = password; 
            Name = name; 
            Surname = surname; 
            Birthday = birthday; 
            Gender = gender; 
            Phone = phone; 
            Post = post; 
            Salary = salary; 
        }
        public override string ToString()
        {
            return $"ID: {this.Id}; Login: {this.Login}; Password: {this.Password}; Name: {this.Name}; Surname: {this.Surname}; Birthday: {this.Birthday}; IsMale: {this.Gender}; Phone: {this.Phone}; Salary: {this.Salary}; Post: {this.Post};\n";
        }
    }
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public bool Gender {  get; set; }
        public string Phone { get; set; }
        public User() { }
        public User(User user)
        {
            this.Login = user.Login;
            this.Password = user.Password;
            this.Name = user.Name;
            this.Surname = user.Surname;
            this.Birthday = user.Birthday;
            this.Gender = user.Gender;
            this.Phone = user.Phone;
        }
        public User(string login, string password, string name, string surname, DateTime birthday, bool gender, string phone) { Login = login; Password = password; Name = name; Surname = surname; Birthday = birthday; Gender = gender; Phone = phone; }
        public override string ToString()
        {
            return $"ID: {this.Id}; Login: {this.Login}; Password: {this.Password}; Name: {this.Name}; Surname: {this.Surname}; Birthday: {this.Birthday}; IsMale: {this.Gender}; Phone: {this.Phone};\n";
        }
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
        public DbSet<User> Users { get; set; }
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
                optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Restaraunt_Flow;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

    }
    public class Tables
    {
        public static void NoMain()
        {
            Product p = GetProduct(1);
            Console.WriteLine(p.ToString());
        }
        public static void AddProduct(string name, string type,double price)
        {
            using (ProjContext db = new ProjContext())
            {
                Product product = new Product { Name = name, Type = type, Price = price};
                db.Products.Add(product);
                db.SaveChanges();
            }
        }
        public static List<Product> GetProducts()
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
        public static Product GetProduct(int id)
        {
            using (ProjContext db = new ProjContext())
            {
                Product product = db.Products.ElementAt(id - 1);
                return product;
                //return $"Name: {product.Name} | Type: {product.Type}";
            }
        }
        public static void UpdateProduct(int id, Product product)
        {
            using (ProjContext db = new ProjContext())
            {
                Product? _product = db.Products.Find(id);
                if (_product != null)
                {
                    _product = new Product(product);
                }
                db.SaveChanges();
            }
        }
        public static void DeleteProduct(int id)
        {
            using (ProjContext db = new ProjContext())
            {
                Product? product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
            }
        }


        // CRUD for Dish

        public static List<Dish> GetDishes()
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
        public static Dish GetDish(int id)
        {
            using (ProjContext db = new ProjContext())
            {
                Dish dish = db.Dishes.ElementAt(id - 1);
                return dish;
            }
        }
        public static Dictionary<Product,int> GetDishIngregients(int id)
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
        public static void AddDish(string name, string description, double primecost, double price, Dictionary<int, int> Recipe)
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
        public static void UpdateDish(int id, Dish dish)
        {
            using (ProjContext db = new ProjContext())
            {
                Dish? _dish = db.Dishes.Find(id);
                if (_dish != null)
                {
                    _dish = new Dish(dish);
                }
                db.SaveChanges();
            }
        }
        public static void DeleteDish(int id)
        {
            using (ProjContext db = new ProjContext())
            {
                Dish? dish = db.Dishes.Find(id);
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

        public static Dictionary<Dish,int> GetOrderCompound(int id)
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
        public static List<Order> GetOrders()
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
        public static Order GetOrder(int id)
        {
            using(ProjContext db = new ProjContext())
            {
                return db.Orders.ElementAt(id-1);
            }
        }
        public static void AddOrder(int userId, DateTime date, Dictionary<int, int> compound, Address? address = null)
        {
            using (ProjContext db = new ProjContext())
            {
                Order ord = new Order(date,true, address);
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
        public static void UpdateOrder(int id, Order order)
        {
            using (ProjContext db = new ProjContext())
            {
                Order? _order = db.Orders.Find(id);
                if (_order != null)
                {
                   _order =  new Order(order);
                }
                db.SaveChanges();
            }
        }
        public static void DeleteOrder(int id)
        {
            using (ProjContext db = new ProjContext())
            {
                Order? ord = db.Orders.Find(id);
                db.Orders.Remove(ord);
                db.SaveChanges();
            }
        }


        //CRUD for User

        public static List<User> GetUsers()
        {
            List<User> users = new List<User>();
            using (ProjContext db = new ProjContext())
            {
                foreach (User user in db.Users)
                {
                    users.Add(user);
                    Console.WriteLine($"ID: {user.Id} | Name: {user.Name} | Surname: {user.Surname} | Phone: {user.Phone}\n");
                }
            }
            return users;
        }
        public static User GetUser(int id)
        {
            using (ProjContext db = new ProjContext())
            {
                return db.Users.ElementAt(id-1);
            }
        }
        public static void AddUser(string login, string password, string name, string surname, DateTime birthday, bool gender, string phone)
        {
            using (ProjContext db = new ProjContext())
            {
                User user = new User(login, password, name, surname, birthday, gender, phone);
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
        public static void DeleteUser(int id)
        {
            using (ProjContext db = new ProjContext())
            {
                User? user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }
        public static void UpdateUser(int id, User user)
        {
            using (ProjContext db = new ProjContext())
            {
                User? _user = db.Users.Find(id);
                if( _user != null )
                {
                    _user = new User(user);
                }
            }
        }

        //CRUD for Employee

        public static List<Employee> GetEmployees()
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
        public static Employee GetEmployee(int id)
        {
            using(ProjContext db = new ProjContext())
            {
                return db.Employees.ElementAt(id-1);
            }
        }
        public static void AddEmployee(string login, string password, string name, string surname, DateTime birthday, bool gender,  string phone, double salary, string post)
        {
            using (ProjContext db = new ProjContext())
            {
                Employee emp = new Employee(login, password, name, surname, birthday, gender, phone, salary, post);
                db.Employees.Add(emp);
                db.SaveChanges();
            }
        }
        public static void DeleteEmployee(int id)
        {
            using (ProjContext db = new ProjContext())
            {
                Employee? emp = db.Employees.Find(id);
                db.Employees.Remove(emp);
                db.SaveChanges();
            }

        }
        public static void UpdateEmployee(int id, Employee employee)
        {
            using (ProjContext db = new ProjContext())
            {
                Employee? _employee = db.Employees.Find(id);
                if (_employee != null)
                {
                    _employee.Name = employee.Name;
                    _employee.Surname = employee.Surname;
                    _employee.Post = employee.Post;
                    _employee.Birthday = employee.Birthday;
                    _employee.Phone = employee.Phone;
                    _employee.Gender = employee.Gender;
                    _employee.Password = employee.Password;
                    _employee.Salary = employee.Salary;
                    db.SaveChanges();
                }
                else
                {
                    // Обрабатываем случай, если сотрудник с указанным ID не найден
                    throw new ArgumentException("Сотрудник не найден");
                }
            }
        }
        public static Dictionary<Product, int> GetWarehouse()
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

