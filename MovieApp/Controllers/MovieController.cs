using Microsoft.AspNetCore.Mvc;
using MovieApp.Models.Domain;
using MovieApp.Models.Dto.Comment;
using MovieApp.Models.Dto.Movie;
using MovieApp.Repository.Implementation;
using MovieApp.Repository.Interface;
using System.Xml.Linq;

namespace MovieApp.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieRepo _IMovie;
        private readonly IWebHostEnvironment _iwebhostenvironment;
        private readonly ICommentRepo _iComment;

        public MovieController(IMovieRepo imovie, IWebHostEnvironment iwebhostenvironment,ICommentRepo iComment)
        {
            _IMovie = imovie;
            _iwebhostenvironment = iwebhostenvironment;
            _iComment = iComment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var data = _IMovie.GetAllMovies();
            return View(data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AddMovie addmovie)
        {
            var image = Request.Form.Files.FirstOrDefault();
            var fileName = Guid.NewGuid().ToString();
            var path = $@"images\";
            var wwwRootPath = _iwebhostenvironment.WebRootPath;
            var uploads = Path.Combine(wwwRootPath, path);
            var extension = Path.GetExtension(image.FileName);
            using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            {
                image.CopyTo(fileStreams);
            }
            addmovie.MoviePhoto = $"\\images\\{fileName}" + extension;
            _IMovie.AddMovies(addmovie);

            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var movie = _IMovie.GetByID(Id);
            return View(movie);
        }

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
        public IActionResult Delete(UpdateMovie deletemovie)
        {
            _IMovie.DeleteMovies(deletemovie);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var movie = _IMovie.GetByID(id);
            ViewBag.Comments = _iComment.GetComments(id);
            return View(movie);
        }
    }
}
