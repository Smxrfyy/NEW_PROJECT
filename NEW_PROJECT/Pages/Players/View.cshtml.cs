using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NEW_PROJECT.Models;

namespace NEW_PROJECT.Pages.Players
{
    public class ViewModel : PageModel
    {
        public List<Player> PlayerRec { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Level { get; set; }

        public List<int> LevelItems { get; set; } = new List<int> { 4, 5, 6, 7 };

        public void OnGet()
        {

            string DbConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Steven\source\repos\NEW_PROJECT\NEW_PROJECT\Data\Players_Database.mdf;Integrated Security=True";

            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM PlayerTable";

                if (!string.IsNullOrEmpty(Level) && (Level != "All"))
                {
                    command.CommandText += " WHERE PlayerLevel = @PlayerLevel";
                    command.Parameters.AddWithValue("@PlayerLevel", Convert.ToInt32(Level));
                }

                SqlDataReader reader = command.ExecuteReader(); //SqlDataReader is used to read record from a table

                PlayerRec = new List<Player>(); //this object of list is created to populate all records from the table

                while (reader.Read())
                {
                    Player record = new Player(); //a local var to hold a record temporarily
                    record.Id = reader.GetInt32(0); //getting the first field from the table
                    record.PlayerID = reader.GetString(1); //getting the second field from the table
                    record.PlayerFirstName = reader.GetString(2); //getting the third field from the table
                    record.PlayerSurname = reader.GetString(3);
                    record.PlayerAge = reader.GetInt32(4);

                    PlayerRec.Add(record); //adding the single record into the list
                }

                // Call Close when done reading.
                reader.Close();
            }


        }
    }
}
