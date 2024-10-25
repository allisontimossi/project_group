using System.Data.SQLite;
using System.Runtime.CompilerServices;

public class Controller
    {
        private MyView _myView;
        private Database _database;
        public Controller(MyView myView, Database database){
            _myView = myView;
            _database = database;

        }
        public void MainMenu(){
            bool exit = true;
            while(exit)
            {
                _myView.ShowMainMenu();
                string seselection = Console.ReadLine()!;
                switch (seselection)
                {
                    case "1":
                        VisualizzaProdotti();
                        break;
                    case "2":
                        VisualizzaProdottiOrdinatiPerPrezzo();
                        break;
                    case "3":
                        VisualizzaProdottiOrdinatiPerQuantita();
                        break;
                    case "4":
                        ModificaPrezzoProdotto();
                        break;
                    case "5":
                        EliminaProdotto();
                        break;
                    case "6":
                        VisualizzaProdottoPiuCostoso();
                        break;
                    case "7":
                        VisualizzaProdottoMenoCostoso();
                        break;
                    case "8":
                        InserisciProdotto();
                        break;
                    case "9":
                        VisualizzaProdotto();
                        break;
                    case "10":
                        VisualizzaProdottiCategoria();
                        break;
                    case "11":
                        InserisciCategoria();
                        break;
                    case "12":
                        EliminaCategoria();
                        break;
                    case "13":
                        _database.CloseConnection();
                        exit = false;
                        break;
                }
            }
            return;
        }

    private void VisualizzaProdotti()
    {
        using var reader = _database.GetProducts();
        while (reader.Read())
        {
            _myView.ShowProduct(reader["id"].ToString(), reader["nome"].ToString(), reader["prezzo"].ToString(), reader["quantita"].ToString(), reader["id_categoria"].ToString());
        }
        _database.CloseConnection();
        
    }

    private void VisualizzaProdottiOrdinatiPerPrezzo()
    {
        using var reader = _database.GetProductsOrderedByPrice();
        while (reader.Read())
        {
            _myView.ShowProduct(reader["id"].ToString(), reader["nome"].ToString(), reader["prezzo"].ToString(), reader["quantita"].ToString(), reader["id_categoria"].ToString());
        }
        _database.CloseConnection();
    }

    private void VisualizzaProdottiOrdinatiPerQuantita()
    {
        using var reader = _database.GetProductsOrderedByQuantity();
        while (reader.Read())
        {
            _myView.ShowProduct(reader["id"].ToString(), reader["nome"].ToString(), reader["prezzo"].ToString(), reader["quantita"].ToString(), reader["id_categoria"].ToString());
        }
        _database.CloseConnection();
    }

    private void ModificaPrezzoProdotto()
    {
        Console.WriteLine("inserisci il nome del prodotto");
        string nome = Console.ReadLine()!;
        Console.WriteLine("inserisci il nuovo prezzo");
        string prezzo = Console.ReadLine()!;
        _database.UpdateProductPrice(nome, prezzo);
        _database.CloseConnection();
    }

    private void EliminaProdotto()
    {
        Console.WriteLine("inserisci il nome del prodotto");
        string nome = Console.ReadLine()!;
        _database.DeleteProduct(nome);
        _database.CloseConnection();        
    }

    private void VisualizzaProdottoPiuCostoso()
    {
        using var reader = _database.GetMostExpensiveProduct();
        while (reader.Read())
        {
            _myView.ShowProduct(reader["id"].ToString(), reader["nome"].ToString(), reader["prezzo"].ToString(), reader["quantita"].ToString(), reader["id_categoria"].ToString());
        }
        _database.CloseConnection();
    }

    private void VisualizzaProdottoMenoCostoso()
    {
        using var reader = _database.GetLeastExpensiveProduct();
        while (reader.Read())
        {
            _myView.ShowProduct(reader["id"].ToString(), reader["nome"].ToString(), reader["prezzo"].ToString(), reader["quantita"].ToString(), reader["id_categoria"].ToString());
        }
        _database.CloseConnection();
    }

    private void InserisciProdotto()
    {
        Console.WriteLine("inserisci il nome del prodotto");
        string nome = Console.ReadLine()!;
        Console.WriteLine("inserisci il prezzo del prodotto");
        string prezzo = Console.ReadLine()!;
        Console.WriteLine("inserisci la quantità del prodotto");
        string quantita = Console.ReadLine()!;
        Console.WriteLine("inserisci l'id della categoria del prodotto");
        string id_categoria = Console.ReadLine()!;
        _database.AddProduct(nome, prezzo, quantita, id_categoria);
        _database.CloseConnection();
    }

    private void VisualizzaProdotto()
    {
        Console.WriteLine("inserisci il nome del prodotto");
        string nome = Console.ReadLine()!;
        using var reader = _database.GetProductByName(nome);
        while (reader.Read())
        {
            _myView.ShowProduct(reader["id"].ToString(), reader["nome"].ToString(), reader["prezzo"].ToString(), reader["quantita"].ToString(), reader["id_categoria"].ToString());
        }
        _database.CloseConnection();        
    }

    private void VisualizzaProdottiCategoria()
    {
        Console.WriteLine("inserisci l'id della categoria");
        string id_categoria = Console.ReadLine()!;
        using var reader = _database.GetProductsByCategory(id_categoria);
        while (reader.Read())
        {
            _myView.ShowProduct(reader["id"].ToString(), reader["nome"].ToString(), reader["prezzo"].ToString(), reader["quantita"].ToString(), reader["id_categoria"].ToString());
        }
        _database.CloseConnection();        
    }

    private void InserisciCategoria()
    {
        Console.WriteLine("inserisci il nome della categoria");
        string nome = Console.ReadLine()!;
        _database.AddCategory(nome);
        _database.CloseConnection();

    }

    private void EliminaCategoria()
    {
        Console.WriteLine("inserisci il nome della categoria");
        string nome = Console.ReadLine()!;
        _database.DeleteCategory(nome);
        _database.CloseConnection();        
    }
}