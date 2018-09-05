using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieGames.Models
{
    public class MovieCreate
    {
        [Required]
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(100, ErrorMessage = "There are too many characters in this field.")]
        public string MovieName { get; set; }
        
        [MaxLength(8000)]
        public string Description { get; set; }
        [Required]
        [Range(1,5, ErrorMessage = "User much think of a number between 1 and 5")]
        public int Rating { get; set; }
        [Required]
        public string Producer { get; set; }
        
        public override string ToString() => MovieName;
    }
}