﻿class Program 
{
    static void Main(string[] args)
    {
        // Initialize the database context to interact with the database
        Database database = new Database();

        // Instantiate view classes for displaying information to the user
        CustomerView customerView = new CustomerView();
        CategoryView categoryView = new CategoryView();
        ProductView productView = new ProductView();
        PurchaseView purchaseView = new PurchaseView(); 
        MyView myView = new MyView();

        // Create controller instances for each entity to handle business logic
        CategoryController categoryController = new CategoryController(categoryView, database);
        CustomerController customerController = new CustomerController(customerView, database);
        ProductController productController = new ProductController(productView, database);
        PurchaseController purchaseController = new PurchaseController(purchaseView, database);

        // Initialize the main Controller with all other controllers and views
        Controller controller = new Controller(myView, categoryController, customerController, productController, purchaseController);

        // Start the main menu for user interaction
        controller.MainMenu();
    }
}
