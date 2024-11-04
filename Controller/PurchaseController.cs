public class PurchaseController
{
    private PurchaseView _purchaseView;
    private Database _database;

    // Constructor initializes the PurchaseView and Database instances.
    public PurchaseController(PurchaseView purchaseView, Database database)
    {
        _purchaseView = purchaseView;
        _database = database;
    }

    // Displays the purchase menu and handles user input for purchasing actions.
    public void PurchaseMenu()
    {
        _database.LoadProductsTable();
        _database.LoadPurchasesTable(); // Loads necessary tables from the database.
        bool exit = true;
        while (exit)
        {
            _purchaseView.ShowPurchaseMainMenu(); // Show the main menu for purchases.
            string selection = Console.ReadLine()!;

            switch (selection)
            {
                case "1":
                    ShowPurchases(); // Displays all purchases.
                    break;
                case "2":
                    AddPurchase(); // Initiates the process to add a new purchase.
                    break;
                case "3":
                    DeletePurchase(); // Initiates the process to add a new purchase.
                    break;
                case "4":
                    exit = false; // Exits the purchase menu.
                    break;
            }
        }
    }

    // Handles the process of adding a new purchase.
    private void AddPurchase()
    {
        Console.WriteLine("Insert product Id");
        Int32.TryParse(Console.ReadLine()!, out int productId);
        var product = _database.GetProductById(productId); // Retrieves product by ID.

        if (product == null)
        {
            Console.WriteLine("Product not found."); // Error handling if product doesn't exist.
            return;
        }

        Console.WriteLine("Insert product quantity");
        Int32.TryParse(Console.ReadLine()!, out int quantity);

        // Checks if sufficient stock is available for the purchase.
        if (quantity <= product.Stock)
        {
            Console.WriteLine("Insert customer Id");
            Int32.TryParse(Console.ReadLine()!, out int customerId);

            var customer = _database.GetCustomerById(customerId); // Retrieves customer by ID.
            if (customer == null)
            {
                Console.WriteLine("Customer not found."); // Error handling if customer doesn't exist.
                return;
            }

            // Adds the purchase to the database.
            _database.AddPurchase(customer, product, quantity);
            Console.WriteLine("Purchase added successfully.");
        }
        else
        {
            Console.WriteLine("Not enough products in stock."); // Error if insufficient stock.
        }
    }

    private void DeletePurchase()
    {
        Console.WriteLine("Insert purchase Id");
        Int32.TryParse(Console.ReadLine()!, out int purchaseId);

        // Call the database method to delete the purchase
        _database.DeletePurchase(purchaseId);
    }


    // Displays all the purchases made.
    private void ShowPurchases()
    {
        List<Purchase> purchases = _database.GetPurchases(); // Retrieves the list of purchases.
        foreach (Purchase p in purchases)
        {
            // Displays details of each purchase.
            _purchaseView.ShowPurchase(p.Id.ToString(), p.Customer.Name.ToString(), p.Product.Name.ToString(), p.Quantity.ToString(), p.Date.ToString());
        }
        Console.ReadKey(); // Waits for user input before returning to menu.
    }
}
