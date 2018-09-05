using MovieGames.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieGames.Contracts
{
    public interface IComment
    {
        bool CreateComment(CommentCreate model);
        CommentDetail GetSingleCommentById(int commentId);
        ICollection<CommentListItem> GetAllComments();
        ICollection<CommentListItem> GetCommentByMovieName(string MovieName);
        bool DeleteComment(int lessonId);
    }
}
