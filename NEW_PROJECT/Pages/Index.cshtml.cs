using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEW_PROJECT.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public string UserName;
        public string FirstName;
        public string SessionID;
        public string Role;

        public void OnGet()
        {

            UserName = HttpContext.Session.GetString("username");
            FirstName = HttpContext.Session.GetString("fname");
            SessionID = HttpContext.Session.GetString("sessionID");
            Role = HttpContext.Session.GetString("role");

            if (!(string.IsNullOrEmpty(UserName) && string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(SessionID)))
            {
                if (Role == "Admin")
                {
                    @Response.Redirect("/AdminPages/AdminIndex");
                }
                else
                { 
                    @Response.Redirect("/UserPages/UserIndex");
                }
            }
        }
    }
}
