public class Controller
{
    // Fields representing controllers for each entity, responsible for handling business logic
    private CategoryController _categoryController;
    private CustomerController _customerController;
    private ProductController _productController;
    private PurchaseController _purchaseController;

    // Field for the main view, which handles the display of the main menu
    private MyView _myView;

    // Constructor initializes all the entity controllers and the main view
    public Controller(MyView myView, CategoryController categoryController, CustomerController customerController, 
                      ProductController productController, PurchaseController purchaseController)
    {
        _categoryController = categoryController;     // Controller for category-related logic
        _customerController = customerController;     // Controller for customer-related logic
        _productController = productController;       // Controller for product-related logic
        _purchaseController = purchaseController;     // Controller for purchase-related logic
        _myView = myView;                             // Main view to show the menu
    }

    // Main menu for navigating different entity management sections
    public void MainMenu()
    {
        bool exit = true; // Control variable for menu loop to allow repeated display of menu until exit is chosen

        while (exit) // Loop continues until the user chooses to exit
        {
            Console.Clear(); // Clears the console screen for a fresh display of the main menu
            _myView.ShowMainMenu(); // Displays the main menu options using MyView
            
            string selection = Console.ReadLine()!; // Reads the user's menu selection
            
            // Handle the user's selection with a switch statement
            switch (selection)
            {
                case "1":
                    _productController.ProductMenu(); // Navigate to the product management menu
                    break;
                case "2":
                    _categoryController.CategoryMenu(); // Navigate to the category management menu
                    break;
                case "3":
                    _customerController.CustomerMenu(); // Navigate to the customer management menu
                    break;
                case "4":
                    _purchaseController.PurchaseMenu(); // Navigate to the purchase management menu
                    break;
                case "5":
                    exit = false; // Set exit to false to exit the loop and end the main menu
                    break;
            }
        }
        return; // Return control to the calling function (if any)
    }
}
