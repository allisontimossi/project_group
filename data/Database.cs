using System.Data.SQLite;
using System.Security;
using Microsoft.EntityFrameworkCore;


public class Database : DbContext
{
    private DbSet<Customer> _customers{get;set;}
    private DbSet<Category> _categories{get;set;}
    private DbSet<Product> _products{get;set;}
    private DbSet<Purchase> _purchases{get;set;}
    protected override void OnConfiguring(DbContextOptionsBuilder options){
        options.UseSqlite("Data Source = database.db");
        //options.UseLazyLoadingProxies();
    }
    private const string ConnectionString = "Data Source=database.db;Version=3;";
    private SQLiteConnection _connection;
    private void OpenConnection()
    {
        _connection = new SQLiteConnection(ConnectionString);
        _connection.Open();
    }
    public void CloseConnection()
    {
        if (_connection.State != System.Data.ConnectionState.Closed)
            _connection.Close();
    }
    public int CheckStock(int productId)
    {
        int stock = 0;
        foreach(Product p in _products){
            if(p.Id == productId){
                stock = p.Stock;
            }
        }
        return stock;
    }
    public List<Product> GetProducts()
    {
        return _products.ToList();
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
        foreach(Product p in _products){
            if(p.Name == name){
                p.Price = price;
            }
        }
        SaveChanges();
    }
    public void DeleteProduct(string name)
    {
        foreach(Product p in _products){
            if(p.Name == name)
                _products.Remove(p);
        }
        SaveChanges();
    }

    public void UpdateCustomer(int id, string newName)
    {
        string sql = $"UPDATE customers SET name = '{newName}' WHERE id = {id}";
        OpenConnection();
        using var command = new SQLiteCommand(sql, _connection);
        command.ExecuteNonQuery();

    }
    public List<Product> GetMostExpensiveProduct()
    {
        List<Product> mostExpensive = new List<Product>();
        Product temp = new Product();
        foreach(Product p in _products){
            if(p.Price >= temp.Price){
                temp = p;
            }
        }
        foreach(Product p in _products){
            if(p.Price >= temp.Price){
                mostExpensive.Add(p);
            }
        }
        return mostExpensive;
    }

    public List<Product> GetLeastExpensiveProduct()
    {
        List<Product> LeastExpensive = new List<Product>();
        Product temp = new Product();
        foreach(Product p in _products){
            if(p.Price <= temp.Price){
                temp = p;
            }
        }
        foreach(Product p in _products){
            if(p.Price <= temp.Price){
                LeastExpensive.Add(p);
            }
        }
        return LeastExpensive;
    }

    public void AddProduct(string name, int price, int stock, int categoryId)
    {
        _products.Add(new Product{Name = name, Price = price, Stock = stock, CategoryId = categoryId});
        SaveChanges();
    }

    public Product GetProductByName(string name)
    {
        return _products.FirstOrDefault(p => p.Name == name);
    }

    public List<Product> GetProductsByCategory(int categoryId)
    {
        List<Product> productsByCategory =new List<Product>();
        foreach(Product p in _products){
            if(p.CategoryId == categoryId){
                productsByCategory.Add(p);
            }
        }
        return productsByCategory;
    }

    public SQLiteDataReader GetCustomers()
    {
        string sql = "SELECT * FROM customers"; 
        OpenConnection();
        var command = new SQLiteCommand(sql, _connection);
        return command.ExecuteReader();
    }

    public SQLiteDataReader GetPurchases()
    {
        string sql = "SELECT * FROM purchases";
        OpenConnection();
        var command = new SQLiteCommand(sql, _connection);
        return command.ExecuteReader();
    }

    public void AddCustomer(string name, string surname, string email, Int64 phoneNumber, string address, string clientCode)
    {
        _customers.Add(new Customer{Name = name, Surname = surname, Email = email, PhoneNumber = phoneNumber, Address = address, ClientCode = clientCode});
        SaveChanges();
    }

    public SQLiteDataReader GetCustomerBySurname(string surname)
    {
        string sql = $"SELECT * FROM customers WHERE surname = '{surname}'";
        OpenConnection();
        var command = new SQLiteCommand(sql, _connection);
        return command.ExecuteReader();
    }

    public void DeleteCustomer(int id)
    {
        string sql = $"DELETE FROM customers WHERE id = '{id}'";
        OpenConnection();
        using var command = new SQLiteCommand(sql, _connection);
        command.ExecuteNonQuery();
    }

public void AddPurchase(int customerId, int productId, int quantity)
{
    OpenConnection();  // Ensure the connection is open before operations

    // Insert the purchase record with the actual quantity purchased
    string purchaseSql = "INSERT INTO purchases (customer_id, product_id, quantity) VALUES (@customerId, @productId, @quantity)";
    using var purchaseCommand = new SQLiteCommand(purchaseSql, _connection);
    purchaseCommand.Parameters.AddWithValue("@customerId", customerId);
    purchaseCommand.Parameters.AddWithValue("@productId", productId);
    purchaseCommand.Parameters.AddWithValue("@quantity", quantity);
    purchaseCommand.ExecuteNonQuery();

    // Retrieve the current stock for the product
    int currentStock = CheckStock(productId);
    OpenConnection();
    // Calculate the new stock by subtracting the quantity purchased
    int newStock = currentStock - quantity;

    // Update the stock in the products table with the calculated new stock value
    string updateStockSql = "UPDATE products SET stock = @newStock WHERE id = @productId";
    using var updateStockCommand = new SQLiteCommand(updateStockSql, _connection);
    updateStockCommand.Parameters.AddWithValue("@newStock", newStock);
    updateStockCommand.Parameters.AddWithValue("@productId", productId);
    updateStockCommand.ExecuteNonQuery();
    CloseConnection();  // Close connection only after all operations are done
}

    public void AddCategory(string name)
    {
        string sql = $"INSERT INTO categories (name) VALUES ('{name}')";
        OpenConnection();
        using var command = new SQLiteCommand(sql, _connection);
        command.ExecuteNonQuery();
    }

    public void DeleteCategory(string name)
    {
        string sql = $"DELETE FROM categories WHERE name = '{name}'";
        OpenConnection();
        using var command = new SQLiteCommand(sql, _connection);
        command.ExecuteNonQuery();
    }
    public  SQLiteDataReader GetCategories() 
    {
        string sql = "SELECT * FROM categories"; 
        OpenConnection();
        var command = new SQLiteCommand(sql, _connection);
        return command.ExecuteReader();
    }
}
