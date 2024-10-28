﻿class Program 
{
    private static string _path = @"database.db";
    static void Main(string[] args)
    {
        Database database = new Database();
        CustomerView customerView = new CustomerView();
        CategoryView categoryView = new CategoryView();
        ProductView productView = new ProductView();
        PurchaseView purchaseView = new PurchaseView(); 
        MyView myView = new MyView();
        CategoryController categoryController = new CategoryController(categoryView, database);
        CustomerController customerController = new CustomerController(customerView, database);
        ProductController productController = new ProductController(productView, database);
        PurchaseController purchaseController = new PurchaseController(purchaseView, database);
        Controller controller = new Controller(myView, categoryController, customerController, productController, purchaseController);
        //database.CreateDatabase(_path);
        controller.MainMenu();
    }
}
