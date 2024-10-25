using System.Data.SQLite;
using System.Runtime.CompilerServices;

public class Controller
    {
        private MyView _myView;
        public Controller(MyView myView){
            _myView = myView;
        }
        public void MainMenu(){
            bool exit = true;
            while (exit)
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
                        exit = false;
                        break;
                }
                return;
            }
        }

        public void VisualizzaProdotti()
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;"); // crea la connessione di nuovo perché è stata chiusa alla fine del while in modo da poter visualizzare i dati aggiornati
            connection.Open();
            string sql = "SELECT * FROM prodotti"; // crea il comando sql che seleziona tutti i dati dalla tabella prodotti
            SQLiteCommand command = new SQLiteCommand(sql, connection); // crea il comando sql da eseguire sulla connessione al database
            SQLiteDataReader reader = command.ExecuteReader(); // esegue il comando sql sulla connessione al database e salva i dati in reader che è un oggetto di tipo SQLiteDataReader incaricato di leggere i dati
            while (reader.Read())
            {
                _myView.ShowProduct(reader["id"].ToString(), reader["nome"].ToString(), reader["prezzo"].ToString(), reader["quantita"].ToString(), reader["id_categoria"].ToString());
                //Console.WriteLine($"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}, id_categoria: {reader["id_categoria"]}");
            }
            connection.Close(); // chiude la connessione al database se non è già chiusa
        }

        public void VisualizzaProdottiAdvanced()
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connection.Open();

            // Modifica la query SQL per includere una join con la tabella categorie
            string sql = @"
                SELECT prodotti.id, prodotti.nome, prodotti.prezzo, prodotti.quantita, categorie.nome AS nome_categoria 
                FROM prodotti
                JOIN categorie ON prodotti.id_categoria = categorie.id";

            SQLiteCommand command = new SQLiteCommand(sql, connection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                _myView.ShowProduct(reader["id"].ToString(), reader["nome"].ToString(), reader["prezzo"].ToString(), reader["quantita"].ToString(), reader["nome_categoria"].ToString());
                //Console.WriteLine($"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}, categoria: {reader["nome_categoria"]}");
            }
            connection.Close();
        }

        public void VisualizzaProdottiOrdinatiPerPrezzo()
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connection.Open();
            string sql = "SELECT * FROM prodotti ORDER BY prezzo"; // crea il comando sql che seleziona tutti i dati dalla tabella prodotti ordinati per prezzo
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                _myView.ShowProduct(reader["id"].ToString(), reader["nome"].ToString(), reader["prezzo"].ToString(), reader["quantita"].ToString(), reader["id_categoria"].ToString());
                //Console.WriteLine($"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}, id_categoria: {reader["id_categoria"]}");
            }
            connection.Close();
        }

        public void VisualizzaProdottiOrdinatiPerQuantita()
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connection.Open();
            string sql = "SELECT * FROM prodotti ORDER BY quantita"; // crea il comando sql che seleziona tutti i dati dalla tabella prodotti ordinati per quantita
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                _myView.ShowProduct(reader["id"].ToString(), reader["nome"].ToString(), reader["prezzo"].ToString(), reader["quantita"].ToString(), reader["id_categoria"].ToString());
                //Console.WriteLine($"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}, id_categoria: {reader["id_categoria"]}");
            }
            connection.Close();
        }

        static void ModificaPrezzoProdotto()
        {
            Console.WriteLine("inserisci il nome del prodotto"); // chiede il nome del prodotto da modificare
            string nome = Console.ReadLine()!; // legge il nome del prodotto da modificare
            Console.WriteLine("inserisci il nuovo prezzo"); // chiede il nuovo prezzo del prodotto da modificare
            string prezzo = Console.ReadLine()!; // legge il nuovo prezzo del prodotto da modificare
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connection.Open();
            string sql = $"UPDATE prodotti SET prezzo = {prezzo} WHERE nome = '{nome}'"; // crea il comando sql che modifica il prezzo del prodotto con nome uguale a quello inserito
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery(); // esegue il comando sql sulla connessione al database ExecuteNonQuery() viene utilizzato per eseguire comandi che non restituiscono dati, ad esempio i comandi INSERT, UPDATE, DELETE
            connection.Close();
        }

        static void EliminaProdotto()
        {
            Console.WriteLine("inserisci il nome del prodotto");
            string nome = Console.ReadLine()!;
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connection.Open();
            string sql = $"DELETE FROM prodotti WHERE nome = '{nome}'"; // crea il comando sql che elimina il prodotto con nome uguale a quello inserito
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void VisualizzaProdottoPiuCostoso()
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connection.Open();
            string sql = "SELECT * FROM prodotti ORDER BY prezzo DESC LIMIT 1"; // crea il comando sql che seleziona tutti i dati dalla tabella prodotti ordinati per prezzo in modo decrescente e ne prende solo il primo
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                _myView.ShowProduct(reader["id"].ToString(), reader["nome"].ToString(), reader["prezzo"].ToString(), reader["quantita"].ToString(), reader["id_categoria"].ToString());
                //Console.WriteLine($"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}, id_categoria: {reader["id_categoria"]}");
            }
            connection.Close();
        }

        public void VisualizzaProdottoMenoCostoso()
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connection.Open();
            string sql = "SELECT * FROM prodotti ORDER BY prezzo ASC LIMIT 1"; // crea il comando sql che seleziona tutti i dati dalla tabella prodotti ordinati per prezzo in modo crescente e ne prende solo il primo
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                _myView.ShowProduct(reader["id"].ToString(), reader["nome"].ToString(), reader["prezzo"].ToString(), reader["quantita"].ToString(), reader["id_categoria"].ToString());
                //Console.WriteLine($"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}, id_categoria: {reader["id_categoria"]}");
            }
            connection.Close();
        }

        static void InserisciProdotto()
        {
            Console.WriteLine("inserisci il nome del prodotto");
            string nome = Console.ReadLine()!;
            Console.WriteLine("inserisci il prezzo del prodotto");
            string prezzo = Console.ReadLine()!;
            Console.WriteLine("inserisci la quantità del prodotto");
            string quantita = Console.ReadLine()!;
            Console.WriteLine("inserisci l'id della categoria del prodotto");
            string id_categoria = Console.ReadLine()!;
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connection.Open();
            string sql = $"INSERT INTO prodotti (nome, prezzo, quantita, id_categoria) VALUES ('{nome}', {prezzo}, {quantita}, {id_categoria})"; // crea il comando sql che inserisce un prodotto
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        // inserimento di prodotto chiamando prima la categoria e poi il prodotto in modo da avere in inserimento il nome della categoria invece dell id
        static void InserisciProdottoCategoria()
        {
            //visualizza le categorie
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connection.Open();
            string sql = "SELECT * FROM categorie";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"id: {reader["id"]}, nome: {reader["nome"]}");
            }
            connection.Close();
            //seleziona categoria
            Console.WriteLine("inserisci l'id della categoria");
            string id_categoria = Console.ReadLine()!;
            //inserisci prodotto
            Console.WriteLine("inserisci il nome del prodotto");
            string nome = Console.ReadLine()!;
            Console.WriteLine("inserisci il prezzo del prodotto");
            string prezzo = Console.ReadLine()!;
            Console.WriteLine("inserisci la quantità del prodotto");
            string quantita = Console.ReadLine()!;
            SQLiteConnection connectionins = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connectionins.Open();
            string sqlins = $"INSERT INTO prodotti (nome, prezzo, quantita, id_categoria) VALUES ('{nome}', {prezzo}, {quantita}, {id_categoria})"; // crea il comando sql che inserisce un prodotto
            SQLiteCommand commandins = new SQLiteCommand(sqlins, connectionins);
            commandins.ExecuteNonQuery();
            connectionins.Close();
        }

        public void VisualizzaProdotto()
        {
            Console.WriteLine("inserisci il nome del prodotto");
            string nome = Console.ReadLine()!;
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connection.Open();
            string sql = $"SELECT * FROM prodotti WHERE nome = '{nome}'"; // crea il comando sql che seleziona tutti i dati dalla tabella prodotti con nome uguale a quello inserito
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                _myView.ShowProduct(reader["id"].ToString(), reader["nome"].ToString(), reader["prezzo"].ToString(), reader["quantita"].ToString(), reader["id_categoria"].ToString());
                //Console.WriteLine($"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}, id_categoria: {reader["id_categoria"]}");
            }
            connection.Close();
        }

        public void VisualizzaProdottiCategoria()
        {
            Console.WriteLine("inserisci l'id della categoria");
            string id_categoria = Console.ReadLine()!;
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connection.Open();
            string sql = $"SELECT * FROM prodotti WHERE id_categoria = {id_categoria}"; // crea il comando sql che seleziona tutti i dati dalla tabella prodotti con id_categoria uguale a quello inserito
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                _myView.ShowProduct(reader["id"].ToString(), reader["nome"].ToString(), reader["prezzo"].ToString(), reader["quantita"].ToString(), reader["id_categoria"].ToString());
                //Console.WriteLine($"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}, id_categoria: {reader["id_categoria"]}");
            }
            connection.Close();
        }

        static void InserisciCategoria()
        {
            Console.WriteLine("inserisci il nome della categoria");
            string nome = Console.ReadLine()!;
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connection.Open();
            string sql = $"INSERT INTO categorie (nome) VALUES ('{nome}')"; // crea il comando sql che inserisce una categoria
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        static void EliminaCategoria()
        {
            Console.WriteLine("inserisci il nome della categoria");
            string nome = Console.ReadLine()!;
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connection.Open();
            string sql = $"DELETE FROM categorie WHERE nome = '{nome}'"; // crea il comando sql che elimina la categoria con nome uguale a quello inserito
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
