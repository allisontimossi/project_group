public class PurchaseView
{
    public void ShowPurchase(string id, string date, string amount, string customerId, string productId)
    {
        Console.WriteLine($"ID: {id}, Date: {date}, Amount: {amount}, Customer ID: {customerId}, Product ID: {productId}");
        Console.WriteLine("***********************************************************************");
    }
}
