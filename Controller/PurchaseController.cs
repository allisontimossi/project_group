using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class PurchaseController
{
    private MyView _myView;
        private Database _database;
        public PurchaseController(MyView myView, Database database)
        {
        _myView = myView;
        _database = database;
        }
    
    private void AddPurchase(){
        Console.WriteLine("Insert product Id");
        Int32.TryParse(Console.ReadLine()!, out int productId);
        int stock = _database.CheckStock(productId);
        _database.CloseConnection();
        Console.WriteLine("Insert product quantity");
        Int32.TryParse(Console.ReadLine()!, out int quantity);
        if(quantity <= stock){
            Console.WriteLine("Insert customer Id");
            Int32.TryParse(Console.ReadLine()!, out int customerId);
            _database.AddPurchase(customerId, productId, quantity);
            _database.CloseConnection();
        }else{
            Console.WriteLine("Not enought products in stock");
        }
    }
    private void ShowPurchases(){
        using var reader = _database.GetPurchases();
        while (reader.Read())
        {
            _myView.ShowPurchase(reader["id"].ToString(), reader["customer_id"].ToString(), reader["product_id"].ToString(), reader["purchase_date"].ToString(), reader["quantity"].ToString());
        }
        _database.CloseConnection();
    }
}
