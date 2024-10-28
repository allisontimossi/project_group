using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

    public class CategoryController
    {
        private MyView _myView;
        private Database _database;
        public CategoryController(MyView myView, Database database)
        {
        _myView = myView;
        _database = database;
        }

        private void ShowProductsByCategory()
        {
            Console.WriteLine("Insert category Id");
            string categoryId = Console.ReadLine()!;
            using var reader = _database.GetProductsByCategory(categoryId);
            while (reader.Read())
            {
                _myView.ShowProduct(reader["id"].ToString(), reader["name"].ToString(), reader["price"].ToString(), reader["stock"].ToString(), reader["category_id"].ToString());
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
