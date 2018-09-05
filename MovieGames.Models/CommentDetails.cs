using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieGames.Models
{
    public class CommentDetail
    {
        public int CommentId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string MovieName { get; set; }   
        public string CommentText { get; set; } 
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
    }
}