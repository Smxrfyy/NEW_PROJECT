using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NEW_PROJECT.Models;
using NEW_PROJECT.Pages.DatabaseConnection;

namespace NEW_PROJECT.Pages.DeleteFile
{
    public class DeleteModel : PageModel
    {



        [BindProperty]
        public PlayerFile PlayerFileRec { get; set; }

        public readonly IWebHostEnvironment _env;

        //a constructor for the class
        public DeleteModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult OnGet(int? Id)//we receive this Id from View.cs
        {
            DatabaseConnect DBCon = new DatabaseConnect();
            SqlConnection conn = new SqlConnection(DBCon.DatabaseString());
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM FileTable WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", Id);

                var reader = command.ExecuteReader();

                PlayerFileRec = new PlayerFile();
                while (reader.Read())
                {
                    PlayerFileRec.Id = reader.GetInt32(0);
                    PlayerFileRec.PlayerName = reader.GetString(1); //to display on the html page
                    PlayerFileRec.FileName = reader.GetString(2); //to display on the html page
                }

                Console.WriteLine("File name : " + PlayerFileRec.FileName);


            }

            return Page();
        }


        public IActionResult OnPost()
        {

            deletePicture(PlayerFileRec.Id, PlayerFileRec.FileName);
            return RedirectToPage("/ViewFile/ViewFile");
        }



        public void deletePicture(int Id, string FileName)
        {
            Console.WriteLine("Record Id : " + Id);
            Console.WriteLine("File Name : " + FileName);

            DatabaseConnect DBCon = new DatabaseConnect();
            SqlConnection conn = new SqlConnection(DBCon.DatabaseString());
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"DELETE FROM FileTable WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", Id);

                command.ExecuteNonQuery();
            }
            Console.WriteLine(FileName);
            string RetrieveImage = Path.Combine(_env.WebRootPath, "Files", FileName);
            System.IO.File.Delete(RetrieveImage);
            Console.WriteLine("File has been deleted");


        }
    }
}
