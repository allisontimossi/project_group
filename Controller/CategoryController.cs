using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CategoryController
{
    private CategoryView _categoryView;
    private Database _database;
    public CategoryController(CategoryView categoryView, Database database)
    {
        _categoryView = categoryView;
        _database = database;
    }

    public void CategoryMenu()
    {
        bool exit = true;
        while (exit)
        {
            Console.Clear();
            _categoryView.ShowCategoryMenu();
            string selection = Console.ReadLine()!;

            switch (selection)
            {
                case "1":
                    AddCategory();
                    break;
                case "2":
                    ShowCategory();
                    break;
                case "3":
                    DeleteCategory();
                    break;
                case "4":
                    exit = false;
                    break;
            }
        }
    }

    private void AddCategory()
    {
        Console.WriteLine("Insert category name");
        string name = Console.ReadLine()!;
        _database.AddCategory(name);
    }
    private void DeleteCategory()
    {
        Console.WriteLine("Enter the ID of the Category u want to delete:");
        int id = Convert.ToInt32(Console.ReadLine()!);

        Category catId = null;
        foreach (var cat in _database.Categories)
        {
            if (cat.Id == id)
            {
                catId = cat;
            }
        }
        if (catId != null)
        {
            _database.Categories.Remove(catId);
            _database.SaveChanges();
            Console.WriteLine("Category succesfully deleted:");
            Console.WriteLine("Press any key to continue:");
            Console.ReadKey(true);
        }
    }

        public void ShowCategory()
    {
        var categories = _database.Categories.ToList();
        _categoryView.ShowCategories(categories);
    }
}
