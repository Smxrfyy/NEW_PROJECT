using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NEW_PROJECT.Models;
using NEW_PROJECT.Pages.DatabaseConnection;
using NEW_PROJECT.Pages.DeleteFile;

namespace NEW_PROJECT.Pages
{
    
        public class UploadFileModel : PageModel
        {
            [BindProperty(SupportsGet = true)]
            public IFormFile PlayerFile { get; set; }

            [BindProperty(SupportsGet = true)]
            public PlayerFile PlayerFileRec { get; set; }

            public readonly IWebHostEnvironment _env;

            //a constructor for the class
            public UploadFileModel(IWebHostEnvironment env)
            {
                _env = env;
            }

            public void OnGet()
            {

            }

            public IActionResult OnPost()
            {

                var FileToUpload = Path.Combine(_env.WebRootPath, "Files", PlayerFile.FileName);//this variable consists of file path
                Console.WriteLine("File Name : " + FileToUpload);



                using (var FStream = new FileStream(FileToUpload, FileMode.Create))
                {
                    PlayerFile.CopyTo(FStream);//copy the file into FStream variable
                }

            DatabaseConnect DbCon = new DatabaseConnect();
            SqlConnection conn = new SqlConnection(DbCon.DatabaseString());
            conn.Open();

            using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = @"INSERT FileTable (PlayerName, FileName) VALUES (@PlayerName, @FName)";
                    command.Parameters.AddWithValue("@PlayerName", PlayerFileRec.PlayerName);
                    command.Parameters.AddWithValue("@FName", PlayerFile.FileName);
                    Console.WriteLine("File name : " + PlayerFileRec.PlayerName);
                    Console.WriteLine("File name : " + PlayerFile.FileName);
                    command.ExecuteNonQuery();
                }



                return RedirectToPage("/index");
            }
        }
    }
