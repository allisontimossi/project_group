public class CustomerView
{
    // Displays the main menu options for managing customers
    public void ShowCustomerMainMenu()
    {
        Console.WriteLine("1 - Add Customer");         // Option to add a new customer
        Console.WriteLine("2 - Show Customers");        // Option to display all customers
        Console.WriteLine("3 - Show Customers by surname"); // Option to search customers by surname
        Console.WriteLine("4 - Delete Customer");       // Option to delete a customer
        Console.WriteLine("5 - Update Customer");       // Option to update customer details
        Console.WriteLine("6 - Back");                  // Option to return to the previous menu
        Console.WriteLine("Make your selection");       // Prompt for user input
    }

    // Displays the details of a specific customer in a formatted manner
    public void ShowCustomer(string id, string name, string surname, string email, string phoneNumber, string address)
    {
        Console.WriteLine($"ID: {id}, Name: {name}, Surname: {surname}, Email: {email}, Phone: {phoneNumber}, Address: {address}");
        Console.WriteLine("***********************************************************************"); // Divider for readability
    }
}
