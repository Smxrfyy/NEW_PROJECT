using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NEW_PROJECT.Models;
using NEW_PROJECT.Pages.DatabaseConnection;

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
            DatabaseConnect DbCon = new DatabaseConnect();
            SqlConnection conn = new SqlConnection(DbCon.DatabaseString());
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