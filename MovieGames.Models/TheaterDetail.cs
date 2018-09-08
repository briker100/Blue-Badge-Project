using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieGames.Models
{
    public class TheaterDetail
    {
        [Key]
        public int TheaterId { get; set; }

        public string TheaterName { get; set; }
        public string TheaterLocation { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }
        public override string ToString() => $"[{TheaterId}] {TheaterLocation}";
    }
}
