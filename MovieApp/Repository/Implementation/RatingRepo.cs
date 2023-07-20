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
                RatingId= addrating.RatingId,
                MovieId = addrating.MovieId,
                UserId = addrating.UserId,
                Ratings = addrating.Ratings,
               
            };
            _movieDbContext.Ratings.Add(rating);
            _movieDbContext.SaveChanges();
            return true;
        }

        public int GetRatingByUserIdAndMovieId(string UserId, int MovieId)
        {
            Rating rating = new Rating();
            rating = _movieDbContext.Ratings.FirstOrDefault(r => r.UserId == UserId && r.MovieId == MovieId);
            if (rating != null)
            {
                return rating.Ratings;
            }
            else
            {
                return 0;
            }
        }

        public List<AddRating> GetRatings(int MovieId)
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

        public bool UpdateRating(AddRating addrating)
        {
            var rating = _movieDbContext.Ratings.FirstOrDefault(x => x.UserId == addrating.UserId && x.MovieId == addrating.MovieId);
            rating.Ratings=addrating.Ratings;
            _movieDbContext.Ratings.Update(rating);
            _movieDbContext.SaveChanges();
            return true;   
        }

        public double GetAverageRating(int MovieId)
        {
            double averageRating = _movieDbContext.Ratings.Where(r => r.MovieId == MovieId).Average(r => r.Ratings);

            return averageRating;

        }
    }
}
