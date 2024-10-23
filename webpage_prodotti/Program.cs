using System.Data.SQLite;
using Spectre.Console;
class Program
{
    static void Main(string[] args)
    {
        string path = @"database.db"; // la rotta del file del database
        if (!File.Exists(path)) // se il file del database non esiste
        {
            SQLiteConnection.CreateFile(path); // crea il file del database
            SQLiteConnection connection = new SQLiteConnection($"Data Source={path};Version=3;"); // crea la connessione al database la versione 3 è un indicazione della versione del database e può esser personalizzata
            connection.Open(); // apre la connessione al database
            string sql =  @"
        CREATE TABLE IF NOT EXISTS tessere (id_tessera INTEGER PRIMARY KEY AUTOINCREMENT, data_registrazione DATE, stato BOOL);
        CREATE TABLE IF NOT EXISTS utenti (id_utente INTEGER PRIMARY KEY AUTOINCREMENT, nome TEXT, cognome TEXT, compleanno DATE, indirizzo TEXT, id_tessera INTEGER,
        FOREIGN KEY (id_tessera) REFERENCES tessera(id_tessera));
        CREATE TABLE IF NOT EXISTS autori (id_autore INTEGER PRIMARY KEY AUTOINCREMENT, nome TEXT, cognome TEXT, data_nascita DATE, luogo_nascita TEXT);
        CREATE TABLE IF NOT EXISTS generi (id_genere INTEGER PRIMARY KEY AUTOINCREMENT, nome_genere TEXT, scaffale INTEGER);
        CREATE TABLE IF NOT EXISTS libri (id_libro INTEGER PRIMARY KEY AUTOINCREMENT, titolo TEXT, anno_pubblicazione INT, codice Isbn INT, disponibilità BOOL, id_autore INTEGER, id_genere INTEGER,
        FOREIGN KEY (id_autore) REFERENCES autori(id_autore),
        FOREIGN KEY (id_genere) REFERENCES generi(id_genere));
        CREATE TABLE IF NOT EXISTS prestito (id_prestito INTEGER PRIMARY KEY, data_inizio_prestito DATE, data_fine_prestito DATE, id_utente INTEGER, id_libro INTEGER,
        FOREIGN KEY (id_libro) REFERENCES libri (id_libro),
        FOREIGN KEY (id_utente) REFERENCES utenti (id_utente));
        ";

            SQLiteCommand command = new SQLiteCommand(sql, connection); // crea il comando sql da eseguire sulla connessione al database
            command.ExecuteNonQuery(); // esegue il comando sql sulla connessione al database
            connection.Close(); // chiude la connessione al database
        }
        while (true)
        {
            Console.WriteLine("Benvenuto!");
            Console.WriteLine("Scegli un'opzione:");
            string scelta = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more choice)[/]")
                .AddChoices(new[] {
                    "Inserisci prodotto", "Visualizza prodotti", "Classifiche", "Aggiornamenti Record","Elimina prodotto","Aggiungi categoria","Elimina categoria","Esci dal programma"
                    })); 

            if (scelta == "Inserisci prodotto")
            {
                InserisciProdotto();
            }
            else if (scelta == "Visualizza prodotti")
            {
                VisualizzaProdotti();
            }
            else if (scelta == "Classifiche")
            {
                Classifiche();
            }
    
            else if (scelta == "Aggiornamenti Record")
            {
                AggiornamentiRecord();
            }

            else if (scelta == "Aggiungi categoria")
            {
                AggiungiCategoria();
            }
            else if (scelta == "Elimina categoria")
            {
                EliminaCategoria();
            }
            else if (scelta == "Elimina prodotto")
            {
                EliminaProdotto();
            }
            else if (scelta == "Esci dal programma")
            {
                break;
            }
            
        }
    }

    static void InserisciProdotto()
    {
        Console.Write("Inserisci il nome del prodotto: ");
        string nome = Console.ReadLine()!;
        Console.Write("inserisci il prezzo del prodotto: ");
        string prezzo = Console.ReadLine()!;
        Console.Write("inserisci la quantità del prodotto: ");
        string quantita = Console.ReadLine()!;
        Console.Write("inserisci codice categoria del prodotto in base agli id disponibili: ");
        VisualizzaCategorie();
        string categoria = Console.ReadLine()!;
        SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;"); // crea la connessione al database
        connection.Open(); // apre la connessione al database
        string sql = $"INSERT INTO prodotti (nome, prezzo, quantita, id_categoria) VALUES ('{nome}', {prezzo}, {quantita}, {categoria})";
        SQLiteCommand command = new SQLiteCommand(sql, connection); // crea il comando sql da eseguire sulla connessione al database
        command.ExecuteNonQuery(); // esegue il comando sql sulla connessione al database
        connection.Close(); // chiude la connessione al database
    }

    static void VisualizzaProdotti() //con filtro per BRAND
    {
        Console.WriteLine("inserisci il nome della categoria");
        string nome = Console.ReadLine()!;
        
        SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;"); // crea la connessione al database
        connection.Open(); // apre la connessione al database

        string sql = $"SELECT * FROM prodotti WHERE id_categoria = (SELECT id FROM categorie WHERE nome = '{nome}')";
        SQLiteCommand command = new SQLiteCommand(sql, connection); // crea il comando sql da eseguire sulla connessione al database
        SQLiteDataReader reader = command.ExecuteReader(); // esegue il comando sql sulla connessione al database e salva i dati in reader che è un oggetto di tipo SQLiteDataReader incaricato di leggere i dati
        while (reader.Read())
        {
            Console.WriteLine($"id prodotto: {reader["id"]}, nome: {reader["nome"]}, categoria: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantità: {reader["quantita"]}");
        }
        connection.Close(); // chiude la connessione al database
    }

    static void VisualizzaCategorie()
    {
        SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;"); // crea la connessione al database
        connection.Open(); // apre la connessione al database
        string sql = "SELECT * FROM categorie";
        SQLiteCommand command = new SQLiteCommand(sql, connection); // crea il comando sql da eseguire sulla connessione al database
        SQLiteDataReader reader = command.ExecuteReader(); // esegue il comando sql sulla connessione al database e salva i dati in reader che è un oggetto di tipo SQLiteDataReader incaricato di leggere i dati
        while (reader.Read())
        {
            Console.WriteLine($"id: {reader["id"]}, nome: {reader["nome"]}");
        }
        connection.Close(); // chiude la connessione al database
    }


    static void AggiornamentiRecord()
    {
        Console.Write("\nCosa vuoi aggiornare?");
            string scelta3 = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .PageSize(10)
                        .MoreChoicesText("[grey](Move up and down to reveal more choice)[/]")
                        .AddChoices(new[] {
                            "Nome prodotto", "Prezzo prodotto","Quantità prodotto"
                            })); 
            switch (scelta3)
            {
                case "Nome prodotto":
                AggiornaNomeProdotto();
                break;
                case "Prezzo prodotto":
                AggiornaPrezzoProdotto();
                break;
                case "Quantità prodotto":
                AggiornaQuantitàProdotto();
                break;
            }
    }
    static void Classifiche()
    {
    string scelta2 = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more choice)[/]")
                    .AddChoices(new[] {
                        "Top 3 prodotti più costosi", "Top 3 prodotti meno costosi"
                        })); 
    switch (scelta2)
    {
    case "Top 3 prodotti più costosi":
        SQLiteConnection connection2 = new SQLiteConnection($"Data Source=database.db;Version=3;"); 
        connection2.Open(); 
        string sql2 = "SELECT * FROM prodotti ORDER BY prezzo DESC LIMIT 3";
        SQLiteCommand command2 = new SQLiteCommand(sql2, connection2); 
        SQLiteDataReader reader2 = command2.ExecuteReader(); 
        while (reader2.Read())
        {
            Console.WriteLine($"id: {reader2["id"]}, nome: {reader2["nome"]}, prezzo: {reader2["prezzo"]}, quantita: {reader2["quantita"]}");
        }
        connection2.Close(); 
    break;
    case "Top 3 prodotti meno costosi":
        SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;"); 
        connection.Open(); 
        string sql = "SELECT * FROM prodotti ORDER BY prezzo ASC LIMIT 3";
        SQLiteCommand command = new SQLiteCommand(sql, connection); 
        SQLiteDataReader reader = command.ExecuteReader(); 
        while (reader.Read())
        {
            Console.WriteLine($"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}");
        }
        connection.Close();
    break;
    }
    }

static void AggiornaNomeProdotto()
{
    Console.Write("Inserisci il nome del prodotto da rinominare: ");
    string nome = Console.ReadLine()!;
    Console.Write("Digita la quantità aggiornata: ");
    string nomeNuovo = Console.ReadLine()!;
    SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
    connection.Open();
    string sql = $"UPDATE prodotti SET nome = '{nomeNuovo}' WHERE nome = '{nome}'";
    SQLiteCommand command = new SQLiteCommand(sql, connection);
    command.ExecuteNonQuery();
    connection.Close(); 
}

static void AggiornaPrezzoProdotto()
{
    Console.Write("Inserisci il nome del prodotto a cui cambiare il prezzo: ");
    string nome = Console.ReadLine()!;
    Console.Write("Digita il prezzo aggiornato: ");
    string prezzo = Console.ReadLine()!;
    SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
    connection.Open();
    string sql = $"UPDATE prodotti SET prezzo = {prezzo} WHERE nome = '{nome}'";
    SQLiteCommand command = new SQLiteCommand(sql, connection);
    command.ExecuteNonQuery();
    connection.Close(); 
}

static void AggiornaQuantitàProdotto()
{
    Console.Write("Inserisci il nome del prodotto di cui aggiornare la quantità: ");
    string nome = Console.ReadLine()!;
    Console.Write("Digita la quantità aggiornata: ");
    string quantita = Console.ReadLine()!;
    SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
    connection.Open();
    string sql = $"UPDATE prodotti SET quantita = {quantita} WHERE nome = '{nome}'";
    SQLiteCommand command = new SQLiteCommand(sql, connection);
    command.ExecuteNonQuery();
    connection.Close(); 
}


    static void EliminaProdotto()
    {
        Console.Write("inserisci il nome del prodotto");
        string nome = Console.ReadLine()!;
        SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;"); // crea la connessione al database
        connection.Open(); // apre la connessione al database
        string sql = $"DELETE FROM prodotti WHERE nome = '{nome}'";
        SQLiteCommand command = new SQLiteCommand(sql, connection); // crea il comando sql da eseguire sulla connessione al database
        command.ExecuteNonQuery(); // esegue il comando sql sulla connessione al database
        connection.Close(); // chiude la connessione al database
    }
static void AggiungiCategoria()
{
        Console.Write("Inserisci il nome della nuova categoria: ");
        string nome = Console.ReadLine()!;
        SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;"); // crea la connessione al database
        connection.Open(); // apre la connessione al database
        string sql = $"INSERT INTO categorie (nome) VALUES ('{nome}')";
        SQLiteCommand command = new SQLiteCommand(sql, connection); // crea il comando sql da eseguire sulla connessione al database
        command.ExecuteNonQuery(); // esegue il comando sql sulla connessione al database
        connection.Close(); // chiude la connessione al database
}
static void EliminaCategoria()
{
    Console.Write("Inserisci il nome della categoria da eliminare: ");
    string nome = Console.ReadLine()!;
    SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;"); 
    connection.Open();
    string sql = $"DELETE FROM categorie WHERE nome = '{nome}'";
    SQLiteCommand command = new SQLiteCommand(sql, connection);
    command.ExecuteNonQuery(); 
    connection.Close();
}

}