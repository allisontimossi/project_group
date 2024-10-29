using Microsoft.EntityFrameworkCore;


public class Database : DbContext
{
    private DbSet<Customer> _customers { get; set; }
    private DbSet<Category> _categories { get; set; }
    private DbSet<Product> _products { get; set; }
    private DbSet<Purchase> _purchases { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source = database.db");
        //options.UseLazyLoadingProxies();
    }
    private const string ConnectionString = "Data Source=database.db;Version=3;";
    public int CheckStock(int productId)
    {
        int stock = 0;
        foreach (Product p in _products)
        {
            if (p.Id == productId)
            {
                stock = p.Stock;
            }
        }
        return stock;
    }
    public List<Product> GetProducts()
    {
        return _products.Include(t => t.Category).ToList();
    }

    public List<Product> GetProductsByPrice()
    {
        return _products.OrderBy(p => p.Price).ToList();
    }

    public List<Product> GetProductsByStock()
    {
        return _products.OrderBy(p => p.Stock).ToList();
    }

    public void UpdateProductPrice(string name, int price)
    {
        foreach (Product p in _products)
        {
            if (p.Name == name)
            {
                p.Price = price;
            }
        }
        SaveChanges();
    }
    public void DeleteProduct(string name)
    {
        foreach (Product p in _products)
        {
            if (p.Name == name)
                _products.Remove(p);
        }
        SaveChanges();
    }

    public void UpdateCustomer(int id, string newName)
    {
        foreach (Customer c in _customers)
        {
            if (c.Id == id)
            {
                c.Name = newName;
            }
        }
        SaveChanges();
    }
    public List<Product> GetMostExpensiveProduct()
    {
        List<Product> mostExpensive = new List<Product>();
        Product temp = new Product();
        foreach (Product p in _products)
        {
            if (p.Price >= temp.Price)
            {
                temp = p;
            }
        }
        foreach (Product p in _products)
        {
            if (p.Price >= temp.Price)
            {
                mostExpensive.Add(p);
            }
        }
        return mostExpensive;
    }

    public List<Product> GetLeastExpensiveProduct()
    {
        List<Product> LeastExpensive = new List<Product>();
        Product temp = new Product();
        foreach (Product p in _products)
        {
            if (p.Price <= temp.Price)
            {
                temp = p;
            }
        }
        foreach (Product p in _products)
        {
            if (p.Price <= temp.Price)
            {
                LeastExpensive.Add(p);
            }
        }
        return LeastExpensive;
    }

    public void AddProduct(string name, int price, int stock, Category category)
    {
        _products.Add(new Product { Name = name, Price = price, Stock = stock, Category = category });
        SaveChanges();
    }

    public Product GetProductByName(string name)
    {
        return _products.FirstOrDefault(p => p.Name == name);
    }

    public List<Product> GetProductsByCategory(int categoryId)
    {
        List<Product> productsByCategory = new List<Product>();
        foreach (Product p in _products)
        {
            if (p.Category.Id == categoryId)
            {
                productsByCategory.Add(p);
            }
        }
        return productsByCategory;
    }

    public List<Customer> GetCustomers()
    {
        return _customers.ToList();
    }
    public void AddCustomer(string name, string surname, string email, Int64 phoneNumber, string address, string clientCode)
    {
        _customers.Add(new Customer { Name = name, Surname = surname, Email = email, PhoneNumber = phoneNumber, Address = address, ClientCode = clientCode });
        SaveChanges();
    }

    public List<Customer> GetCustomerBySurname()
    {
        return _customers.ToList();
    }

    public void DeleteCustomer(int id)
    {
        foreach (Customer c in _customers)
        {
            if (c.Id == id)
            {
                _customers.Remove(c);
            }
        }
        SaveChanges();
    }

    public void AddPurchase(Customer customer, Product product, int quantity)
    {
        // Create a new Purchase record
        var purchase = new Purchase
        {
            Customer = customer,
            Product = product,
            Quantity = quantity,
            Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") // Store the date in the desired format
        };

        // Update the stock
        product.Stock -= quantity;

        // Add the purchase and update stock in a single transaction
        _purchases.Add(purchase);
        SaveChanges();
    }



    public void AddCategory(string name)
    {
        _categories.Add(new Category { Name = name });
        SaveChanges();
    }

    public void RemoveCategory(int id)
    {
        foreach (var cat in _categories)
        {
            if (cat.Id == id)
            {
                _categories.Remove(cat);
                SaveChanges();
            }
        }
    }
    public void Addpurchase()
    {

    }

    public List<Category> GetCategories()
    {
        return _categories.ToList();
    }
    public List<Purchase> GetPurchases()
    {
        return _purchases.ToList();
    }

     public Product GetProductById(int productId)
    {
        return _products.FirstOrDefault(p => p.Id == productId);
    }

    // Helper method to find a customer by ID
    public Customer GetCustomerById(int customerId)
    {
        return _customers.FirstOrDefault(c => c.Id == customerId);
    }

      public Category GetCategoryById(int categoryId)
    {
        return _categories.FirstOrDefault(c => c.Id == categoryId);
    }
}
