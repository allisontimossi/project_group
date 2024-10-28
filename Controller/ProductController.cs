using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


    public class ProductController
    {
        private ProductView _productview;
        private MyView _myView;
        private Database _database;
        public ProductController(ProductView productview, Database database)
        {
        _productview = productview;
        _database = database;
        }

public void ProductMenu()
    {
        bool exit = true;
        while (exit)
        {
            _productview.ShowProductMainMenu();
            string selection = Console.ReadLine()!;

            switch (selection)
            {
                case "1":
                    ShowProducts();
                    break;
                case "2":
                    ShowProductByName();
                    break;
                case "3":
                    ShowProductsOrderedByPrice();
                    break;
                case "4":
                    ShowProductsOrderedByQuantity();
                    break;
                case "5":
                    ShowProductsByCategory();
                    break;
                case "6":
                    ShowMostExpensiveProduct();
                    break;
                case "7":
                    ShowLeastExpensiveProduct();
                    break;
                case "8":
                    UpdateProductPrice();
                    break;
                case "9":
                    DeleteProduct();
                    break;
                case "10":
                    AddProduct();
                    break;
                case "11":
                    exit = false;
                    break;
            }
        }
    }

        private void ShowProducts()
        {
            using var reader = _database.GetProducts();
            while (reader.Read())
            {
                _productview.ShowProduct(reader["id"].ToString(), reader["name"].ToString(), reader["price"].ToString(), reader["stock"].ToString(), reader["category_id"].ToString());
            }
            _database.CloseConnection();
        }
        private void ShowProductsOrderedByPrice()
        {
            using var reader = _database.GetProductsOrderedByPrice();
            while (reader.Read())
            {
                _productview.ShowProduct(reader["id"].ToString(), reader["name"].ToString(), reader["price"].ToString(), reader["stock"].ToString(), reader["category_id"].ToString());
            }
            _database.CloseConnection();
        }
        private void ShowProductsOrderedByQuantity()
        {
            using var reader = _database.GetProductsOrderedByStock();
            while (reader.Read())
            {
                _productview.ShowProduct(reader["id"].ToString(), reader["name"].ToString(), reader["price"].ToString(), reader["stock"].ToString(), reader["category_id"].ToString());
            }
            _database.CloseConnection();
        }
        private void UpdateProductPrice()
        {
            Console.WriteLine("Insert product name");
            string name = Console.ReadLine()!;
            Console.WriteLine("Insert new price");
            string price = Console.ReadLine()!;
            _database.UpdateProductPrice(name, price);
            _database.CloseConnection();
        }
        private void DeleteProduct()
        {
            Console.WriteLine("Insert product name");
            string name = Console.ReadLine()!;
            _database.DeleteProduct(name);
            _database.CloseConnection();        
        }
        private void ShowMostExpensiveProduct()
        {
            using var reader = _database.GetMostExpensiveProduct();
            while (reader.Read())
            {
                _productview.ShowProduct(reader["id"].ToString(), reader["name"].ToString(), reader["price"].ToString(), reader["stock"].ToString(), reader["category_id"].ToString());
            }
            _database.CloseConnection();
        }
        private void ShowLeastExpensiveProduct()
        {
            using var reader = _database.GetLeastExpensiveProduct();
            while (reader.Read())
            {
                _productview.ShowProduct(reader["id"].ToString(), reader["name"].ToString(), reader["price"].ToString(), reader["stock"].ToString(), reader["category_id"].ToString());
            }
            _database.CloseConnection();
        }
        private void AddProduct()
        {
            Console.WriteLine("Insert product name");
            string name = Console.ReadLine()!;
            Console.WriteLine("Insert product price");
            string price = Console.ReadLine()!;
            Console.WriteLine("Insert product quantity");
            string quantity = Console.ReadLine()!;
            Console.WriteLine("Insert category Id");
            string categoryId = Console.ReadLine()!;
            _database.AddProduct(name, price, quantity, categoryId);
            _database.CloseConnection();
        }
        private void ShowProductByName()
        {
            Console.WriteLine("Insert product name");
            string name = Console.ReadLine()!;
            using var reader = _database.GetProductByName(name);
            while (reader.Read())
            {
                _productview.ShowProduct(reader["id"].ToString(), reader["name"].ToString(), reader["price"].ToString(), reader["stock"].ToString(), reader["category_id"].ToString());
            }
            _database.CloseConnection();        
        }
        private void ShowProductsByCategory()
        {
            Console.WriteLine("Insert category Id");
            string categoryId = Console.ReadLine()!;
            using var reader = _database.GetProductsByCategory(categoryId);
            while (reader.Read())
            {
                _productview.ShowProduct(reader["id"].ToString(), reader["name"].ToString(), reader["price"].ToString(), reader["stock"].ToString(), reader["category_id"].ToString());
            }
            _database.CloseConnection();        
        }
    }
