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
        public Product(string name, string type, double price) { Name = name; Type = type; Price = price; }
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
            this.Name = dish.Name;
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
        public int? Table { get; set; }
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
            if (Address != null)
            {
                return $"ID: {this.Id}; Date: {this.OrderDate}; IsActive: {this.IsActive}; Address: {this.Address};\n";
            }
            else if (Table != null)
            {
                return $"ID: {this.Id}; Date: {this.OrderDate}; IsActive: {this.IsActive}; Table: {this.Table};\n";
            }
            return $"ID: {this.Id}; Date: {this.OrderDate}; IsActive: {this.IsActive};\n";
        }
    }
    public class Employee
    {
        public int Id { get; set; }
        public string Login { get; set; }
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
        public Employee(string login, string password, string name, string surname, DateTime birthday, bool gender, string phone, double salary, string post) { Login = login; Password = password; Name = name; Surname = surname; Birthday = birthday; Phone = phone; Gender = gender; Post = post; Salary = salary; }
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
        public bool Gender { get; set; }
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
        public override string ToString()
        {
            return $"ID: {this.Id}; ProductId: {this.ProductId}; Quantity: {ProductQuantity}";
        }
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
        public int OrderId { get; set; }
        public int DishId { get; set; }
        public int Quantity { get; set; }
        public OrdersDishes() { }
        public OrdersDishes(int order_id, int dish_id, int quantity)
        {
            OrderId = order_id;
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
        public static void Main()
        {
            // ADD

            //AddProduct("beef", "meat food", 600.25);
            Dictionary<int, int> prods = new Dictionary<int, int>() { { 1, 16 } };
            //AddDish("chop", 100.5, 250.8, prods);
            Dictionary<int, int> comp = new Dictionary<int, int>() { { 1, 5 } };
            //AddOrder(DateTime.Today, comp, table: 1);
            //AddUser("us", "user", "week", "joohn", DateTime.UtcNow, true, "+79834560126");
            //AddEmployee("admin", "admin", "Rosveld", "Johns", DateTime.UtcNow, true, "+764028565933", 15000, "administrator");

            // GET
            //Product product = GetProduct(1);
            //Console.WriteLine(product.ToString());
            //List<Product> products = GetProducts();
            //foreach (Product product1 in products)
            //{
            //    Console.WriteLine(product1.ToString());
            //}
            //Dish dish = GetDish(1);
            //Console.WriteLine(dish.ToString());
            //List<Dish> dishes = GetDishes();
            //foreach (Dish dish1 in dishes)
            //{
            //    Console.WriteLine(dish1.ToString());
            //}
            //Order order = GetOrder(1);
            //Console.WriteLine(order.ToString());
            //List<Order> orders = GetOrders();
            //foreach (Order order1 in orders)
            //{
            //    Console.WriteLine(order1.ToString());
            //}
            //User user = GetUser(1);
            //Console.WriteLine(user.ToString());
            //List<User> users = GetUsers();
            //foreach (User user1 in users)
            //{
            //    Console.WriteLine(user1.ToString());
            //}
            //Employee employee = GetEmployee(1);
            //Console.WriteLine(employee.ToString());
            //List<Employee> employees = GetEmployees();
            //foreach (Employee employee1 in employees)
            //{
            //    Console.WriteLine(employee1.ToString());
            //}

            // UPDATE 
            //Product p = new Product("meat", "meat food", 50.6);
            //UpdateProduct(1, p);
            //Console.WriteLine(GetProduct(1));

            //Dish d = new Dish("the beefshtacks", 150, 500.27);
            //UpdateDish(1, d);
            //Console.WriteLine(GetDish(1));

            //Dictionary<Product, int> prs = GetDishIngregients(1);
            //foreach(KeyValuePair<Product, int> kvp in prs)
            //{
            //    Console.WriteLine($"{kvp.Key.ToString()}: {kvp.Value}");
            //}

            //Order o = new Order(DateTime.Now, true, _table: 12);
            //UpdateOrder(1, o);
            //Console.WriteLine(GetOrder(1));

            //Dictionary<Dish, int> compound = GetOrderCompound(2);
            //foreach (KeyValuePair<Dish, int> pair in compound)
            //{
            //    Console.WriteLine($"{pair.Key}: {pair.Value}");
            //}

            //User u = new User("a", "a", "b", "c", DateTime.Now, true, "+79882385617");
            //UpdateUser(1, u);
            //Console.WriteLine(GetUser(1)); 

            //Employee e = new Employee("ad", "ad", "bd", "cd", DateTime.Now, true, "+79882385617", 13500, "admin");
            //UpdateEmployee(1, e);
            //Console.WriteLine(GetEmployee(1));
            //DeleteEmployee(1);
            //AddProductToWarehouse(1, 15);
            //Dictionary<Product, int> warehouse = GetWarehouse();
            //foreach (KeyValuePair<Product, int> kvp in warehouse)
            //{
            //    Console.WriteLine($"Product: {kvp.Key}; Quantity: {kvp.Value}\n");
            //}

            //DeleteProductToWarehouse(2);
            //warehouse = GetWarehouse();
            //foreach (KeyValuePair<Product, int> kvp in warehouse)
            //{
            //    Console.WriteLine($"Product: {kvp.Key}; Quantity: {kvp.Value}\n");
            //}
            //Warehouse prod = GetProductToWarehouse(1);
            //Console.WriteLine(prod);
            //List<Order> ActiveOrderCollection = GetActiveOrderCollection();
            //foreach(Order o in ActiveOrderCollection)
            //{
            //    Console.WriteLine(o);
            //}
        }
        public static void AddProduct(string name, string type, double price)
        {
            using (ProjContext db = new ProjContext())
            {
                Product product = new Product { Name = name, Type = type, Price = price };
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
                }
            }
            return _products;
        }
        public static Product GetProduct(int id)
        {
            using (ProjContext db = new ProjContext())
            {
                Product product = db.Products.Find(id);
                return product;
                //return $"Name: {product.Name} | Type: {product.Type}";
            }
        }
        public static void UpdateProduct(int id, Product product)
        {
            using (ProjContext db = new ProjContext())
            {
                Product _product = db.Products.Find(id);
                if (_product != null)
                {
                    _product.Name = product.Name;
                    _product.Type = product.Type;
                    _product.Price = product.Price;
                    db.SaveChanges();
                }
            }
        }
        public static void DeleteProduct(int id)
        {
            using (ProjContext db = new ProjContext())
            {
                Product? product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                Warehouse prod = db.Warehouse.ElementAt(id - 1);
                db.Warehouse.Remove(prod);
            }
        }


        // CRUD for Dish

        public static List<Dish> GetDishes()
        {
            List<Dish> _dishes = new List<Dish>();
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
                Dish dish = db.Dishes.Find(id);
                return dish;
            }
        }
        public static Dictionary<Product, int> GetDishIngregients(int id)
        {
            Dictionary<Product, int> _ingredients = new Dictionary<Product, int>();
            using (ProjContext db = new ProjContext())
            {
                foreach (Ingredient ing in db.Ingredients)
                {
                    if (ing.DishId == id)
                    {
                        _ingredients.Add(GetProduct(ing.ProductId), ing.ProductQuantity);
                    }
                }
            }
            return _ingredients;
        }
        public static void AddDish(string name, double primecost, double price, Dictionary<Product, int> Recipe)
        {
            using (ProjContext db = new ProjContext())
            {
                Dish dish = new Dish(name, primecost, price);
                db.Dishes.Add(dish);
                db.SaveChanges();

                AddRecipe(db.Dishes.ElementAt(db.Dishes.Count() - 1).Id, Recipe);
                db.SaveChanges();

            }

        }
        public static void DeleteRecipe(int id)
        {
            using (ProjContext db = new ProjContext())
            {
                foreach (Ingredient ing in db.Ingredients)
                {
                    if (ing.DishId == id)
                    {
                        db.Ingredients.Remove(ing);
                    }
                }
                db.SaveChanges();
            }
        }
        public static void AddRecipe(int id, Dictionary<Product, int> Recipe)
        {
            using (ProjContext db = new ProjContext())
            {
                foreach (KeyValuePair<Product, int> kvp in Recipe)
                {
                    //int temp = db.Dishes.ElementAt(db.Dishes.Count() - 1).Id;
                    //temp = kvp.Key;
                    //temp = kvp.Value;
                    Ingredient ing = new Ingredient(kvp.Key.Id, kvp.Value, id);
                    db.Ingredients.Add(ing);
                    db.SaveChanges();
                    //db.Ingredients.Add(new Ingredient( kvp.Key, kvp.Value, db.Dishes.ElementAt(db.Dishes.Count() - 1).Id));

                }
                db.SaveChanges();
            }
        }
        public static void UpdateDish(int id, Dish dish, Dictionary<Product, int> Recipe)
        {
            using (ProjContext db = new ProjContext())
            {
                Dish? _dish = db.Dishes.Find(id);
                if (_dish != null)
                {
                    _dish.Name = dish.Name;
                    _dish.Price = dish.Price;
                    _dish.Primecost = dish.Primecost;
                    DeleteRecipe(id);
                    AddRecipe(_dish.Id, Recipe);
                }
                db.SaveChanges();
            }
        }
        public static void UpdateDish(int id, Dish dish)
        {
            using (ProjContext db = new ProjContext())
            {
                Dish? _dish = db.Dishes.Find(id);
                if (_dish != null)
                {
                    _dish.Name = dish.Name;
                    _dish.Price = dish.Price;
                    _dish.Primecost = dish.Primecost;
                }
                db.SaveChanges();
            }
        }
        public static void DeleteDish(int id)
        {
            using (ProjContext db = new ProjContext())
            {
                Dish? dish = db.Dishes.Find(id);
                DeleteRecipe(id);
                db.Dishes.Remove(dish);
                db.SaveChanges();
            }
        }

        //// CRUD for Order

        public static Dictionary<Dish, int> GetOrderCompound(int id)
        {
            Dictionary<Dish, int> _compound = new Dictionary<Dish, int>();
            using (ProjContext db = new ProjContext())
            {
                foreach (OrdersDishes comp in db.Compound)
                {
                    if (comp.OrderId == id)
                    {
                        _compound.Add(GetDish(comp.DishId), comp.Quantity);
                    }
                }


            }
            return _compound;
        }
        public static void DeleteCompound(int id)
        {
            using (ProjContext db = new ProjContext())
            {
                foreach (OrdersDishes comp in db.Compound)
                {
                    if (comp.OrderId == id)
                    {
                        db.Compound.Remove(comp);
                    }
                }
            }
        }
        public static void AddCompound(int id, Dictionary<int, int> compound)
        {
            using (ProjContext db = new ProjContext())
            {
                foreach (KeyValuePair<int, int> kvp in compound)
                {
                    OrdersDishes compoundIndgredient = new OrdersDishes(id, kvp.Key, kvp.Value);
                    db.Compound.Add(compoundIndgredient);
                    db.SaveChanges();
                }
            }
        }
        public static List<Order> GetOrders()
        {
            List<Order> _orders = new List<Order>();
            using (ProjContext db = new ProjContext())
            {
                foreach (Order ord in db.Orders)
                {
                    _orders.Add(ord);
                    GetOrderCompound(ord.Id);
                }
            }
            return _orders;
        }
        public static Order GetOrder(int id)
        {
            using (ProjContext db = new ProjContext())
            {
                return db.Orders.Find(id);
            }
        }
        public static void AddOrder(DateTime date,  Dictionary<int, int> compound, Address? address = null, int? table = null)
        {
            using (ProjContext db = new ProjContext())
            {
                Order ord = new Order(date,true, address, table);
                db.Orders.Add(ord);
                db.SaveChanges();
                AddCompound(db.Orders.ElementAt(db.Orders.Count() - 1).Id, compound);
                db.SaveChanges();
            }
        }
        public static void UpdateOrder(int id, Order order, Dictionary<int, int> compound)
        {
            using (ProjContext db = new ProjContext())
            {
                Order? _order = db.Orders.Find(id);
                if (_order != null)
                {
                    _order.OrderDate = order.OrderDate;
                    _order.Address = order.Address;
                    _order.Table = order.Table;
                    _order.IsActive = order.IsActive;
                    DeleteCompound(id);
                    AddCompound(id, compound);
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
                    _order.OrderDate = order.OrderDate;
                    _order.Address = order.Address;
                    _order.Table = order.Table;
                }
                db.SaveChanges();
            }
        }
        public static void DeleteOrder(int id)
        {
            using (ProjContext db = new ProjContext())
            {
                Order? ord = db.Orders.Find(id);
                DeleteCompound(id);
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
                }
            }
            return users;
        }
        public static User GetUser(int id)
        {
            using (ProjContext db = new ProjContext())
            {
                return db.Users.Find(id);
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
                if (_user != null)
                {
                    _user.Login = user.Login;
                    _user.Password = user.Password;
                    _user.Name = user.Name;
                    _user.Surname = user.Surname;
                    _user.Birthday = user.Birthday;
                    _user.Gender = user.Gender;
                    _user.Phone = user.Phone;
                }
                db.SaveChanges();
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
                }
            }
            return _employees;

        }
        public static Employee GetEmployee(int id)
        {
            using (ProjContext db = new ProjContext())
            {
                return db.Employees.Find(id);
            }
        }
        public static void AddEmployee(string login, string password, string name, string surname, DateTime birthday, bool gender, string phone, double salary, string post)
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
                    _employee.Login = employee.Login;
                    _employee.Password = employee.Password;
                    _employee.Name = employee.Name;
                    _employee.Surname = employee.Surname;
                    _employee.Birthday = employee.Birthday;
                    _employee.Gender = employee.Gender;
                    _employee.Phone = employee.Phone;
                    _employee.Salary = employee.Salary;
                    _employee.Post = employee.Post;
                }
                db.SaveChanges();
            }
        }
        public static void AddProductToWarehouse(int productId, int productQuantity)
        {
            using(ProjContext db = new ProjContext())
            {
                db.Warehouse.Add(new Warehouse(productId, productQuantity));
                db.SaveChanges();
            }
        }
        public static Dictionary<Product, int> GetWarehouse()
        {
            Dictionary<Product, int> _warehouse = new Dictionary<Product, int>();
            using (ProjContext db = new ProjContext())
            {
                foreach (Warehouse warehouse in db.Warehouse)
                {
                    _warehouse.Add(GetProduct(warehouse.ProductId), warehouse.ProductQuantity);
                }
            }
            return _warehouse;
        }
        public static Warehouse GetProductToWarehouse(int id)
        {
            using (ProjContext db = new ProjContext()) 
            {
                return db.Warehouse.Find(id);
            }
        }
        public static void UpdateProductInWarehouse(int id, Warehouse w)
        {
            using(ProjContext db = new ProjContext())
            {
                Warehouse product = db.Warehouse.Find(id);
                if(product!=null)
                {
                    product.ProductId = w.ProductId;
                    product.ProductQuantity = w.ProductQuantity;
                    db.SaveChanges();
                }
            }
        }
        public static void DeleteProductToWarehouse(int id)
        {
            using(ProjContext db = new ProjContext())
            {
                Warehouse prod = db.Warehouse.Find(id);
                db.Warehouse.Remove(prod);
                db.SaveChanges();
            }
        }
        public static List<Order> GetActiveOrderCollection()
        {
            List<Order> orders = new List<Order>();
            using (ProjContext db = new ProjContext())
            {
                foreach (Order order in db.Orders)
                {
                    if(order.IsActive)
                    {
                        orders.Add(order);
                        
                    }
                }
            }
            return orders;
        }
    }
}

