using System.Data.SQLite;
using System.Runtime.CompilerServices;

public class Controller
{
    private CategoryController _categoryController;
    private CustomerController _customerController;
    private ProductController _productController;
    private PurchaseController _purchaseController;
    private MyView _myView;
    public Controller(MyView myView, CategoryController categoryController, CustomerController customerController, ProductController productController, 
                        PurchaseController purchaseController){
        _categoryController = categoryController;
        _customerController = customerController;
        _productController = productController;
        _purchaseController = purchaseController;
        _myView = myView;
    }
    public void MainMenu(){
        bool exit = true;
        while(exit)
        {
            _myView.ShowMainMenu();
            string selection = Console.ReadLine()!;
            switch (selection)
            {
                case "1":
                    _productController.ProductMenu();
                    break;
                case "2":
                    _categoryController.CategoryMenu();
                    break;
                case "3":
                    _customerController.CustomerMenu();
                    break;
                case "4":
                    _purchaseController.PurchaseMenu();
                    break;
                case "5":
                    exit = false;
                    break;
            }
        }
        return;
    }
}