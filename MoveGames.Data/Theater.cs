using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveGames.Data
{
     public class Theater
    {
        public enum MovieListDrop
        {
            MovieTheaters,MovieNames,Comments
        }

        [Display(Name = "Theater was Located")]
        [Key]
        public int TheaterId { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        [Required]
        public string TheaterName { get; set; }
        [Required]
        public string TheaterLocation { get; set; }

        //TODO: Eliminate code below -- extra fluff
        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}

