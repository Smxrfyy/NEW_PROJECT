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
using NEW_PROJECT.Pages.DeleteFile;

namespace NEW_PROJECT.Pages
{
    
        public class UploadFileModel : PageModel
        {
            [BindProperty(SupportsGet = true)]
            public IFormFile StdFile { get; set; }

            [BindProperty(SupportsGet = true)]
            public PlayerFile StudFileRec { get; set; }

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

                var FileToUpload = Path.Combine(_env.WebRootPath, "Files", StdFile.FileName);//this variable consists of file path
                Console.WriteLine("File Name : " + FileToUpload);



                using (var FStream = new FileStream(FileToUpload, FileMode.Create))
                {
                    StdFile.CopyTo(FStream);//copy the file into FStream variable
                }

            //DBConnection DBCon = new DBConnection();
            string DbString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Steven\source\repos\NEW_PROJECT\NEW_PROJECT\Data\Players_Database.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(DbString);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = @"INSERT PlayerFile (PlayerName, FileName) VALUES (@StdName, @FName)";
                    command.Parameters.AddWithValue("@StdName", StudFileRec.PlayerName);
                    command.Parameters.AddWithValue("@FName", StdFile.FileName);
                    Console.WriteLine("File name : " + StudFileRec.PlayerName);
                    Console.WriteLine("File name : " + StdFile.FileName);
                    command.ExecuteNonQuery();
                }



                return RedirectToPage("/index");
            }
        }
    }
