using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieGames.Models
{
    public class MovieEdit
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public string Producer { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
    }
}
