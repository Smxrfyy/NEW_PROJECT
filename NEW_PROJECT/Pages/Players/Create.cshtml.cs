using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NEW_PROJECT.Pages.Players
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Student StudRec { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            string DbConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\zairu\source\repos\Week8A\DatabaseConnection1\Data\DatabaseConnection1.mdf;Integrated Security=True;Connect Timeout=30";

            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"INSERT INTO Student (StudentID, StudentName, StudentLevel, StudentCourse) VALUES (@SID, @SName, @SLevel, @SCourse)";

                command.Parameters.AddWithValue("@SID", StudRec.StudentID);
                command.Parameters.AddWithValue("@SName", StudRec.StudentName);
                command.Parameters.AddWithValue("@SLevel", StudRec.StudentLevel);
                command.Parameters.AddWithValue("@SCourse", StudRec.StudentCourse);


                Console.WriteLine(StudRec.StudentID);
                Console.WriteLine(StudRec.StudentName);
                Console.WriteLine(StudRec.StudentLevel);
                Console.WriteLine(StudRec.StudentCourse);



                command.ExecuteNonQuery();
            }



            return RedirectToPage("/Index");
        }
    }
}