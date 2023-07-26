using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Models.Domain;
using MovieApp.Models.Dto.Comment;
using MovieApp.Models.Dto.Movie;
using MovieApp.Models.Dto.Pager;
using MovieApp.Repository.Implementation;
using MovieApp.Repository.Interface;
using System.Drawing.Printing;
using System.Xml.Linq;

namespace MovieApp.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieRepository _IMovie;
        private readonly IWebHostEnvironment _iwebhostenvironment;
        private readonly ICommentRepository _iComment;
        private readonly IRatingRepository _iRating;

        public MovieController(
            IMovieRepository imovie, 
            IWebHostEnvironment iwebhostenvironment,
            ICommentRepository iComment,
            IRatingRepository iRating)
        {
            _IMovie = imovie;
            _iwebhostenvironment = iwebhostenvironment;
            _iComment = iComment;
            _iRating = iRating;
        }
        [HttpGet]
        public IActionResult Index(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 3;
            var movies = _IMovie.GetAllMovies();

            int totalMovies = movies.Count;
            int totalPages = (int)Math.Ceiling(totalMovies / (double)pageSize);

            //starting index of each page
            int startIndex = (pageNumber - 1) * pageSize;

            //skip skips first specified number of data and take takes the specified number of data
            var pagedMovies = movies.Skip(startIndex).Take(pageSize).ToList();
            var pager= new PagerVM
            {
                Movies = pagedMovies,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalMovies = totalMovies,
                TotalPages = totalPages
            };
            return View(pager);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AddMovie addmovie)
        {
            //var image = Request.Form.Files.FirstOrDefault();
            //var fileName = Guid.NewGuid().ToString();
            //var path = $@"images\";
            //var wwwRootPath = _iwebhostenvironment.WebRootPath;
            //var uploads = Path.Combine(wwwRootPath, path);
            //var extension = Path.GetExtension(image.FileName);
            //using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            //{
            //    image.CopyTo(fileStreams);
            //}
            //addmovie.MoviePhoto = $"\\images\\{fileName}" + extension;
            _IMovie.AddMovies(addmovie);

            return RedirectToAction("Index");

        }
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var movie = _IMovie.GetByID(Id);
            return View(movie);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(UpdateMovie updatemovie)
        {
            var images = Request.Form.Files.FirstOrDefault();
            var fileName = Guid.NewGuid().ToString();
            var path = $@"updateimages\";
            var wwwRootPath = _iwebhostenvironment.WebRootPath;
            var uploads = Path.Combine(wwwRootPath, path);
            var extension = Path.GetExtension(images.FileName);
            using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            {
                images.CopyTo(fileStreams);
            }
            updatemovie.MoviePhoto = $"\\updateimages\\{fileName}" + extension;
            _IMovie.UpdateMovies(updatemovie);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var movie = _IMovie.GetByID(id);
            return View(movie);

        }
        [HttpPost]
        public IActionResult DeleteId(int Id)
        {
            _IMovie.DeleteMovies(Id);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var movie = _IMovie.GetByID(id);
            ViewBag.Comments = _iComment.GetComments(id);
            ViewBag.Ratings = _iRating.GetRatings(id);
            
            return View(movie);
        }
    }
}
