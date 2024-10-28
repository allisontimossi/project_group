using System.Data.SQLite;
using Microsoft.EntityFrameworkCore;


public class Database : DbContext
{
    public DbSet<Customer> Customers{get;set;}
    public DbSet<Category> Categories{get;set;}
    public DbSet<Product> Products{get;set;}
    public DbSet<Purchase> Purchases{get;set;}
    protected override void OnConfiguring(DbContextOptionsBuilder options){
        options.UseSqlite("Data Source = database.db");
        //options.UseLazyLoadingProxies();
    }
    private const string ConnectionString = "Data Source=database.db;Version=3;";
    private SQLiteConnection _connection;

    /*public void CreateDatabase(string path)
    {
        if (!File.Exists(path))
        {
            SQLiteConnection.CreateFile(path);
            SQLiteConnection connection = new SQLiteConnection($"Data Source={path};Version=3;");
            connection.Open();

            string sql = @"
            CREATE TABLE IF NOT EXISTS categories (
                id INTEGER PRIMARY KEY AUTOINCREMENT, 
                name TEXT UNIQUE
            );

            CREATE TABLE IF NOT EXISTS products (
                id INTEGER PRIMARY KEY AUTOINCREMENT, 
                name TEXT UNIQUE, 
                price REAL, 
                stock INTEGER CHECK (stock >= 0), 
                category_id INTEGER, 
                FOREIGN KEY (category_id) REFERENCES categories(id)
            );

            CREATE TABLE IF NOT EXISTS customers (
                id INTEGER PRIMARY KEY AUTOINCREMENT, 
                name TEXT, 
                surname TEXT, 
                email TEXT UNIQUE, 
                phone_number TEXT, 
                address TEXT, 
                client_code TEXT UNIQUE
            );

            CREATE TABLE IF NOT EXISTS purchases (
                id INTEGER PRIMARY KEY AUTOINCREMENT, 
                customer_id INTEGER, 
                product_id INTEGER, 
                purchase_date DATETIME DEFAULT CURRENT_TIMESTAMP, 
                quantity INTEGER CHECK (quantity > 0),
                FOREIGN KEY (customer_id) REFERENCES customers(id), 
                FOREIGN KEY (product_id) REFERENCES products(id)
            );

            INSERT INTO categories (name) VALUES ('c1'), ('c2'), ('c3');
            INSERT INTO products (name, price, stock, category_id) VALUES 
                ('p1', 1, 10, 1), 
                ('p2', 2, 20, 2);
            INSERT INTO customers (name, surname, email, phone_number, address, client_code) VALUES 
                ('client1', 'Smith', 'client1@example.com', '1234567890', '123 Main St', 'C001'), 
                ('client2', 'Jones', 'client2@example.com', '0987654321', '456 Elm St', 'C002');
            ";

            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }*/

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
        string sql = "SELECT stock FROM products WHERE id = @productId";
        OpenConnection();
        using var command = new SQLiteCommand(sql, _connection);
        command.Parameters.AddWithValue("@productId", productId);
        int stock = Convert.ToInt32(command.ExecuteScalar());
        CloseConnection();
        
        return stock;
    }

    public SQLiteDataReader GetProducts()
    {
        string sql = "SELECT * FROM products";
        OpenConnection();
        var command = new SQLiteCommand(sql, _connection);
        return command.ExecuteReader();
    }

    public SQLiteDataReader GetProductsByPrice()
    {
        string sql = "SELECT * FROM products ORDER BY price";
        OpenConnection();
        var command = new SQLiteCommand(sql, _connection);
        return command.ExecuteReader();
    }

    public SQLiteDataReader GetProductsByStock()
    {
        string sql = "SELECT * FROM products ORDER BY stock";
        OpenConnection();
        var command = new SQLiteCommand(sql, _connection);
        return command.ExecuteReader();
    }

    public void UpdateProductPrice(string name, string price)
    {
        string sql = $"UPDATE products SET price = {price} WHERE name = '{name}'";
        OpenConnection();
        using var command = new SQLiteCommand(sql, _connection);
        command.ExecuteNonQuery();
    }

    public void UpdateCustomer(int id, string newName)
    {
        string sql = $"UPDATE customers SET name = '{newName}' WHERE id = {id}";
        OpenConnection();
        using var command = new SQLiteCommand(sql, _connection);
        command.ExecuteNonQuery();

    }

    public void DeleteProduct(string name)
    {
        string sql = $"DELETE FROM products WHERE name = '{name}'";
        OpenConnection();
        using var command = new SQLiteCommand(sql, _connection);
        command.ExecuteNonQuery();
    }

    public SQLiteDataReader GetMostExpensiveProduct()
    {
        string sql = "SELECT * FROM products ORDER BY price DESC LIMIT 1";
        OpenConnection();
        var command = new SQLiteCommand(sql, _connection);
        return command.ExecuteReader();
    }

    public SQLiteDataReader GetLeastExpensiveProduct()
    {
        string sql = "SELECT * FROM products ORDER BY price ASC LIMIT 1";
        OpenConnection();
        var command = new SQLiteCommand(sql, _connection);
        return command.ExecuteReader();
    }

    public void AddProduct(string name, string price, string stock, string categoryId)
    {
        string sql = $"INSERT INTO products (name, price, stock, category_id) VALUES ('{name}', {price}, {stock}, {categoryId})";
        OpenConnection();
        using var command = new SQLiteCommand(sql, _connection);
        command.ExecuteNonQuery();
    }

    public SQLiteDataReader GetProductByName(string name)
    {
        string sql = $"SELECT * FROM products WHERE name = '{name}'";
        OpenConnection();
        var command = new SQLiteCommand(sql, _connection);
        return command.ExecuteReader();
    }

    public SQLiteDataReader GetProductsByCategory(string categoryId)
    {
        string sql = $"SELECT * FROM products WHERE category_id = {categoryId}";
        OpenConnection();
        var command = new SQLiteCommand(sql, _connection);
        return command.ExecuteReader();
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
        Customers.Add(new Customer{Name = name, Surname = surname, Email = email, PhoneNumber = phoneNumber, Address = address, ClientCode = clientCode});
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
