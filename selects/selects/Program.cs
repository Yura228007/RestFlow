//Insert, delete,update: 
//    products(warehouse),dish,order, employee
using Microsoft.EntityFrameworkCore;

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
    public Dish(Dictionary<int, int> recipe) { Recipe = recipe; }
    public Dish(string name, string description, double primecost, double price) { Name = name; Description = description; Primecost = primecost; Price = price; }
}
public class Order
{
    private List<int> Dishes { get; set; }
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public Address Address { get; set; }
    public Order() { }
    public Order(List<int> dishes) { Dishes = dishes; }
    public Order(int user_id, DateTime date, Address address) { UserId = user_id; OrderDate = date; Address = address; }

}
public class Post
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Post() { }
    public Post(string name) { Name = name; }
}
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int PostId { get; set; }
    public double Salary { get; set; }
    public Employee() { }
    public Employee(string name, string surname, int post_id, double salary) { Name = name; Surname = surname; PostId = post_id; Salary = salary; }
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
    public Ingredient(int product_id, int product_quantity,int dish_id) { ProductId = product_id; ProductQuantity = product_quantity; DishId = dish_id; }
}


public class ProjContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<BDUser> BDUsers { get; set; }
    public DbSet<Warehouse> Warehouse { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public ProjContext()
    {

        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-8FIP4U5;Database=RestarauntManager;Trusted_Connection=True;TrustServerCertificate=True");

        }
    }

}
public class Program
{
    static void Main(string[] args)
    {
        //AddProduct("Beef", "Meat food");
        //UpdateProduct(4, new Product("Juice", "Drinks"));
        //DeleteProduct(5);
        //GetProducts();
        Dictionary<int, int> recipe = new Dictionary<int, int> { { 1, 19 }, { 2, 16 } };
        AddDish("burger", "american food", 50.35, 150.35, recipe);
        //DeleteDish(4);
        //DeleteDish(5);
        //DeleteDish(6);
        //DeleteDish(7);
        //GetDishIngregients(4);
        GetDish();
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
            Product product = db.Products.ElementAt(id-1);
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
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
        }
    }


    // CRUD for Dish

    static void GetDish()
    {
        using (ProjContext db = new ProjContext())
        {
            foreach(Dish dish in db.Dishes)
            {
                Console.WriteLine($"ID: {dish.Id} | Name: {dish.Name} | Primecost: {dish.Primecost} | Description:  {dish.Description} | Price:  {dish.Price}");
                Console.WriteLine("Ingredients:");
                GetDishIngregients(dish.Id);
            }
            

        }
    }
    static void GetDishIngregients(int id)
    {
        using(ProjContext db = new ProjContext())
        {
            foreach (Ingredient ing in db.Ingredients)
            {
                if(ing.DishId == id)
                {
                    Console.WriteLine($"Product: {GetProduct(ing.ProductId)} | Quantity: {ing.ProductQuantity}");
                }
            }
        }
    }
    static void AddDish(string name, string description, double primecost, double price, Dictionary<int, int> Recipe)
    {
        using (ProjContext db = new ProjContext())
        {
            Dish dish = new Dish(name, description, primecost, price);
            db.Dishes.Add(dish);
            foreach(KeyValuePair<int, int> kvp in Recipe)
            {
                //int temp = db.Dishes.ElementAt(db.Dishes.Count() - 1).Id;
                //temp = kvp.Key;
                //temp = kvp.Value;
                int TEMP = db.Dishes.Count();
                //db.Ingredients.Add(new Ingredient( kvp.Key, kvp.Value, db.Dishes.ElementAt(db.Dishes.Count() - 1).Id));

            }
            
            db.SaveChanges();
        }

    }
    //static void UpdateDish(int id, Product product)
    //{
    //    using (ProjContext db = new ProjContext())
    //    {
    //        db.Products.Find(id + 1000).Name = product.Name;
    //        db.Products.Find(id + 1000).Type = product.Type;
    //        db.SaveChanges();
    //    }
    //}
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
            Dish dish = db.Dishes.ElementAt(id-8);
            foreach(Ingredient ing in db.Ingredients)
            {
                if(ing.DishId == id)
                {
                    db.Ingredients.Remove(ing);
                }
            }
            db.Dishes.Remove(dish);
            db.SaveChanges();
        }
    }


    //// CRUD for Order

    //static void GetProducts()
    //{
    //    using (ProjContext db = new ProjContext())
    //    {
    //        foreach (Product product in db.Products)
    //        {
    //            Console.WriteLine($"ID: {product.Id} | Name: {product.Name} | Type: {product.Type}");
    //        }
    //    }
    //}
    //static void AddProduct(string name, string type)
    //{
    //    using (ProjContext db = new ProjContext())
    //    {
    //        Product prod = new Product(name, type);
    //        db.Products.Add(prod);
    //        db.SaveChanges();
    //    }
    //}
    //static void DeleteProduct(int id)
    //{
    //    using (ProjContext db = new ProjContext())
    //    {
    //        Product product = db.Products.Find(id);
    //        db.Products.Remove(product);
    //        db.SaveChanges();
    //    }
    //}


    //CRUD for User

    /*static void GetProducts()
    {
        using (ProjContext db = new ProjContext())
        {
            foreach (Product product in db.Products)
            {
                  Console.WriteLine($"ID: {product.Id} | Name: {product.Name} | Type: {product.Type}");
            }
        }
    }
    static void AddProduct(string name, string type)
    {
        using (ProjContext db = new ProjContext())
        {
            Product prod = new Product(name,type);
            db.Products.Add(prod);
            db.SaveChanges();
        }
    }
    static void DeleteProduct(int id)
    {
        using (ProjContext db = new ProjContext())
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
        }
    }*/

    //CRUD for Employee

    //static void GetProducts()
    //{
    //    using (ProjContext db = new ProjContext())
    //    {
    //        foreach (Product product in db.Products)
    //        {
    //              Console.WriteLine($"ID: {product.Id} | Name: {product.Name} | Type: {product.Type}");
    //        }
    //    }
    //}
    //static void AddProduct(string name, string type)
    //{
    //    using (ProjContext db = new ProjContext())
    //    {
    //        Product prod = new Product(name,type);
    //        db.Products.Add(prod);
    //        db.SaveChanges();
    //    }
    //}
    //static void DeleteProduct(int id)
    //{
    //    using (ProjContext db = new ProjContext())
    //    {
    //        Product product = db.Products.Find(id);
    //        db.Products.Remove(product);
    //        db.SaveChanges();
    //    }
    //}
}
