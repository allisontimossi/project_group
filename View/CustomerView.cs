public class CustomerView
{
    public void ShowCustomerMainMenu(){
        Console.WriteLine("1 - Add Customer");
        Console.WriteLine("2 - Show Customers");
        Console.WriteLine("3 - Show Customers by surname");
        Console.WriteLine("4 - Delete Customer");
        Console.WriteLine("5 - Update Customer");
        Console.WriteLine("6 - Back");
        Console.WriteLine("Make your selection");
    }
    public void ShowCustomer(string id, string name, string surname, string email, string phoneNumber, string address)
    {
        Console.WriteLine($"ID: {id}, Name: {name}, Surname: {surname}, Email: {email}, Phone: {phoneNumber}, Address: {address}");
        Console.WriteLine("***********************************************************************");
    }
}
