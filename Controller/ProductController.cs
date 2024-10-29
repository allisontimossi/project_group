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
        foreach (Product p in products)
        {
            // Check if the Category is null before accessing its properties
            
            _productview.ShowProduct(p.Id.ToString(), p.Name, p.Price.ToString(), p.Stock.ToString(), p.Category.Name);
        }
        Console.ReadKey();
    }
    private void ShowProductsByPrice()
    {
        List<Product> products = _database.GetProducts();
        foreach (Product p in products)
        {
            string categoryId = p.Category?.Id.ToString() ?? "No Category"; // Fallback if Category is null

            _productview.ShowProduct(p.Id.ToString(), p.Name, p.Price.ToString(), p.Stock.ToString(), categoryId);
        }
        Console.ReadKey();
    }
    private void ShowProductsByQuantity()
    {
        List<Product> products = _database.GetProducts();
        foreach (Product p in products)
        {
            string categoryId = p.Category?.Id.ToString() ?? "No Category"; // Fallback if Category is null

            _productview.ShowProduct(p.Id.ToString(), p.Name, p.Price.ToString(), p.Stock.ToString(), categoryId);
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
        foreach (Product p in mostExpensive)
        {
            string categoryId = p.Category?.Id.ToString() ?? "No Category"; // Fallback if Category is null
            _productview.ShowProduct(p.Id.ToString(), p.Name, p.Price.ToString(), p.Stock.ToString(), categoryId);
        }
        Console.ReadKey();
    }
    private void ShowLeastExpensiveProduct()
    {
        List<Product> LeastExpensive = _database.GetLeastExpensiveProduct();
        foreach (Product p in LeastExpensive)
        {
            string categoryId = p.Category?.Id.ToString() ?? "No Category"; // Fallback if Category is null
            _productview.ShowProduct(p.Id.ToString(), p.Name, p.Price.ToString(), p.Stock.ToString(), categoryId);
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
        var category = _database.GetCategoryById(categoryId);
        _database.AddProduct(name, price, stock, category);
    }
    private void ShowProductByName()
    {
        Console.WriteLine("Insert product name");
        string name = Console.ReadLine()!;
        Product product = _database.GetProductByName(name);
        string categoryId = product.Category?.Id.ToString() ?? "No Category"; // Fallback if Category is null

        _productview.ShowProduct(product.Id.ToString(), product.Name, product.Price.ToString(), product.Stock.ToString(), categoryId);
        Console.ReadKey();
    }
    private void ShowProductsByCategory()
{
    Console.WriteLine("Insert category Id");
    
    // Check for valid input
    if (!int.TryParse(Console.ReadLine(), out int categoryId))
    {
        Console.WriteLine("Invalid category ID. Please enter a number.");
        Console.ReadKey();
        return; // Exit the method if the input is not valid
    }

    List<Product> productsByCategory = _database.GetProductsByCategory(categoryId);

    if (productsByCategory == null || productsByCategory.Count == 0)
    {
        Console.WriteLine("No products found in this category.");
        Console.ReadKey();
        return; // Exit if no products are found
    }

    foreach (Product p in productsByCategory)
    {
        // Use the null conditional operator to safely access the category ID
        string categoryIdString = p.Category?.Id.ToString() ?? "No Category"; // Fallback if Category is null
        _productview.ShowProduct(p.Id.ToString(), p.Name, p.Price.ToString(), p.Stock.ToString(), categoryIdString);
    }

    Console.ReadKey();
}

}
