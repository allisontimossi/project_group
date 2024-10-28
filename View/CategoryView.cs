public class CategoryView
{
    public void ShowCategoryMenu()
    {
        Console.WriteLine("1 - Add Category");
        Console.WriteLine("2 - Show Categories");
        Console.WriteLine("3 - Delete Category");
        Console.WriteLine("4 - Back");
        Console.WriteLine("Make your selection");
    }

    public void ShowCategories(string id, string name)
    {
        Console.WriteLine($"ID: {id}, Name: {name}");
        Console.WriteLine("***********************************************************************");
    }
}