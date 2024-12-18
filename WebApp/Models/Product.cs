public class Product
{
    public float Price { get; set; }
    public int Stock { get; set; }
    public string Material { get; set; }

    public string Size { get; set; }

    public string Color { get; set; }

    public int BrandId { get; set; }
    public Brand Brand { get; set; }

    public bool IsApproved { get; set; }
}