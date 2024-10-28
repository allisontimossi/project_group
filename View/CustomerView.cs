public class CustomerView
{
    public void ShowCustomerMainMenu(){
        Console.WriteLine("1 - Show Customers");
        Console.WriteLine("2 - Show Customers By Surname");
        Console.WriteLine("3 - Delete Customer by Id");
        Console.WriteLine("4 - Add Customer");
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
