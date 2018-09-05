using MoveGames.Data;
using MovieGames.Models;
using MovieGames.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieGames.Services
{
     public class TheaterService
    {
        private readonly Guid _userId;

         public TheaterService(Guid userId)
        {
            _userId = userId;   
        }

        public bool CreateTheater(TheaterCreate model)
        {
            var entity =
                new Theater()
                {
                    OwnerId = _userId,
                    TheaterName = model.TheaterName,
                    TheaterLocation = model.TheaterLocation,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Theater.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<TheaterListItem> GetTheater()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Theater
                        .Select(
                            e =>
                                new TheaterListItem
                                {
                                    TheaterName = e.TheaterName,
                                    TheaterLocation = e.TheaterLocation,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );

                return query.ToArray();
            }
        }
        public TheaterDetail GetTheaterById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Theater
                        .Single(e => e.TheaterId == id && e.OwnerId == _userId);
                return
                    new TheaterDetail
                    {
                        TheaterId = entity.TheaterId,
                        TheaterName = entity.TheaterName,
                        TheaterLocation = entity.TheaterLocation,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }
        public bool UpdateTheater(TheaterEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Theater
                        .Single(e => e.TheaterId == model.TheaterId && e.OwnerId == _userId);

                entity.TheaterName = model.TheaterName;
                entity.TheaterLocation = model.TheaterLocation;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteTheater(int TheaterId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Theater
                        .Single(e => e.TheaterId == TheaterId && e.OwnerId == _userId);

                ctx.Theater.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
   