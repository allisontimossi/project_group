public class CustomerController
{
    // View instance to display customer-related information.
    private CustomerView _customerView;

    // Database instance for data operations related to customers.
    private Database _database;

    // Constructor to initialize CustomerController with a CustomerView and Database instance.
    public CustomerController(CustomerView customerView, Database database)
    {
        _customerView = customerView; // Assign the customer view instance.
        _database = database; // Assign the database instance.
    }

    // Main method to display the customer management menu and handle user selection.
    public void CustomerMenu()
    {
        bool exit = true; // Control variable for the menu loop.
        while (exit)
        {
            Console.Clear(); // Clear the console for a fresh menu display.
            _customerView.ShowCustomerMainMenu(); // Display the customer menu options.
            string selection = Console.ReadLine()!; // Read user input for menu selection.

            // Handle user selection using a switch statement.
            switch (selection)
            {
                case "1": // Case for adding a new customer.
                    AddCustomer();
                    break;
                case "2": // Case for showing all customers.
                    ShowCustomers();
                    break;
                case "3": // Case for showing customers by surname.
                    ShowCustomersBySurname();
                    break;
                case "4": // Case for deleting a customer.
                    DeleteCustomer();
                    break;
                case "5": // Case for updating customer information.
                    UpdateCustomer();
                    break;
                case "6": // Case to exit the menu.
                    exit = false;
                    break; // Exit the loop and return to the previous menu.
            }
        }
    }

    // Method to add a new customer to the database.
    private void AddCustomer()
    {
        Console.WriteLine("Insert customer's name"); // Prompt user for customer's name.
        string name = Console.ReadLine()!; // Read the name input.

        Console.WriteLine("Insert customer's surname"); // Prompt user for customer's surname.
        string surname = Console.ReadLine()!; // Read the surname input.

        Console.WriteLine("Insert customer's email"); // Prompt user for customer's email.
        string email = Console.ReadLine()!; // Read the email input.

        Console.WriteLine("Insert customer's address"); // Prompt user for customer's address.
        string address = Console.ReadLine()!; // Read the address input.

        Console.WriteLine("Insert customer's phone number"); // Prompt user for customer's phone number.
        string phoneNumber = Console.ReadLine()!; // Read and convert the input to a long.

        Console.WriteLine("Insert customer's code"); // Prompt user for customer's client code.
        string clientCode = Console.ReadLine()!; // Read the client code input.

        // Add the new customer to the database.
        _database.AddCustomer(name, surname, email, phoneNumber, address, clientCode);
    }

    // Method to display all customers to the user.
    private void ShowCustomers()
    {
        List<Customer> customers = _database.GetCustomers(); // Retrieve the list of customers from the database.
        foreach (Customer c in customers) // Iterate through each customer.
        {
            // Display the customer's details.
            _customerView.ShowCustomer(c.Id.ToString(), c.Name, c.Surname, c.Email, c.PhoneNumber.ToString(), c.Address);
        }
        Console.ReadKey(); // Wait for user input before proceeding.
    }

    // Method to show customers filtered by their surname.
    private void ShowCustomersBySurname()
    {
        Console.WriteLine("Insert customer surname"); // Prompt user for customer surname input.
        string surname = Console.ReadLine()!; // Read the surname input.

        List<Customer> customers = _database.GetCustomers(); // Retrieve the list of customers from the database.
        foreach (Customer c in customers) // Iterate through each customer.
        {
            if (c.Surname == surname) // Check if the surname matches.
            {
                // Display the customer's details if the surname matches.
                _customerView.ShowCustomer(c.Id.ToString(), c.Name, c.Surname, c.Email, c.PhoneNumber.ToString(), c.Address);
            }
        }
        Console.ReadKey(); // Wait for user input before proceeding.
    }

    // Method to delete a customer from the database by their ID.
    private void DeleteCustomer()
    {
        ShowCustomers(); // Display all customers for reference.
        Console.WriteLine("Insert customer's ID"); // Prompt user for customer ID input.
        string id = Console.ReadLine()!; // Read the ID input.

        // Validate that the input is a valid integer.
        if (!int.TryParse(id, out int idConverted))
        {
            Console.WriteLine("Non valid ID."); // Inform the user if the ID is invalid.
            return; // Exit the method if invalid.
        }
        _database.DeleteCustomer(idConverted); // Delete the customer from the database by ID.
    }

    // Method to update an existing customer's information.
    private void UpdateCustomer()
    {
        ShowCustomers(); // Display all customers for reference.
        Console.WriteLine("Insert ID's customer"); // Prompt user for the customer's ID.

        // Validate that the input is a valid integer.
        if (!int.TryParse(Console.ReadLine()!, out int id))
        {
            Console.WriteLine("Non valid ID."); // Inform the user if the ID is invalid.
            return; // Exit the method if invalid.
        }

        Console.WriteLine("Insert new customer's name"); // Prompt user for the new customer's name.
        var newName = Console.ReadLine()!; // Read the new name input.

        // Validate that the new name is not null or whitespace.
        if (string.IsNullOrWhiteSpace(newName))
        {
            Console.WriteLine("Name cannot be null"); // Inform the user if the name is invalid.
            return; // Exit the method if invalid.
        }

        _database.UpdateCustomer(id, newName); // Update the customer's name in the database.
        Console.WriteLine("Customer updated"); // Inform the user of the successful update.
        Console.ReadKey(); // Wait for user input before proceeding.
    }
}
