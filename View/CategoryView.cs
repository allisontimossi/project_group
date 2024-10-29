public class CategoryView
{
    // Displays the main menu for category management options.
    public void ShowCategoryMenu()
    {
        Console.WriteLine("1 - Add Category"); // Option to add a new category.
        Console.WriteLine("2 - Show Categories"); // Option to display all categories.
        Console.WriteLine("3 - Delete Category"); // Option to remove a category.
        Console.WriteLine("4 - Back"); // Option to return to the previous menu.
        Console.WriteLine("Make your selection"); // Prompt for user input.
    }

    // Displays the details of a category in a formatted manner.
    public void ShowCategories(string id, string name)
    {
        // Outputs the category ID and name.
        Console.WriteLine($"ID: {id}, Name: {name}");
        Console.WriteLine("***********************************************************************"); // Divider for clarity.
    }
}
