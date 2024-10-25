﻿using System.Data.SQLite;
using System.Runtime.CompilerServices;
// comando per installare il pacchetto System.Data.SQLite
// dotnet add package System.Data.SQLite
// ignore

class Program 
{
    static string path = @"database.db";
    static void Main(string[] args)
    {
        MyView myView = new MyView();
        Database database = new Database();
        Controller controller = new Controller(myView, database);
        database.CreateDatabase(path);
        controller.MainMenu();
    }
}
