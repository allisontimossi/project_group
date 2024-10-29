public class CategoryController
{
    // View instance to display category-related information.
    private CategoryView _categoryView;

    // Database instance for data operations related to categories.
    private Database _database;

    // Constructor to initialize CategoryController with a CategoryView and Database instance.
    public CategoryController(CategoryView categoryView, Database database)
    {
        _categoryView = categoryView; // Assign the category view instance.
        _database = database; // Assign the database instance.
    }

    // Main method to display the category management menu and handle user selection.
    public void CategoryMenu()
    {
        bool exit = true; // Control variable for the menu loop.
        while (exit)
        {
            Console.Clear(); // Clear the console for a fresh menu display.
            _categoryView.ShowCategoryMenu(); // Display the category menu options.
            string selection = Console.ReadLine()!; // Read user input for menu selection.

            // Handle user selection using a switch statement.
            switch (selection)
            {
                case "1": // Case for adding a new category.
                    AddCategory();
                    break;
                case "2": // Case for showing all categories.
                    ShowCategory();
                    break;
                case "3": // Case for deleting a category.
                    DeleteCategory();
                    break;
                case "4": // Case to exit the menu.
                    exit = false;
                    break; // Exit the loop and return to the previous menu.
            }
        }
    }

    // Method to add a new category to the database.
    private void AddCategory()
    {
        Console.WriteLine("Insert category name"); // Prompt user for category name input.
        string name = Console.ReadLine()!; // Read the name input.
        _database.AddCategory(name); // Add the new category to the database.
    }

    // Method to delete a category from the database by its ID.
    private void DeleteCategory()
    {
        Console.WriteLine("Enter the ID of the Category you want to delete:"); // Prompt user for category ID input.
        int id = Convert.ToInt32(Console.ReadLine()!); // Read and convert the input to an integer.
        _database.DeleteCategory(id); // Delete the category from the database.
    }

    // Method to display all categories to the user.
    private void ShowCategory()
    {
        List<Category> categories = _database.GetCategories(); // Retrieve the list of categories from the database.
        foreach (Category c in categories) // Iterate through each category.
        {
            _categoryView.ShowCategories(c.Id.ToString(), c.Name); // Display the category ID and name.
        }
        Console.ReadKey(); // Wait for user input before proceeding.
    }
}
