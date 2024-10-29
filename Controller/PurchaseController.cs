public class PurchaseController
{
    private PurchaseView _purchaseView;
    private Database _database;
    public PurchaseController(PurchaseView purchaseView, Database database)
    {
        _purchaseView = purchaseView;
        _database = database;
    }
    public void PurchaseMenu()
    {
        _database.LoadTables();
        bool exit = true;
        while (exit)
        {
            _purchaseView.ShowPurchaseMainMenu();
            string selection = Console.ReadLine()!;

            switch (selection)
            {
                case "1":
                    ShowPurchases();
                    break;
                case "2":
                    AddPurchase();
                    break;
                case "3":
                    exit = false;
                    break;
            }
        }
    }
    private void AddPurchase()
    {
        Console.WriteLine("Insert product Id");
        Int32.TryParse(Console.ReadLine()!, out int productId);
        var product = _database.GetProductById(productId);  // Use helper method

        if (product == null)
        {
            Console.WriteLine("Product not found.");
            return;
        }

        Console.WriteLine("Insert product quantity");
        Int32.TryParse(Console.ReadLine()!, out int quantity);

        if (quantity <= product.Stock)
        {
            Console.WriteLine("Insert customer Id");
            Int32.TryParse(Console.ReadLine()!, out int customerId);

            var customer = _database.GetCustomerById(customerId);  // Use helper method
            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            _database.AddPurchase(customer, product, quantity);
            Console.WriteLine("Purchase added successfully.");
        }
        else
        {
            Console.WriteLine("Not enough products in stock.");
        }
    }

    private void ShowPurchases()
    {
        List<Purchase> purchases = _database.GetPurchases();
        foreach (Purchase p in purchases)
        {
            _purchaseView.ShowPurchase(p.Id.ToString(), p.Date.ToString(), p.Quantity.ToString(), p.Customer.Surname, p.Product.Name);
        }
        Console.ReadKey();
    }
}
