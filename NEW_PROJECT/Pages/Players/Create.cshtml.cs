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
                command.CommandText = @"INSERT INTO Player (PlayerID, PlayerName, PlayerLevel, PlayerCourse) VALUES (@SID, @SName, @SLevel, @SCourse)";

                command.Parameters.AddWithValue("@SID", PlayerRec.PlayerID);
                command.Parameters.AddWithValue("@SName", PlayerRec.PlayerName);
                command.Parameters.AddWithValue("@SLevel", PlayerRec.PlayerLevel);
                command.Parameters.AddWithValue("@SCourse", PlayerRec.PlayerCourse);


                Console.WriteLine(PlayerRec.PlayerID);
                Console.WriteLine(PlayerRec.PlayerName);
                Console.WriteLine(PlayerRec.PlayerLevel);
                Console.WriteLine(PlayerRec.PlayerCourse);



                command.ExecuteNonQuery();
            }



            return RedirectToPage("/Index");
        }
    }
}