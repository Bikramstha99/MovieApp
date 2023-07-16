using MovieApp.Data;
using MovieApp.Models.Dto.Comment;
using MovieApp.Models.Domain;
using MovieApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using MovieApplication.Repository.Implementations;

namespace MovieApp.Repository.Implementation
{
    public class CommentRepo : ICommentRepo
    {
        private readonly MovieDbContext _movieDbContext;


        public CommentRepo(MovieDbContext movieDbContext)
        {
            _movieDbContext = movieDbContext;
        }

        public bool AddComments(AddComment addcomment)
        {
            var comment = new Comment()
            {
                CommentId= addcomment.CommentId,
                MovieId= addcomment.MovieId,
                UserId = addcomment.UserId,
                CommentDesc = addcomment.CommentDesc,
                TimeStamp = addcomment.TimeStamp,
            };
            _movieDbContext.Comments.Add(comment);
            _movieDbContext.SaveChanges();
            return true;
        }
        public List<Comment> GetComments(int MovieId)
        {
            var data = _movieDbContext.Comments.Include(e => e.IdentityUser).Where(c => c.MovieId == MovieId).ToList();
            return data;

        }

        public bool UpdateComments(UpdateComment updatecomment)
        {
            var comment = _movieDbContext.Comments.Find(updatecomment.CommentId);
            comment.CommentDesc = updatecomment.CommentDesc;

            _movieDbContext.SaveChanges();
            return true;
        }
        public UpdateComment GetById(int CommentId)
        {
            var comments = _movieDbContext.Comments.Find(CommentId);
            var comment = new UpdateComment()
            {
                CommentId = comments.CommentId,
                CommentDesc = comments.CommentDesc,
                UserId = comments.UserId,

            };
            return comment;
        }
        public bool DeleteComments(UpdateComment deletecomment)
        {
            var comment = _movieDbContext.Comments.Find(deletecomment.CommentId);
            _movieDbContext.Comments.Remove(comment);
            _movieDbContext.SaveChanges();
            return true;
        }

       
    }
}
