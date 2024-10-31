public class MyView
{
    // Displays the main menu options for navigating different management sections
    public void ShowMainMenu()
    {
        Console.WriteLine("1 - Manage Products");      // Option to go to product management
        Console.WriteLine("2 - Manage Categories");    // Option to go to category management
        Console.WriteLine("3 - Manage Customers");     // Option to go to customer management
        Console.WriteLine("4 - Manage Purchases");     // Option to go to purchase management
        Console.WriteLine("5 - Exit");                 // Option to exit the application
    }
}
