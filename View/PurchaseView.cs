public class PurchaseView
{
        public void ShowPurchaseMainMenu(){
        Console.WriteLine("1 - Show Purchases");
        Console.WriteLine("2 - Add Purchase");
        Console.WriteLine("3 - Back");
        Console.WriteLine("Make your selection");
    }
    public void ShowPurchase(string id, string date, string amount, string customerId, string productId)
    {
        Console.WriteLine($"ID: {id}, Date: {date}, Amount: {amount}, Customer ID: {customerId}, Product ID: {productId}");
        Console.WriteLine("***********************************************************************");
    }
}
