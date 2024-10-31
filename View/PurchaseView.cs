public class PurchaseView
{
    // Displays the main menu options for managing purchases
    public void ShowPurchaseMainMenu()
    {
        Console.WriteLine("1 - Show Purchases");       // Option to display all purchases
        Console.WriteLine("2 - Add Purchase");         // Option to add a new purchase
        Console.WriteLine("3 - Back");                 // Option to return to the previous menu
        Console.WriteLine("Make your selection");      // Prompt for user input
    }

    // Displays detailed information for a specific purchase
    public void ShowPurchase(string id, string date, string amount, string customerId, string product)
    {
        Console.WriteLine($"ID: {id}, Date: {date}, Amount: {amount}, Customer Surname: {customerId}, Product Name: {product}");
        Console.WriteLine("***********************************************************************"); // Divider for readability
    }
}
