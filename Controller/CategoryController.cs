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


    }

        private void ShowCategory()
        {
            List<Category> categories = _database.GetCategories();
            foreach(Category c in categories)
            {
                _categoryView.ShowCategories(c.Id.ToString(), c.Name);
            }
            Console.ReadKey();
        }
}
