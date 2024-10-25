﻿using System.Data.SQLite;
using System.Runtime.CompilerServices;
// comando per installare il pacchetto System.Data.SQLite
// dotnet add package System.Data.SQLite
// ignore

class Program 
{
    static void Main(string[] args)
    {
        MyView myView = new MyView();
        Controller controller = new Controller(myView);
        string path = @"database.db"; // il file deve essere nella stessa cartella del programma
        if (!File.Exists(path))
        {
            SQLiteConnection.CreateFile(path);
            SQLiteConnection connection = new SQLiteConnection($"Data Source={path};Version=3;");
            connection.Open();

            // Add the new table `clienti` with the field `codice_cliente`
            string sql = @"
                            CREATE TABLE categorie (id INTEGER PRIMARY KEY AUTOINCREMENT, nome TEXT UNIQUE);
                            CREATE TABLE prodotti (id INTEGER PRIMARY KEY AUTOINCREMENT, nome TEXT UNIQUE, 
                            prezzo REAL, quantita INTEGER CHECK (quantita >= 0), id_categoria INTEGER, 
                            FOREIGN KEY (id_categoria) REFERENCES categorie(id));
                            CREATE TABLE clienti (id INTEGER PRIMARY KEY AUTOINCREMENT, nome TEXT UNIQUE, codice_cliente TEXT UNIQUE);
                            INSERT INTO categorie (nome) VALUES ('c1');
                            INSERT INTO categorie (nome) VALUES ('c2');
                            INSERT INTO categorie (nome) VALUES ('c3');
                            INSERT INTO prodotti (nome, prezzo, quantita, id_categoria) VALUES ('p1', 1, 10, 1);
                            INSERT INTO prodotti (nome, prezzo, quantita, id_categoria) VALUES ('p2', 2, 20, 2);";

            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
        controller.MainMenu();
    }
}
