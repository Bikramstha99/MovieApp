using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Models.Domain;
using MovieApp.Models.Dto.Comment;
using MovieApp.Models.Dto.Rating;
using MovieApp.Repository.Interface;

namespace MovieApp.Repository.Implementation
{
    public class RatingRepo : IRating
    {
        private readonly MovieDbContext _movieDbContext;

        public RatingRepo(MovieDbContext movieDbContext)
        {
            _movieDbContext = movieDbContext;
        }

        public bool AddRating(AddRating addrating)
        {
            var rating = new Rating()
            {
                MovieId = addrating.MovieId,
                UserId = addrating.UserId,
                Ratings = addrating.Ratings,
               
            };
            _movieDbContext.Ratings.Add(rating);
            _movieDbContext.SaveChanges();
            return true;
        }

        public List<AddRating> Ratings(int MovieId)
        {
            var data = _movieDbContext.Ratings.Include(e => e.IdentityUser).Where(c => c.MovieId == MovieId).Select(d => new AddRating
            {
                MovieId = d.MovieId,
                IdentityUser = d.IdentityUser,
                UserId = d.UserId,
                Ratings=d.Ratings,
                RatingId=d.RatingId
            }).ToList();
            return data;
        }
    }
}
