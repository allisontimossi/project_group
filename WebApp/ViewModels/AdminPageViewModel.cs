public class AdminViewModel
{
    public List<Product> Products { get; set; }

    public List<Customer> Customers { get; set; }

    public List<Brand> Brands { get; set; }

    private Database _database;

    public AdminViewModel()
        {

            Products = _database.GetProducts();
            Customers = _database.GetCustomers();
        
        }

}
