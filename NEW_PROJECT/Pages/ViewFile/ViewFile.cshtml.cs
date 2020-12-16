using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NEW_PROJECT.Models;
using NEW_PROJECT.Pages.DeleteFile;

namespace NEW_PROJECT.Pages
{
    public class ViewModel : PageModel
    {
        public List<PlayerFile> FileRec { get; set; }
        public void OnGet()
        {
            //DBConnection DBCon = new DBConnection();
            string DbString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Steven\source\repos\NEW_PROJECT\NEW_PROJECT\Data\Players_Database.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(DbString);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM PlayerFile";

                var reader = command.ExecuteReader();

                FileRec = new List<PlayerFile>();

                while (reader.Read())
                {
                    PlayerFile rec = new PlayerFile();
                    rec.Id = reader.GetInt32(0); // we need this to send the Id to Delete page for another enquiry
                    rec.PlayerName = reader.GetString(1);
                    rec.FileName = reader.GetString(2);
                    FileRec.Add(rec);
                }
            }

        }
    }
}
