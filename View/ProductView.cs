public class ProductView
{
    public void ShowProduct(string id, string name, string price, string stock, string categoryId)
    {
        Console.WriteLine($"ID: {id}, Name: {name}, Price: {price}, Stock: {stock}, Category ID: {categoryId}");
        Console.WriteLine("***********************************************************************");
    }
}
