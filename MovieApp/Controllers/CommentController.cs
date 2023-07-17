
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Models.Domain;
using MovieApp.Models.Dto.Comment;
using MovieApp.Repository.Implementation;
using MovieApp.Repository.Interface;
using MovieApplication.Repository.Implementations;

namespace MovieApp.Controllers
{
    public class CommentController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICommentRepo _iComment;

        public CommentController(UserManager<IdentityUser> userManager, ICommentRepo iComment)
        {
            _userManager = userManager;
            _iComment = iComment;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AddComment addcomment)
        {
            
            addcomment.UserId = _userManager.GetUserId(User);
            addcomment.TimeStamp = DateTime.Now;
            _iComment.AddComments(addcomment);
            TempData["Comment Addition"] = "Comment Added Successfully.";
            return RedirectToAction("Details","Movie" ,new { id = addcomment.MovieId });
        }



        [HttpGet]
        public IActionResult Edit(int CommentId)
        {
            var comment = _iComment.GetById(CommentId);
            return View(comment);

        }
        [HttpPost]
        public IActionResult Edit(UpdateComment updatecomment)
        {
            _iComment.UpdateComments(updatecomment);
            return RedirectToAction("Details","Movie", new { id = updatecomment.MovieId });
        }

        [HttpGet]
        public IActionResult Delete(int CommentId)
        {
            var comment = _iComment.GetById(CommentId);
            return View(comment);

        }

        [HttpPost]
        public IActionResult Delete(UpdateComment deletecomment)
        {
            _iComment.DeleteComments(deletecomment);
            return RedirectToAction("Details", "Movie", new { id = deletecomment.MovieId });
            
        }


    }
}
