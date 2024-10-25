using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


    public class CustomerController
    {
        private MyView _myView;
        private Database _database;
        public Controller(MyView myView, Database database)
        {
        _myView = myView;
        _database = database;
        }

        private void ShowCustomers()
        {
            using var reader = _database.GetCustomers();
            while (reader.Read())
            {
                _myView.ShowCustomers(reader["id"].ToString(), reader["name"].ToString(), reader["surname"].ToString(), reader["email"].ToString(), reader["address"].ToString(), reader["phoneNumber"].ToString());
            }
            _database.CloseConnection();
        }

        private void ShowProductsOrderedBySurname()
        {
            using var reader = _database.GetProductsOrderedBySurname();
            while (reader.Read())
            {
                _myView.ShowCustomers(reader["id"].ToString(), reader["name"].ToString(), reader["surname"].ToString());
            }
            _database.CloseConnection();
        }

        private void DeleteCustomer()
        {
            Console.WriteLine("Insert customer's name");
            string name = Console.ReadLine()!;
            _database.DeleteCustomer(name);
            _database.CloseConnection();        
        }

        private void AddCustomer()
        {
            Console.WriteLine("Insert customer's name");
            string name = Console.ReadLine()!;
            Console.WriteLine("Insert customer's surname");
            string surname = Console.ReadLine()!;
            Console.WriteLine("Insert customer's email");
            string email = Console.ReadLine()!;
            Console.WriteLine("Insert customer's address");
            string address = Console.ReadLine()!;
            Console.WriteLine("Insert customer's phone number");
            int phoneNumber = Console.ReadLine()!;
            _database.AddProduct(name, surname, email, address, phoneNumber);
            _database.CloseConnection();
        }

        private void UpdateCustomer()
        {
            Console.WriteLine("Insert ID's customer");

            ShowCustomers();

            int id = Console.ReadLine()!;

            if (!int.TryParse(id, out int id))
                    {
                        Console.WriteLine("Non valid ID.");
                        return;
                    }

            Console.WriteLine("Insert new customer's name");
            var newName = _view.GetInput();

            if (string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("Name cannot be null");
                return;
            }

            _db.UpdateCustomer(id, newName);

            Console.WriteLine("Updated customer");
            _database.CloseConnection();
        }
    }
