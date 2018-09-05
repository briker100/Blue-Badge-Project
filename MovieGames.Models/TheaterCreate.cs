using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieGames.Models
{
     public class TheaterCreate
    {
        [Required]
        public string TheaterName { get; set; }
        public string TheaterLocation { get; set; } 

         public override string ToString() => TheaterName;

    }
}
