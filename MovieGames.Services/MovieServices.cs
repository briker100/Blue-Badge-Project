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
    public class MovieService : IMovie
    {
        private readonly Guid _userId;

        public MovieService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateMovie(MovieCreate model)
        {
            var entity =
                new Movie()
                {
                    CustomerID = _userId,
                    MovieName = model.MovieName,
                    Description = model.Description,
                    Rating = model.Rating,
                    Producer = model.Producer,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Movies.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<MovieListItem> GetMovies()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Movies
                        .Select(
                            e =>
                                new MovieListItem
                                {
                                    MovieId = e.MovieId,
                                    Title = e.MovieName,
                                    Producer = e.Producer,
                                    Rating = e.Rating,
                                    Description = e.Description,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );
                return query.ToArray();
            }
        }
        public MovieDetails GetMovieById(int movieId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Movies
                        .Single(e => e.MovieId == movieId && e.CustomerID == _userId);
                return
                    new MovieDetails
                    {
                        MovieId = entity.MovieId,
                        Title = entity.MovieName,
                        Producer = entity.Producer,
                        Rating = entity.Rating,
                        Description = entity.Description,
                        CreatedUtc = entity.CreatedUtc,
                    };
            }
        }
        public bool UpdateNote(MovieEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Movies
                        .Single(e => e.MovieId == model.MovieId && e.CustomerID == _userId);

                entity.MovieName = model.MovieName;
                entity.Producer = model.Producer;
                entity.Rating = model.Rating;
                entity.Description = model.Description;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteMovie(int MovieId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = 
                    ctx
                        .Movies
                        .Single(e => e.MovieId == MovieId && e.CustomerID == _userId);

                ctx.Movies.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}