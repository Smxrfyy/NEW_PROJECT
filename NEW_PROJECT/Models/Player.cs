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

        [Display(Name = "Player Name")]
        public string PlayerName { get; set; }

        [Display(Name = "Player Level")]
        public int PlayerLevel { get; set; }

        [Display(Name = "Player Course")]
        public string PlayerCourse { get; set; }



    }
}