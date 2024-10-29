using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


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
        bool exit = true;
        while (exit)
        {
            _purchaseView.ShowPurchaseMainMenu();
            string selection = Console.ReadLine()!;

            switch (selection)
            {
                case "1":
                    ShowPurchase();
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
    var product = _database.Products.Find(productId);
    
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
        
        var customer = _database.Customers.Find(customerId);
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

        public void ShowPurchase()
    {
        var purchases = _database.Purchases.ToList();
        _purchaseView.ShowPurchases(purchases);
    }
}
