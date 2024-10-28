using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


    public class CustomerController
    {
        private CustomerView _customerView;
        private Database _database;
        public CustomerController(CustomerView customerView, Database database)
        {
        _customerView = customerView;
        _database = database;
        }

        public void CustomerMenu()
    {
        bool exit = true;
        while (exit)
        {
            _customerView.ShowCustomerMainMenu();
            string selection = Console.ReadLine()!;

            switch (selection)
            {
                case "1":
                    AddCustomer();
                    break;
                case "2":
                    ShowCustomers();
                    break;
                case "3":
                    ShowCustomersOrderedBySurname();
                    break;
                case "4":
                    DeleteCustomer();
                    break;
                case "5":
                    AddCustomer();
                    break;
                case "6":
                    UpdateCustomer();
                    break;
                case "7":
                    exit = false;
                    break;
            }
        }
    }

        private void ShowCustomers()
        {
            using var reader = _database.GetCustomers();
            while (reader.Read())
            {
                _customerView.ShowCustomer(reader["id"].ToString(), reader["name"].ToString(), reader["surname"].ToString(), reader["email"].ToString(), reader["address"].ToString(), reader["phoneNumber"].ToString());
            }
            _database.CloseConnection();
        }

        private void ShowCustomersOrderedBySurname()
        {
            Console.WriteLine("Insert customer surname");
            string surname = Console.ReadLine()!;
            using var reader = _database.GetCustomerBySurname(surname);
            while (reader.Read())
            {
                _customerView.ShowCustomer(reader["id"].ToString(), reader["name"].ToString(), reader["surname"].ToString(), reader["email"].ToString(), reader["address"].ToString(), reader["phoneNumber"].ToString());;
            }
            _database.CloseConnection();
        }

        private void DeleteCustomer()
        {
            ShowCustomers();
            Console.WriteLine("Insert customer's ID");
            string id = Console.ReadLine()!;

            if (!int.TryParse(id, out int idConverted))
            {
                Console.WriteLine("Non valid ID.");
                return;
            }
            
            _database.DeleteCustomer(idConverted);
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
            string phoneNumber = Console.ReadLine()!;
            Console.WriteLine("Insert customer's code");
            string clientCode = Console.ReadLine()!;
            _database.AddCustomer(name, surname, email, address, phoneNumber, clientCode);
            _database.CloseConnection();
        }

        private void UpdateCustomer()
        {
            Console.WriteLine("Insert ID's customer");

            ShowCustomers();

            if (!int.TryParse(Console.ReadLine()!, out int id))
            {
                Console.WriteLine("Non valid ID.");
                return;
            }

            Console.WriteLine("Insert new customer's name");
            var newName = Console.ReadLine()!;

            if (string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("Name cannot be null");
                return;
            }

           // _database.UpdateCustomer(id, newName);

            Console.WriteLine("Updated customer");
            _database.CloseConnection();
        }
    }
