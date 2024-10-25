public class CustomerView
{
    public void ShowCustomer(string id, string name, string surname, string email, string phoneNumber, string address)
    {
        Console.WriteLine($"ID: {id}, Name: {name}, Surname: {surname}, Email: {email}, Phone: {phoneNumber}, Address: {address}");
        Console.WriteLine("***********************************************************************");
    }
}
