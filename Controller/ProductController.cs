public class ProductController
{
    private ProductView _productview; // View for displaying product information
    private Database _database; // Database context for accessing data

    // Constructor initializes the ProductView and Database
    public ProductController(ProductView productview, Database database)
    {
        _productview = productview;
        _database = database;
    }

    // Main menu for managing products
    public void ProductMenu()
    {
        _database.LoadTables(); // Load initial data from the database
        bool exit = true; // Control variable for the menu loop

        while (exit) // Loop until user chooses to exit
        {
            Console.Clear(); // Clear the console for fresh output
            _productview.ShowProductMainMenu(); // Show the product menu
            string selection = Console.ReadLine()!; // Read user selection

            switch (selection)
            {
                case "1":
                    AddProduct(); // Add a new product
                    break;
                case "2":
                    DeleteProduct(); // Delete an existing product
                    break;
                case "3":
                    ShowProducts(); // Show all products
                    break;
                case "4":
                    ShowProductByName(); // Show a product by its name
                    break;
                case "5":
                    ShowProductsByPrice(); // Show products sorted by price
                    break;
                case "6":
                    ShowProductsByQuantity(); // Show products sorted by stock quantity
                    break;
                case "7":
                    ShowProductsByCategory(); // Show products by category
                    break;
                case "8":
                    ShowMostExpensiveProduct(); // Show the most expensive product(s)
                    break;
                case "9":
                    ShowLeastExpensiveProduct(); // Show the least expensive product(s)
                    break;
                case "10":
                    UpdateProductPrice(); // Update the price of a product
                    break;
                case "11":
                    exit = false; // Exit the menu
                    break;
            }
        }
    }

    // Displays all products in the console
    private void ShowProducts()
    {
        List<Product> products = _database.GetProducts(); // Retrieve all products
        foreach (Product p in products)
        {
            _productview.ShowProduct(p.Id.ToString(), p.Name, p.Price.ToString(), p.Stock.ToString(), p.Category.Name); // Show each product
        }
        Console.ReadKey(); // Wait for user input
    }

    // Displays products sorted by price
    private void ShowProductsByPrice()
    {
        List<Product> products = _database.GetProductsByPrice(); // Retrieve products sorted by price
        foreach (Product p in products)
        {
            _productview.ShowProduct(p.Id.ToString(), p.Name, p.Price.ToString(), p.Stock.ToString(), p.Category.Name); // Show each product
        }
        Console.ReadKey(); // Wait for user input
    }

    // Displays products sorted by stock quantity
    private void ShowProductsByQuantity()
    {
        List<Product> products = _database.GetProductsByStock(); // Retrieve products sorted by stock
        foreach (Product p in products)
        {
            _productview.ShowProduct(p.Id.ToString(), p.Name, p.Price.ToString(), p.Stock.ToString(), p.Category.Name); // Show each product
        }
        Console.ReadKey(); // Wait for user input
    }

    // Updates the price of a product by its name
    private void UpdateProductPrice()
    {
        Console.WriteLine("Insert product name"); // Prompt for product name
        string name = Console.ReadLine()!; // Read product name
        Console.WriteLine("Insert new price"); // Prompt for new price
        int price = Convert.ToInt32(Console.ReadLine()!); // Read new price
        _database.UpdateProductPrice(name, price); // Update the price in the database
    }

    // Deletes a product by its name
    private void DeleteProduct()
    {
        Console.WriteLine("Insert product name"); // Prompt for product name
        string name = Console.ReadLine()!; // Read product name
        _database.DeleteProduct(name); // Delete the product from the database
    }

    // Displays the most expensive product(s)
    private void ShowMostExpensiveProduct()
    {
        List<Product> mostExpensive = _database.GetMostExpensiveProduct(); // Retrieve the most expensive product(s)
        foreach (Product p in mostExpensive)
        {
            _productview.ShowProduct(p.Id.ToString(), p.Name, p.Price.ToString(), p.Stock.ToString(), p.Category.Name); // Show each product
        }
        Console.ReadKey(); // Wait for user input
    }

    // Displays the least expensive product(s)
    private void ShowLeastExpensiveProduct()
    {
        List<Product> leastExpensive = _database.GetLeastExpensiveProduct(); // Retrieve the least expensive product(s)
        foreach (Product p in leastExpensive)
        {
            _productview.ShowProduct(p.Id.ToString(), p.Name, p.Price.ToString(), p.Stock.ToString(), p.Category.Name); // Show each product
        }
        Console.ReadKey(); // Wait for user input
    }

    // Adds a new product to the database
    private void AddProduct()
    {
        Console.WriteLine("Insert product name"); // Prompt for product name
        string name = Console.ReadLine()!; // Read product name
        Console.WriteLine("Insert product price"); // Prompt for product price
        int price = Convert.ToInt32(Console.ReadLine()!); // Read product price
        Console.WriteLine("Insert product quantity"); // Prompt for product stock
        int stock = Convert.ToInt32(Console.ReadLine()!); // Read product stock
        Console.WriteLine("Insert category Id"); // Prompt for category ID
        int categoryId = Convert.ToInt32(Console.ReadLine()!); // Read category ID
        Category category = _database.GetCategoryById(categoryId); // Get category by ID
        _database.AddProduct(name, price, stock, category); // Add product to the database
    }

    // Displays a product by its name
    private void ShowProductByName()
    {
        Console.WriteLine("Insert product name"); // Prompt for product name
        string name = Console.ReadLine()!; // Read product name
        Product product = _database.GetProductByName(name); // Retrieve the product by name
        _productview.ShowProduct(product.Id.ToString(), product.Name, product.Price.ToString(), product.Stock.ToString(), product.Category.Name); // Show the product details
        Console.ReadKey(); // Wait for user input
    }

    // Displays products belonging to a specific category
    private void ShowProductsByCategory()
    {
        Console.WriteLine("Insert category Id"); // Prompt for category ID
        if (!int.TryParse(Console.ReadLine(), out int categoryId)) // Validate input
        {
            Console.WriteLine("Invalid category ID. Please enter a number."); // Error message for invalid input
            Console.ReadKey(); // Wait for user input
            return; // Exit the method
        }

        List<Product> productsByCategory = _database.GetProductsByCategory(categoryId); // Retrieve products by category

        if (productsByCategory == null || productsByCategory.Count == 0) // Check if any products were found
        {
            Console.WriteLine("No products found in this category."); // Inform the user
            Console.ReadKey(true); // Wait for user input
            return; // Exit the method
        }

        foreach (Product p in productsByCategory)
        {
            _productview.ShowProduct(p.Id.ToString(), p.Name, p.Price.ToString(), p.Stock.ToString(), p.Category.Name); // Show each product
        }
        Console.ReadKey(); // Wait for user input
    }
}
