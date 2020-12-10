using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NEW_PROJECT.Models
{
    public class PlayerFile
    {

        public int Id { get; set; }

        [Display(Name = "Player Name")]
        public string PlayerName { get; set; }

        public string FileName { get; set; }
    }
}
