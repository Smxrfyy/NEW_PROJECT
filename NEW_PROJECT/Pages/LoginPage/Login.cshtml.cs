using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NEW_PROJECT.Models;
using NEW_PROJECT.Pages.DatabaseConnection;

namespace NEW_PROJECT.Pages.LoginPage
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }
        public string Message { get; set; }

        public string SessionID;


        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            DatabaseConnect Dbstring = new DatabaseConnect(); //creating an object from the class
            SqlConnection conn = new SqlConnection(Dbstring.DatabaseString());
            conn.Open();

            Console.WriteLine(User.UserName);
            Console.WriteLine(User.Password);


            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT FirstName, UserName, Role FROM UserTable WHERE UserName = @UName AND Password = @Pwd";

                command.Parameters.AddWithValue("@UName", User.UserName);
                command.Parameters.AddWithValue("@Pwd", User.Password);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    User.FirstName = reader.GetString(0);
                    User.UserName = reader.GetString(1);
                    User.Role = reader.GetString(2);
                }


            }

            if (!string.IsNullOrEmpty(User.FirstName))
            {
                SessionID = HttpContext.Session.Id;
                HttpContext.Session.SetString("sessionID", SessionID);
                HttpContext.Session.SetString("username", User.UserName);
                HttpContext.Session.SetString("fname", User.FirstName);
                HttpContext.Session.SetString("role", User.Role);

                if (User.Role == "User")
                {
                    return RedirectToPage("/UserPages/UserIndex");
                }
                else
                {
                    return RedirectToPage("/AdminPages/AdminIndex");
                }


            }
            else
            {
                Message = "Invalid Username and Password!";
                return Page();
            }



        }


    }
}
