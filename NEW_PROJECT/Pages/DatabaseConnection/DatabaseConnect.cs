using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEW_PROJECT.Pages.DatabaseConnection
{
    public class DatabaseConnect
    {
        public string DatabaseString()
        {
            string DbString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Ryan\source\repos\Smxrfyy\NEW_PROJECT\Players_Database.mdf;Integrated Security=True;Connect Timeout=30";
            return DbString;
        }
    }
}
