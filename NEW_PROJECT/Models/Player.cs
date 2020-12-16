using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NEW_PROJECT.Models
{
    public class Player
    {
        public int Id { get; set; }

        [Display(Name = "Player ID")]
        public string PlayerID { get; set; }

        [Display(Name = "Player First Name")]
        public string PlayerFirstName { get; set; }

        [Display(Name = "Player Surname")]
        public string PlayerSurname { get; set; }

        [Display(Name = "Player Age")]
        public int PlayerAge { get; set; }
    
    }
}