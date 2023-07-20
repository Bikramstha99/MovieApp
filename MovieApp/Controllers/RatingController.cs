using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MovieApp.Models.Domain;
using MovieApp.Models.Dto.Comment;
using MovieApp.Models.Dto.Rating;
using MovieApp.Repository.Interface;

namespace MovieApp.Controllers
{
    public class RatingController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly IRating _iRating;
        private readonly IMovieRepository _iMovieRepo;

        public RatingController
            (
                UserManager<IdentityUser> userManager,
                IRating ratingService,
                IMovieRepository iMovieRepo
            )
        {
            _userManager = userManager;

            _iRating = ratingService;
            _iMovieRepo = iMovieRepo;
        }
        [HttpPost]
        public IActionResult SubmitRating([Bind("MovieId,Ratings")] AddRating addrating)
        {
            addrating.UserId = _userManager.GetUserId(User);
            int rate = _iRating.GetRatingByUserIdAndMovieId(addrating.UserId, addrating.MovieId);
            if (rate == 0)
            {
                _iRating.AddRating(addrating);
            }
            else
            {
                _iRating.UpdateRating(addrating);
            }
            double averageRating = _iRating.GetAverageRating(addrating.MovieId);
            var movie = _iMovieRepo.GetByID(addrating.MovieId);
            if (movie != null)
            {
                movie.AverageRating = averageRating;
                _iMovieRepo.UpdateMovies(movie);
            }


            return RedirectToAction("Details", "Movie", new { id = addrating.MovieId });

        }
    }
}


