public class ProductController
{
    private ProductView _productview;
    private Database _database;
    public ProductController(ProductView productview, Database database)
    {
        _productview = productview;
        _database = database;
    }

    public void ProductMenu()
    {
        bool exit = true;
        while (exit)
        {
            Console.Clear();
            _productview.ShowProductMainMenu();
            string selection = Console.ReadLine()!;
            switch (selection)
            {
                case "1":
                    ShowProducts();
                    break;
                case "2":
                    ShowProductByName();
                    break;
                case "3":
                    ShowProductsByPrice();
                    break;
                case "4":
                    ShowProductsByQuantity();
                    break;
                case "5":
                    ShowProductsByCategory();
                    break;
                case "6":
                    ShowMostExpensiveProduct();
                    break;
                case "7":
                    ShowLeastExpensiveProduct();
                    break;
                case "8":
                    UpdateProductPrice();
                    break;
                case "9":
                    DeleteProduct();
                    break;
                case "10":
                    AddProduct();
                    break;
                case "11":
                    exit = false;
                    break;
            }
        }
    }
    private void ShowProducts()
    {
        List<Product> products = _database.GetProducts();
        foreach(Product p in products)
        {
            _productview.ShowProduct(p.Id.ToString(), p.Name, p.Price.ToString(), p.Stock.ToString(), p.CategoryId.ToString());
        }
        Console.ReadKey();
    }
    private void ShowProductsByPrice()
    {
        List<Product> products = _database.GetProducts();
        foreach(Product p in products)
        {
            _productview.ShowProduct(p.Id.ToString(), p.Name, p.Price.ToString(), p.Stock.ToString(), p.CategoryId.ToString());
        }
        Console.ReadKey();
    }
    private void ShowProductsByQuantity()
    {
        List<Product> products = _database.GetProducts();
        foreach(Product p in products)
        {
            _productview.ShowProduct(p.Id.ToString(), p.Name, p.Price.ToString(), p.Stock.ToString(), p.CategoryId.ToString());
        }
        Console.ReadKey();
    }
    private void UpdateProductPrice()
    {
        Console.WriteLine("Insert product name");
        string name = Console.ReadLine()!;
        Console.WriteLine("Insert new price");
        int price = Convert.ToInt32(Console.ReadLine()!);
        _database.UpdateProductPrice(name, price);
    }
    private void DeleteProduct()
    {
        Console.WriteLine("Insert product name");
        string name = Console.ReadLine()!;
        _database.DeleteProduct(name);       
    }
    private void ShowMostExpensiveProduct()
    {
        List<Product> mostExpensive = _database.GetMostExpensiveProduct();
        foreach(Product p in mostExpensive){
            _productview.ShowProduct(p.Id.ToString(), p.Name, p.Price.ToString(), p.Stock.ToString(), p.CategoryId.ToString());
        }
        Console.ReadKey();
    }
    private void ShowLeastExpensiveProduct()
    {
        List<Product> LeastExpensive = _database.GetLeastExpensiveProduct();
        foreach(Product p in LeastExpensive){
            _productview.ShowProduct(p.Id.ToString(), p.Name, p.Price.ToString(), p.Stock.ToString(), p.CategoryId.ToString());
        }
        Console.ReadKey();
    }
    private void AddProduct()
    {
        Console.WriteLine("Insert product name");
        string name = Console.ReadLine()!;
        Console.WriteLine("Insert product price");
        int price = Convert.ToInt32(Console.ReadLine()!);
        Console.WriteLine("Insert product quantity");
        int stock = Convert.ToInt32(Console.ReadLine()!);
        Console.WriteLine("Insert category Id");
        int categoryId = Convert.ToInt32(Console.ReadLine()!);
        _database.AddProduct(name, price, stock, categoryId);
    }
    private void ShowProductByName()
    {
        Console.WriteLine("Insert product name");
        string name = Console.ReadLine()!;
        Product product = _database.GetProductByName(name);
        _productview.ShowProduct(product.Id.ToString(), product.Name, product.Price.ToString(), product.Stock.ToString(), product.CategoryId.ToString());
        Console.ReadKey();       
    }
    private void ShowProductsByCategory()
    {
        Console.WriteLine("Insert category Id");
        int categoryId =Convert.ToInt32(Console.ReadLine()!);
        List<Product> productsByCategory = _database.GetProductsByCategory(categoryId);
        foreach(Product p in productsByCategory)
        {
            _productview.ShowProduct(p.Id.ToString(), p.Name, p.Price.ToString(), p.Stock.ToString(), p.CategoryId.ToString());
        }
        Console.ReadKey();       
    }
}
