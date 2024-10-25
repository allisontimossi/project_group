using System;
using System.Data.SQLite;

public class Database
{
    private const string ConnectionString = "Data Source=database.db;Version=3;";
    private SQLiteConnection _connection;
    
    public void CreateDatabase(string path)
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

        CREATE TABLE IF NOT EXISTS clients (
            id INTEGER PRIMARY KEY AUTOINCREMENT, 
            name TEXT UNIQUE, 
            client_code TEXT UNIQUE
        );

        CREATE TABLE IF NOT EXISTS purchases (
            id INTEGER PRIMARY KEY AUTOINCREMENT, 
            client_id INTEGER, 
            product_id INTEGER, 
            purchase_date DATETIME DEFAULT CURRENT_TIMESTAMP, 
            quantity INTEGER CHECK (quantity > 0),
            FOREIGN KEY (client_id) REFERENCES clients(id), 
            FOREIGN KEY (product_id) REFERENCES products(id)
        );

        INSERT INTO categories (name) VALUES ('c1'), ('c2'), ('c3');
        INSERT INTO products (name, price, stock, category_id) VALUES 
            ('p1', 1, 10, 1), 
            ('p2', 2, 20, 2);
        INSERT INTO clients (name, client_code) VALUES 
            ('client1', 'C001'), 
            ('client2', 'C002');
    ";


        SQLiteCommand command = new SQLiteCommand(sql, connection);
        command.ExecuteNonQuery();
        connection.Close();
    }
}

    private SQLiteConnection OpenConnection()
{
    _connection = new SQLiteConnection(ConnectionString);
    _connection.Open();
    return _connection;
}

public void CloseConnection()
{
    if (_connection.State != System.Data.ConnectionState.Closed)
        _connection.Close();
}

public SQLiteDataReader GetProducts()
{
    string sql = "SELECT * FROM products";
    OpenConnection();
    var command = new SQLiteCommand(sql, _connection);
    return command.ExecuteReader();
}

public SQLiteDataReader GetProductsOrderedByPrice()
{
    string sql = "SELECT * FROM products ORDER BY price";
    OpenConnection();
    var command = new SQLiteCommand(sql, _connection);
    return command.ExecuteReader();
}

public SQLiteDataReader GetProductsOrderedByStock()
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
public void AddPurchase(int clientId, int productId, int quantity)
{
    string sql = $"INSERT INTO purchases (client_id, product_id, quantity) VALUES ({clientId}, {productId}, {quantity})";  
    OpenConnection();
    using var command = new SQLiteCommand(sql, _connection);
    command.ExecuteNonQuery();
}

}