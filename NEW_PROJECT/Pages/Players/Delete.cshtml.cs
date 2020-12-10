using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NEW_PROJECT.Pages.Players
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Student StudentRec { get; set; }
        public IActionResult OnGet(int? id)
        {

            string DbConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\zairu\source\repos\Week8A\DatabaseConnection1\Data\DatabaseConnection1.mdf;Integrated Security=True;Connect Timeout=30";

            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT * FROM Student WHERE Id = @ID";
                command.Parameters.AddWithValue("@ID", id);

                SqlDataReader reader = command.ExecuteReader();
                StudentRec = new Student();
                while (reader.Read())
                {
                    StudentRec.Id = reader.GetInt32(0);
                    StudentRec.StudentID = reader.GetString(1);
                    StudentRec.StudentName = reader.GetString(2);
                    StudentRec.StudentLevel = reader.GetInt32(3);
                    StudentRec.StudentCourse = reader.GetString(4);
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

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "DELETE Student WHERE Id = @ID";
                command.Parameters.AddWithValue("@ID", StudentRec.Id);
                command.ExecuteNonQuery();
            }

            conn.Close();
            return RedirectToPage("/Index");
        }
    }
}


