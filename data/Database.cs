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

            // Add the new table `clienti` with the field `codice_cliente`
            string sql = @"
                            CREATE TABLE categorie (id INTEGER PRIMARY KEY AUTOINCREMENT, nome TEXT UNIQUE);
                            CREATE TABLE prodotti (id INTEGER PRIMARY KEY AUTOINCREMENT, nome TEXT UNIQUE, 
                            prezzo REAL, quantita INTEGER CHECK (quantita >= 0), id_categoria INTEGER, 
                            FOREIGN KEY (id_categoria) REFERENCES categorie(id));
                            CREATE TABLE clienti (id INTEGER PRIMARY KEY AUTOINCREMENT, nome TEXT UNIQUE, codice_cliente TEXT UNIQUE);
                            INSERT INTO categorie (nome) VALUES ('c1');
                            INSERT INTO categorie (nome) VALUES ('c2');
                            INSERT INTO categorie (nome) VALUES ('c3');
                            INSERT INTO prodotti (nome, prezzo, quantita, id_categoria) VALUES ('p1', 1, 10, 1);
                            INSERT INTO prodotti (nome, prezzo, quantita, id_categoria) VALUES ('p2', 2, 20, 2);";

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
        string sql = "SELECT * FROM prodotti";
        OpenConnection();
        var command = new SQLiteCommand(sql, _connection);
        return command.ExecuteReader();
    }

    public SQLiteDataReader GetProductsOrderedByPrice()
    {
        string sql = "SELECT * FROM prodotti ORDER BY prezzo";
        var connection = OpenConnection();
        var command = new SQLiteCommand(sql, connection);
        return command.ExecuteReader();
    }

    public SQLiteDataReader GetProductsOrderedByQuantity()
    {
        string sql = "SELECT * FROM prodotti ORDER BY quantita";
        var connection = OpenConnection();
        var command = new SQLiteCommand(sql, connection);
        return command.ExecuteReader();
    }

    public void UpdateProductPrice(string name, string price)
    {
        string sql = $"UPDATE prodotti SET prezzo = {price} WHERE nome = '{name}'";
        using var connection = OpenConnection();
        using var command = new SQLiteCommand(sql, connection);
        command.ExecuteNonQuery();
    }

    public void DeleteProduct(string name)
    {
        string sql = $"DELETE FROM prodotti WHERE nome = '{name}'";
        using var connection = OpenConnection();
        using var command = new SQLiteCommand(sql, connection);
        command.ExecuteNonQuery();
    }

    public SQLiteDataReader GetMostExpensiveProduct()
    {
        string sql = "SELECT * FROM prodotti ORDER BY prezzo DESC LIMIT 1";
        var connection = OpenConnection();
        var command = new SQLiteCommand(sql, connection);
        return command.ExecuteReader();
    }

    public SQLiteDataReader GetLeastExpensiveProduct()
    {
        string sql = "SELECT * FROM prodotti ORDER BY prezzo ASC LIMIT 1";
        var connection = OpenConnection();
        var command = new SQLiteCommand(sql, connection);
        return command.ExecuteReader();
    }

    public void AddProduct(string name, string price, string quantity, string categoryId)
    {
        string sql = $"INSERT INTO prodotti (nome, prezzo, quantita, id_categoria) VALUES ('{name}', {price}, {quantity}, {categoryId})";
        using var connection = OpenConnection();
        using var command = new SQLiteCommand(sql, connection);
        command.ExecuteNonQuery();
    }

    public SQLiteDataReader GetProductByName(string name)
    {
        string sql = $"SELECT * FROM prodotti WHERE nome = '{name}'";
        var connection = OpenConnection();
        var command = new SQLiteCommand(sql, connection);
        return command.ExecuteReader();
    }

    public SQLiteDataReader GetProductsByCategory(string categoryId)
    {
        string sql = $"SELECT * FROM prodotti WHERE id_categoria = {categoryId}";
        var connection = OpenConnection();
        var command = new SQLiteCommand(sql, connection);
        return command.ExecuteReader();
    }

    public void AddCategory(string name)
    {
        string sql = $"INSERT INTO categorie (nome) VALUES ('{name}')";
        using var connection = OpenConnection();
        using var command = new SQLiteCommand(sql, connection);
        command.ExecuteNonQuery();
    }

    public void DeleteCategory(string name)
    {
        string sql = $"DELETE FROM categorie WHERE nome = '{name}'";
        using var connection = OpenConnection();
        using var command = new SQLiteCommand(sql, connection);
        command.ExecuteNonQuery();
    }
}
