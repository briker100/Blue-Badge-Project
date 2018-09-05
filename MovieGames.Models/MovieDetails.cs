﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieGames.Models
{
        public class MovieDetails
    {
        public int MovieId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int Rating { get; set; }
        public string Producer { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Modified")]
        public DateTimeOffset ModifiedUtc { get; set; }
        public override string ToString() => $"[{MovieId}] {Title}";

    }
}
