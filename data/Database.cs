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
    public void LoadTables(){
        _products.Include(t => t.Category);
        _purchases.Include(t => t.Customer).Include(t => t.Product);
    }
    public List<Product> GetProducts()
    {
        return _products.Include(t => t.Category).ToList();
    }

    public List<Product> GetProductsByPrice()
    {
        return _products.Include(t => t.Category).OrderBy(p => p.Price).ToList();
    }

    public List<Product> GetProductsByStock()
    {
        return _products.Include(t => t.Category).OrderBy(p => p.Stock).ToList();
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
    // Deletes a category from the database by its ID.
    public void DeleteCategory(int id)
    {
        foreach (Category c in _categories)
        {
            if (c.Id == id)
            {
                _categories.Remove(c); // Remove the category from the set.
            }
        }
        SaveChanges(); // Persist changes to the database.
    }
public void UpdateCustomer(int id, string newName)
{
    // Iterate through the customer list to find the customer with the specified ID.
    foreach (Customer c in _customers)
    {
        if (c.Id == id) // Check if the customer's ID matches the specified ID.
        {
            c.Name = newName; // Update the customer's name.
        }
    }
    SaveChanges(); // Persist changes to the database.
}
    public List<Product> GetMostExpensiveProduct()
    {
        List<Product> mostExpensive = new List<Product>();
        Product temp = new Product();
        foreach (Product p in _products.Include(t => t.Category))
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
        temp.Price = 9000000;
        foreach (Product p in _products.Include(t => t.Category))
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
        foreach (Product p in _products.Include(t => t.Category))
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
    return _customers.ToList(); // Retrieves a list of all customers from the database.
}

public void AddCustomer(string name, string surname, string email, Int64 phoneNumber, string address, string clientCode)
{
    // Create and add a new customer to the database.
    _customers.Add(new Customer { Name = name, Surname = surname, Email = email, PhoneNumber = phoneNumber, Address = address, ClientCode = clientCode });
    SaveChanges(); // Persist changes to the database.
}

    public List<Customer> GetCustomerBySurname()
    {
        return _customers.ToList();
    }

    public void DeleteCustomer(int id)
{
    // Iterate through the customer list to find and remove the specified customer.
    foreach (Customer c in _customers)
    {
        if (c.Id == id) // Check if the customer's ID matches the specified ID.
        {
            _customers.Remove(c); // Remove the customer from the set.
        }
    }
    SaveChanges(); // Persist changes to the database.
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



    // Adds a new category to the database.
    public void AddCategory(string name)
    {
        _categories.Add(new Category { Name = name });
        SaveChanges(); // Persist changes to the database.
    }

    // Removes a category from the database by its ID.
    public void RemoveCategory(int id)
    {
        foreach (var cat in _categories)
        {
            if (cat.Id == id)
            {
                _categories.Remove(cat); // Remove the category from the set.
                SaveChanges(); // Persist changes to the database.
            }
        }
    }

    public List<Category> GetCategories()
    {
        return _categories.ToList(); // Returns all categories as a list.
    }
    public List<Purchase> GetPurchases()
    {
        return _purchases.Include(t => t.Customer).Include(t => t.Product).ToList();
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

    // Retrieves a specific category by its ID.
    public Category GetCategoryById(int categoryId)
    {
        return _categories.FirstOrDefault(c => c.Id == categoryId); // Returns the category if found, otherwise null.
    }
}