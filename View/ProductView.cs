public class ProductView
{
    public void ShowProductMainMenu(){
        Console.WriteLine("1 - Add Product");
        Console.WriteLine("2 - Delete Product");
        Console.WriteLine("3 - Show Products");
        Console.WriteLine("4 - Show Products By Name");
        Console.WriteLine("5 - Show Products By Price");
        Console.WriteLine("6 - Show Products By Quantity");
        Console.WriteLine("7 - Show Products By Category");
        Console.WriteLine("8 - Show Most Expensive Product");
        Console.WriteLine("9 - Show Least Expensive Product");
        Console.WriteLine("10 - Update Product Price");
        Console.WriteLine("11 - Back");
        Console.WriteLine("Make your selection");
    }
    public void ShowProduct(string id, string name, string price, string stock, string category)
    {
        Console.WriteLine($"ID: {id}, Name: {name}, Price: {price}, Stock: {stock}, Category: {category}");
        Console.WriteLine("***********************************************************************");
    }
}
