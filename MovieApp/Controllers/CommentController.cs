
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
        private readonly ICommentRepository _iComment;

        public CommentController(UserManager<IdentityUser> userManager, ICommentRepository iComment)
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
            addcomment.UserName = _userManager.GetUserName(User);
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
            updatecomment.UserId = _userManager.GetUserId(User);
            updatecomment.UserName = _userManager.GetUserName(User);
            updatecomment.TimeStamp = DateTime.Now;
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
        public IActionResult Delete(int CommentId, UpdateComment deletecomment)
        {
            _iComment.DeleteComments(CommentId);
            return RedirectToAction("Details", "Movie", new { id = deletecomment.MovieId });
            
        }


    }
}
