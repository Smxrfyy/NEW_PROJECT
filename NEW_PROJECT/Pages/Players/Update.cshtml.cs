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
                command.CommandText = "SELECT * FROM Player WHERE Id = @ID";

                command.Parameters.AddWithValue("@ID", id);
                Console.WriteLine("The id : " + id);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PlayerRec.Id = reader.GetInt32(0);
                    PlayerRec.PlayerID = reader.GetString(1);
                    PlayerRec.PlayerName = reader.GetString(2);
                    PlayerRec.PlayerLevel = reader.GetInt32(3);
                    PlayerRec.PlayerCourse = reader.GetString(4);
                }


            }

            conn.Close();

            return Page();

        }


        public IActionResult OnPost()
        {
            string DbConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\zairu\source\repos\Week8A\DatabaseConnection1\Data\DatabaseConnection1.mdf;Integrated Security=True;Connect Timeout=30";

            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            Console.WriteLine("Player ID : " + PlayerRec.Id);
            Console.WriteLine("Player Player ID : " + PlayerRec.PlayerID);
            Console.WriteLine("Player Name : " + PlayerRec.PlayerName);
            Console.WriteLine("Player Level : " + PlayerRec.PlayerLevel);
            Console.WriteLine("Player Course : " + PlayerRec.PlayerCourse);

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "UPDATE Player SET PlayerId = @StdID, PlayerName = @StdName, PlayerLevel = @StdLevel, PlayerCourse = @StdCourse WHERE Id = @ID";

                command.Parameters.AddWithValue("@ID", PlayerRec.Id);
                command.Parameters.AddWithValue("@StdID", PlayerRec.PlayerID);
                command.Parameters.AddWithValue("@StdName", PlayerRec.PlayerName);
                command.Parameters.AddWithValue("@StdLevel", PlayerRec.PlayerLevel);
                command.Parameters.AddWithValue("@StdCourse", PlayerRec.PlayerCourse);

                command.ExecuteNonQuery();
            }

            conn.Close();

            return RedirectToPage("/Index");
        }

    }
}
