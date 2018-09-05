using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveGames.Data
{
     public class Comment
    {
        [Display(Name = "Comment was Created")]
        [Key]
        public int CommentId { get; set; }

        [Required]
        public string MovieName { get; set; }

        [Required]
        public Guid UserId { get; set; }
            
        [Required]
        public string UserName { get; set; }

        [Required]
        public string CommentText { get; set; }

        [Required]
        public DateTimeOffset CreatedDate { get; set; }
    }
}
