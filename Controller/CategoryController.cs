using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CategoryController
{
    private MyView _myView;
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
            _categoryView.ShowCategoryMenu();
            string selection = Console.ReadLine()!;

            switch (selection)
            {
                case "1":
                    AddCategory();
                    break;
                case "2":
                    ShowCategories();
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


    public void ShowCategories()
    {
        using var reader = _database.GetCategories();
        while (reader.Read())
        {
            _categoryView.ShowCategories(reader["id"].ToString(), reader["name"].ToString());
        }
        _database.CloseConnection();
    }
    private void AddCategory()
    {
        Console.WriteLine("Insert category name");
        string name = Console.ReadLine()!;
        _database.AddCategory(name);
        _database.CloseConnection();

    }
    private void DeleteCategory()
    {
        Console.WriteLine("inserisci il nome della categoria");
        string name = Console.ReadLine()!;
        _database.DeleteCategory(name);
        _database.CloseConnection();
    }
}
