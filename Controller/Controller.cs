using System.Data.SQLite;
using System.Runtime.CompilerServices;

public class Controller
{
    private MyView _myView;
    private Database _database;
    public Controller(MyView myView, Database database){
        _myView = myView;
        _database = database;
    }
    public void MainMenu(){
        bool exit = true;
        while(exit)
        {
            _myView.ShowMainMenu();
            string selection = Console.ReadLine()!;
            switch (selection)
            {
                case "1":
                    ShowProducts();
                    break;
                case "2":
                    ShowProductsOrderedByPrice();
                    break;
                case "3":
                    ShowProductsOrderedByQuantity();
                    break;
                case "4":
                    UpdateProductPrice();
                    break;
                case "5":
                    DeleteProduct();
                    break;
                case "6":
                    ShowMostExpensiveProduct();
                    break;
                case "7":
                    ShowLeastExpensiveProduct();
                    break;
                case "8":
                    AddProduct();
                    break;
                case "9":
                    ShowProductByName();
                    break;
                case "10":
                    ShowProductsByCategory();
                    break;
                case "11":
                    AddCategory();
                    break;
                case "12":
                    DeleteCategory();
                    break;
                case "13":
                    AddPurchase();
                    break;
                case "14":
                    ShowPurchases();
                    break;
                case "15":
                    ShowCustomers();
                    break;
                case "16":
                    exit = false;
                    break;
            }
        }
        return;
    }
    private void ShowProducts()
    {
        using var reader = _database.GetProducts();
        while (reader.Read())
        {
            _myView.ShowProduct(reader["id"].ToString(), reader["name"].ToString(), reader["price"].ToString(), reader["stock"].ToString(), reader["category_id"].ToString());
        }
        _database.CloseConnection();
    }
    private void ShowProductsOrderedByPrice()
    {
        using var reader = _database.GetProductsOrderedByPrice();
        while (reader.Read())
        {
            _myView.ShowProduct(reader["id"].ToString(), reader["name"].ToString(), reader["price"].ToString(), reader["stock"].ToString(), reader["category_id"].ToString());
        }
        _database.CloseConnection();
    }
    private void ShowProductsOrderedByQuantity()
    {
        using var reader = _database.GetProductsOrderedByStock();
        while (reader.Read())
        {
            _myView.ShowProduct(reader["id"].ToString(), reader["name"].ToString(), reader["price"].ToString(), reader["stock"].ToString(), reader["category_id"].ToString());
        }
        _database.CloseConnection();
    }
    private void UpdateProductPrice()
    {
        Console.WriteLine("Insert product name");
        string name = Console.ReadLine()!;
        Console.WriteLine("Insert new price");
        string price = Console.ReadLine()!;
        _database.UpdateProductPrice(name, price);
        _database.CloseConnection();
    }
    private void DeleteProduct()
    {
        Console.WriteLine("Insert product name");
        string name = Console.ReadLine()!;
        _database.DeleteProduct(name);
        _database.CloseConnection();        
    }
    private void ShowMostExpensiveProduct()
    {
        using var reader = _database.GetMostExpensiveProduct();
        while (reader.Read())
        {
            _myView.ShowProduct(reader["id"].ToString(), reader["name"].ToString(), reader["price"].ToString(), reader["stock"].ToString(), reader["category_id"].ToString());
        }
        _database.CloseConnection();
    }
    private void ShowLeastExpensiveProduct()
    {
        using var reader = _database.GetLeastExpensiveProduct();
        while (reader.Read())
        {
            _myView.ShowProduct(reader["id"].ToString(), reader["name"].ToString(), reader["price"].ToString(), reader["stock"].ToString(), reader["category_id"].ToString());
        }
        _database.CloseConnection();
    }
    private void AddProduct()
    {
        Console.WriteLine("Insert product name");
        string name = Console.ReadLine()!;
        Console.WriteLine("Insert product price");
        string price = Console.ReadLine()!;
        Console.WriteLine("Insert product quantity");
        string quantity = Console.ReadLine()!;
        Console.WriteLine("Insert category Id");
        string categoryId = Console.ReadLine()!;
        _database.AddProduct(name, price, quantity, categoryId);
        _database.CloseConnection();
    }
    private void ShowProductByName()
    {
        Console.WriteLine("Insert product name");
        string name = Console.ReadLine()!;
        using var reader = _database.GetProductByName(name);
        while (reader.Read())
        {
            _myView.ShowProduct(reader["id"].ToString(), reader["name"].ToString(), reader["price"].ToString(), reader["stock"].ToString(), reader["category_id"].ToString());
        }
        _database.CloseConnection();        
    }
    private void ShowProductsByCategory()
    {
        Console.WriteLine("Insert category Id");
        string categoryId = Console.ReadLine()!;
        using var reader = _database.GetProductsByCategory(categoryId);
        while (reader.Read())
        {
            _myView.ShowProduct(reader["id"].ToString(), reader["name"].ToString(), reader["price"].ToString(), reader["stock"].ToString(), reader["category_id"].ToString());
        }
        _database.CloseConnection();        
    }
    private void AddCategory()
    {
        Console.WriteLine("Insert category name");
        string name = Console.ReadLine()!;
        _database.AddCategory(name);
        _database.CloseConnection();

    }
    private void DeleteCategory()
    {
        Console.WriteLine("inserisci il nome della categoria");
        string name = Console.ReadLine()!;
        _database.DeleteCategory(name);
        _database.CloseConnection();        
    }
    private void AddPurchase(){
        Console.WriteLine("Insert product Id");
        Int32.TryParse(Console.ReadLine()!, out int productId);
        int stock = _database.CheckStock(productId);
        _database.CloseConnection();
        Console.WriteLine("Insert product quantity");
        Int32.TryParse(Console.ReadLine()!, out int quantity);
        if(quantity <= stock){
            Console.WriteLine("Insert customer Id");
            Int32.TryParse(Console.ReadLine()!, out int customerId);
            _database.AddPurchase(customerId, productId, stock-quantity);
            _database.CloseConnection();
        }else{
            Console.WriteLine("Not enought products in stock");
        }
    }
    private void ShowPurchases(){
        using var reader = _database.GetPurchases();
        while (reader.Read())
        {
            _myView.ShowPurchase(reader["id"].ToString(), reader["customer_id"].ToString(), reader["product_id"].ToString(), reader["purchase_date"].ToString(), reader["quantity"].ToString());
        }
        _database.CloseConnection();
    }
    private void ShowCustomers(){
        using var reader = _database.GetCustomers();
        while (reader.Read())
        {
            _myView.ShowCustomer(reader["id"].ToString(), reader["name"].ToString(), reader["surname"].ToString(), reader["email"].ToString(), reader["address"].ToString(), reader["phoneNumber"].ToString());
            _database.CloseConnection();
        }
    }
}