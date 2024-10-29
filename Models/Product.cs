public class Product: Entity
{
    public float Price {get;set;}    
    public int Stock {get;set;}  
    public int CategoryId { get; set; } // This property is inferred as a foreign key to the Subscription entity

    public Category Category {get;set;}    
}