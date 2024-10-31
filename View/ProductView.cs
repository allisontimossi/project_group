public class ProductView
{
    // Displays the main menu options for managing products
    public void ShowProductMainMenu()
    {
        Console.WriteLine("1 - Add Product");                    // Option to add a new product
        Console.WriteLine("2 - Delete Product");                 // Option to delete an existing product
        Console.WriteLine("3 - Show Products");                  // Option to display all products
        Console.WriteLine("4 - Show Products By Name");          // Option to display products by name
        Console.WriteLine("5 - Show Products By Price");         // Option to display products sorted by price
        Console.WriteLine("6 - Show Products By Quantity");      // Option to display products sorted by stock quantity
        Console.WriteLine("7 - Show Products By Category");      // Option to display products by category
        Console.WriteLine("8 - Show Most Expensive Product");    // Option to display the most expensive product
        Console.WriteLine("9 - Show Least Expensive Product");   // Option to display the least expensive product
        Console.WriteLine("10 - Update Product Price");          // Option to update the price of a product
        Console.WriteLine("11 - Back");                          // Option to return to the previous menu
        Console.WriteLine("Make your selection");                // Prompt for user input
    }

    // Displays detailed information for a specific product
    public void ShowProduct(string id, string name, string price, string stock, string category)
    {
        Console.WriteLine($"ID: {id}, Name: {name}, Price: {price}, Stock: {stock}, Category: {category}");
        Console.WriteLine("***********************************************************************"); // Divider for readability
    }
}
