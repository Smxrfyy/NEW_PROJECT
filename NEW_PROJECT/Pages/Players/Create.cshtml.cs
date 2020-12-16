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
    public class CreateModel : PageModel
    {
        [BindProperty]
       public Player PlayerRec { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            string DbConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Steven\source\repos\NEW_PROJECT\NEW_PROJECT\Data\Players_Database.mdf;Integrated Security=True";

            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"INSERT INTO PlayerTable (PlayerID, PlayerFirstName, PlayerSurname, PlayerAge) VALUES (@PID, @FName, @SName, @PAge)";

                command.Parameters.AddWithValue("@PID", PlayerRec.PlayerID);
                command.Parameters.AddWithValue("@FName", PlayerRec.PlayerFirstName);
                command.Parameters.AddWithValue("@SName", PlayerRec.PlayerSurname);
                command.Parameters.AddWithValue("@PAge", PlayerRec.PlayerAge);


                Console.WriteLine(PlayerRec.PlayerID);
                Console.WriteLine(PlayerRec.PlayerFirstName);
                Console.WriteLine(PlayerRec.PlayerSurname);
                Console.WriteLine(PlayerRec.PlayerAge);



                command.ExecuteNonQuery();
            }



            return RedirectToPage("/Index");
        }
    }
}