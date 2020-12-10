using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEW_PROJECT.Pages
{
    public class DatabaseConnect
    {
        public string DatabaseString()
        {
            string DbString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Steven\source\repos\NEW_PROJECT\NEW_PROJECT\Data\Players_Database.mdf;Integrated Security=True";
            return DbString;
        }
    }
}
