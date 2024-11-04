using Microsoft.EntityFrameworkCore;


public class Database : DbContext
{
    // Define DbSet properties for each entity type
    private DbSet<Customer> _customers { get; set; }
    private DbSet<Category> _categories { get; set; }
    private DbSet<Product> _products { get; set; }
    private DbSet<Purchase> _purchases { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source = database.db");
    }

    public int CheckStock(int productId)
    {
        int stock = 0;
        foreach (Product p in _products)
        {
            if (p.Id == productId)
            {
                stock = p.Stock; // Retrieve stock if product found
            }
        }
        return stock;  // Return the stock amount
    }
    public void LoadProductsTable(){
        foreach(Product p in _products){
            foreach(Category c in _categories){
                if(p.CategoryId == c.Id){
                    p.Category = c;
                    break;
                }
            }
        }
    }
    public void LoadPurchasesTable(){
        foreach(Purchase p in _purchases){
            foreach(Customer c in _customers){
                if(p.CustomerId == c.Id){
                    p.Customer = c;
                    break;
                }
            }
            foreach(Product pr in _products){
                if(p.ProductId == pr.Id){
                    p.Product = pr;
                    break;
                }
            }
        }
    }
    public List<Product> GetProducts()   // Retrieves all products along with their categories
    {
        return _products.ToList(); // Return all products with category info
    }
    // Retrieves products ordered by price
    public List<Product> GetProductsByPrice()
    {
        List<Product> originalList = _products.ToList();
        List<Product> orderedList = new List<Product>();
       /* Product temp = new Product();
        foreach(Product p in originalList){
            foreach(Product p2 in originalList){
                if(p.Price>=p2.Price){
                    temp = p;
                }
            }
        }*/
        Product temp;
        while(originalList.Count > 0)
        {
            temp = new Product();
            foreach (Product p in originalList)
            {
                if (p.Price >= temp.Price)
                {
                    temp = p;
                }
            }
            originalList.Remove(temp);
            orderedList.Add(temp);
        }
        return orderedList;
    }
    // Retrieves products ordered by stock
    public List<Product> GetProductsByStock()
    {
        List<Product> originalList = _products.ToList();
        List<Product> orderedList = new List<Product>();
        Product temp;
        while(originalList.Count > 0)
        {
            temp = new Product();
            foreach (Product p in originalList)
            {
                if (p.Stock >= temp.Stock)
                {
                    temp = p;
                }
            }
            originalList.Remove(temp);
            orderedList.Add(temp);
        }
        return orderedList;
    }

    // Updates the price of a product by its name
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
    // Deletes a product from the database by its name
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

    // Retrieves the most expensive product(s)
    public List<Product> GetMostExpensiveProduct()
    {
        List<Product> mostExpensive = new List<Product>();
        Product temp = new Product();
        foreach (Product p in _products)
        {
            if (p.Price >= temp.Price)
            {
                temp = p;  // Update temp to the most expensive product found
            }
        }
        foreach (Product p in _products)
        {
            if (p.Price >= temp.Price)
            {
                mostExpensive.Add(p); // Add products that match the highest price
            }
        }
        return mostExpensive;  // Return the list of most expensive products
    }

    // Retrieves the least expensive product(s)
    public List<Product> GetLeastExpensiveProduct()
    {
        List<Product> LeastExpensive = new List<Product>();
        Product temp;
        temp = _products.ToList()[0];
        foreach (Product p in _products)
        {
            if (p.Price <= temp.Price)
            {
                temp = p;  // Update temp to the least expensive product found
            }
        }
        foreach (Product p in _products)
        {
            if (p.Price <= temp.Price)
            {
                LeastExpensive.Add(p); // Add products that match the lowest price
            }
        }
        return LeastExpensive;  // Return the list of least expensive products
    }

    // Adds a new product to the database
    public void AddProduct(string name, int price, int stock, Category category)
    {
        _products.Add(new Product { Name = name, Price = price, Stock = stock, Category = category });
        SaveChanges();
    }

    

    // Retrieves products by a specific category ID
    public List<Product> GetProductsByCategory(int categoryId)
    {
        List<Product> productsByCategory = new List<Product>();
        foreach (Product p in _products)
        {
            if (p.Category.Id == categoryId)
            {
                productsByCategory.Add(p); // Add product if it matches the category ID
            }
        }
        return productsByCategory; // Return products in the specified category
    }

    // Retrieves all customers from the database
    public List<Customer> GetCustomers()
    {
        return _customers.ToList(); // Retrieves a list of all customers from the database.
    }

  // Adds a new customer to the database
    public void AddCustomer(string name, string surname, string email, string phoneNumber, string address, string clientCode)
    {
        // Create and add a new customer to the database.
        _customers.Add(new Customer { Name = name, Surname = surname, Email = email, PhoneNumber = phoneNumber, Address = address, ClientCode = clientCode });
        SaveChanges(); // Persist changes to the database.
    }

    // Retrieves customers by their surname (implementation can be extended)
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
        var purchases = _purchases.ToList();
        return purchases;
    }

    public Product GetProductById(int productId)
    {

        Product foundProduct = null;

        // Loop through each product to find the one with the matching ID
        foreach (var product in _products)
        {
            if (product.Id == productId)
            {
                foundProduct = product;
                break;
            }
        }
        return foundProduct;
    }

    public Customer GetCustomerById(int customerId)
    {
        Customer foundCustomer = null;

        foreach (var customer in _customers)
        {
            if (customer.Id == customerId)
            {
                foundCustomer = customer;
                break;
            }
        }
        return foundCustomer;
    }

    // Retrieves a specific category by its ID.
    public Category GetCategoryById(int categoryId)
    {
        Category foundCategory = null;

        foreach (var category in _categories)
        {
            if (category.Id == categoryId)
            {
                foundCategory = category;
                break;
            }
        }
        return foundCategory;
    }
}