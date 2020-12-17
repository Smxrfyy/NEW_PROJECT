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
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Player PlayerRec { get; set; }
        public IActionResult OnGet(int? id)
        {
            DatabaseConnect DbCon = new DatabaseConnect();
            SqlConnection conn = new SqlConnection(DbCon.DatabaseString());
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT * FROM PlayerTable WHERE Id = @ID";
                command.Parameters.AddWithValue("@ID", id);

                SqlDataReader reader = command.ExecuteReader();
                PlayerRec = new Player();
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
            DatabaseConnect DbCon = new DatabaseConnect();
            SqlConnection conn = new SqlConnection(DbCon.DatabaseString());
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "DELETE PlayerTable WHERE Id = @ID";
                command.Parameters.AddWithValue("@ID", PlayerRec.Id);
                command.ExecuteNonQuery();
            }

            conn.Close();
            return RedirectToPage("/Index");
        }
    }
}


