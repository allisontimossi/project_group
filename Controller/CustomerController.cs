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
            Console.Clear();
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
                    ShowCustomersBySurname();
                    break;
                case "4":
                    DeleteCustomer();
                    break;
                case "5":
                    UpdateCustomer();
                    break;
                case "6":
                    exit = false;
                    break;
            }
        }
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
        Int64 phoneNumber = Convert.ToInt64(Console.ReadLine()!);
        Console.WriteLine("Insert customer's code");
        string clientCode = Console.ReadLine()!;
        _database.AddCustomer(name, surname, email, phoneNumber, address, clientCode);
    }
    private void ShowCustomers()
    {
        List<Customer> customers = _database.GetCustomers();
        foreach (Customer c in customers)
        {
            _customerView.ShowCustomer(c.Id.ToString(), c.Name, c.Surname, c.Email, c.PhoneNumber.ToString(), c.Address);
        }
        Console.ReadKey();
    }
    private void ShowCustomersBySurname()
    {
        Console.WriteLine("Insert customer surname");
        string surname = Console.ReadLine()!;

        List<Customer> customers = _database.GetCustomers();
        foreach (Customer c in customers)
        {
            if (c.Surname == surname)
            {
                _customerView.ShowCustomer(c.Id.ToString(), c.Name, c.Surname, c.Email, c.PhoneNumber.ToString(), c.Address);
            }
        }
        Console.ReadKey();
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
    }
    private void UpdateCustomer()
    {
        ShowCustomers();
        Console.WriteLine("Insert ID's customer");

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

        _database.UpdateCustomer(id, newName);
        Console.WriteLine("Customer updated");
        Console.ReadKey();
    }
}