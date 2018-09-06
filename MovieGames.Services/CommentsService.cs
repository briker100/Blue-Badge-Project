using MoveGames.Data;
using MovieGames.Contracts;
using MovieGames.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieGames.Services
{
    public class CommentService : IComment
    {
        private readonly int _CommentId;
        private readonly Guid _userId;
        private readonly string _movieName;

        public CommentService() { }

        public CommentService(Guid userId)
        {
            _userId = userId;
        }

        public CommentService(Guid userId, int CommentId)
        {
            _userId = userId;
            _CommentId = CommentId;
        }

        public bool CreateComment(CommentCreate model)
        {
            var entity =
               new Comment
               {    
                   UserId = _userId,
                   CommentId = _CommentId,
                   MovieName = model.MovieName,
                   CommentText = model.CommentText,
                   CreatedDate = DateTimeOffset.UtcNow,
                   UserName = model.UserName, 
               };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Comments.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public CommentDetail GetSingleCommentById(int commentId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var comment =
                    ctx
                        .Comments
                        .SingleOrDefault(r => r.CommentId == commentId);

                return
                    new CommentDetail()
                    {
                        MovieName = comment.MovieName,
                        CommentText = comment.CommentText,
                        CommentId = comment.CommentId,
                        UserName = comment.UserName,
                    };
            }
        }

        public ICollection<CommentListItem> GetAllComments()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var comments =
                    ctx
                        .Comments
                        .Select(
                            e => new CommentListItem()
                            {
                                
                                UserId = e.UserId,
                                CommentId= e.CommentId,
                                UserName= e.UserName,
                                MovieName = e.MovieName,
                                CommentText = e.CommentText,
                                CreatedDate = e.CreatedDate,
                                
                            });

                return comments.ToList();
            }
        }

        public ICollection<CommentListItem> GetCommentByMovieName(string MovieName)
        {       
            using (var ctx = new ApplicationDbContext())    
            {
                var comments =
                    ctx
                        .Comments
                        .Where(c => c.MovieName == MovieName)
                        .Select(
                            e => new CommentListItem()
                            {
                                CommentId = e.CommentId,
                                MovieName =e.MovieName,
                                UserId = e.UserId,
                                CommentText = e.CommentText,
                                CreatedDate = e.CreatedDate

                            });

                var CommentList = comments.ToList();

                foreach (var comment in CommentList)
                {
                    comment.UserName = GetNameFromUserId(comment.UserId);
                }

                return CommentList;
            }
        }
        public bool DeleteComment(int CommentId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =    
                    ctx
                        .Comments       
                        .Single(e => e.CommentId == CommentId);

                ctx.Comments.Remove(entity);  

                return ctx.SaveChanges() == 1;
            }
        }

        private string GetNameFromUserId(Guid userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var user =
                    ctx
                        .Users
                        .SingleOrDefault(u => u.Id == userId.ToString());

                return user.UserName;
            }
        }
    }
}