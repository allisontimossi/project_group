public class ProductView
{
    public void ShowProductMainMenu(){
        Console.WriteLine("1 - Show Products");
        Console.WriteLine("2 - Show Products By Name");
        Console.WriteLine("3 - Show Products By Price");
        Console.WriteLine("4 - Show Products By Quantity");
        Console.WriteLine("5 - Show Products By Category");
        Console.WriteLine("6 - Show Most Expensive Product");
        Console.WriteLine("7 - Show Least Expensive Product");
        Console.WriteLine("8 - Update Product Price");
        Console.WriteLine("9 - Delete Product");
        Console.WriteLine("10 - Add Product");
        Console.WriteLine("11 - Back");
        Console.WriteLine("Make your selection");
    }
    public void ShowProduct(string id, string name, string price, string stock, string categoryId)
    {
        Console.WriteLine($"ID: {id}, Name: {name}, Price: {price}, Stock: {stock}, Category ID: {categoryId}");
        Console.WriteLine("***********************************************************************");
    }
}
