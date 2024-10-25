public class MyView
{
    public void ShowMainMenu(){
        Console.WriteLine("1 - Show products");
        Console.WriteLine("2 - Show products by price");
        Console.WriteLine("3 - Show products by amount");
        Console.WriteLine("4 - Modify product price");
        Console.WriteLine("5 - Delete product");
        Console.WriteLine("6 - Show most expensive product");
        Console.WriteLine("7 - Show cheapest product");
        Console.WriteLine("8 - Add product");
        Console.WriteLine("9 - Show product");
        Console.WriteLine("10 - Show products by category");
        Console.WriteLine("11 - Add category");
        Console.WriteLine("12 - Delete category");
        //Console.WriteLine("13 - Insert product in a category"); not implemented yet
        //Console.WriteLine("14 - Show products and category");
        Console.WriteLine("13 - Exit");
        Console.WriteLine("Make your selection");
    }
    public void ShowProduct(string id, string name, string price, string quantity, string categoryId){
        Console.WriteLine($"id: {id}, nome: {name}, prezzo: {price}, quantita: {quantity}, id_categoria: {categoryId}");
        Console.WriteLine("***********************************************************************");
    }
}