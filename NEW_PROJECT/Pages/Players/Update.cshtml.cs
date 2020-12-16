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
    public class UpdateModel : PageModel
    {
        [BindProperty]
        public Player PlayerRec { get; set; }

        public IActionResult OnGet(int? id)
        {
            string DbConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Steven\source\repos\NEW_PROJECT\NEW_PROJECT\Data\Players_Database.mdf;Integrated Security=True";

            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();



            PlayerRec = new Player();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT * FROM PlayerTable WHERE Id = @ID";

                command.Parameters.AddWithValue("@ID", id);
                Console.WriteLine("The id : " + id);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PlayerRec.Id = reader.GetInt32(0);
                    PlayerRec.PlayerID = reader.GetString(1);
                    PlayerRec.PlayerFirstName = reader.GetString(2);
                    PlayerRec.PlayerSurname = reader.GetString(3);
                    PlayerRec.PlayerAge = reader.GetInt32(4);
                }


            }

            conn.Close();

            return Page();

        }


        public IActionResult OnPost()
        {
            string DbConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Steven\source\repos\NEW_PROJECT\NEW_PROJECT\Data\Players_Database.mdf;Integrated Security=True";

            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            Console.WriteLine("Player ID : " + PlayerRec.Id);
            Console.WriteLine("Player Player ID : " + PlayerRec.PlayerID);
            Console.WriteLine("Player Name : " + PlayerRec.PlayerFirstName);
            Console.WriteLine("Player Age : " + PlayerRec.PlayerSurname);
            Console.WriteLine("Player Ability : " + PlayerRec.PlayerAge);

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "UPDATE PlayerTable SET PlayerId = @PID, PlayerFirstName = @PFName, PlayerSurname = @PSur, PlayerAge = @PAge WHERE Id = @ID";

                command.Parameters.AddWithValue("@ID", PlayerRec.Id);
                command.Parameters.AddWithValue("@PID", PlayerRec.PlayerID);
                command.Parameters.AddWithValue("@PFName", PlayerRec.PlayerFirstName);
                command.Parameters.AddWithValue("@PSur", PlayerRec.PlayerSurname);
                command.Parameters.AddWithValue("@PAge", PlayerRec.PlayerAge);

                command.ExecuteNonQuery();
            }

            conn.Close();

            return RedirectToPage("/Index");
        }

    }
}
