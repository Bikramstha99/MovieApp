using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Models.Dto.Comment;
using MovieApp.Models.Dto.Rating;
using MovieApp.Repository.Interface;

namespace MovieApp.Controllers
{
        public class RatingController : Controller
        {
            private readonly UserManager<IdentityUser> _userManager;
           
            private readonly IRating _iRating;

            public RatingController
                (
                    UserManager<IdentityUser> userManager,
                    IRating ratingService
                )
            {
                _userManager = userManager;
                
                _iRating = ratingService;
            }
            [HttpPost]
            public IActionResult SubmitRating([Bind("MovieId,Ratings")] AddRating addrating)
            {
                addrating.UserId = _userManager.GetUserId(User);
                
                _iRating.AddRating(addrating);
                return RedirectToAction("Details","Movie", new { id = addrating.MovieId });


            }
        }
    }

