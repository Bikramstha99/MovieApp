using MovieApp.Data;
using MovieApp.Models.Dto.Comment;
using MovieApp.Models.Domain;
using MovieApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using MovieApplication.Repository.Implementations;

namespace MovieApp.Repository.Implementation
{
    public class CommentRepository : ICommentRepository
    {
        private readonly MovieDbContext _movieDbContext;


        public CommentRepository(MovieDbContext movieDbContext)
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
        public List<UpdateComment> GetComments(int MovieId)
        {
            var data = _movieDbContext.Comments.Include(e => e.IdentityUser).Where(c => c.MovieId == MovieId).Select(d=>new UpdateComment
            {
                MovieId = d.MovieId,
                IdentityUser = d.IdentityUser,
                UserId = d.UserId,
                CommentDesc = d.CommentDesc,
                CommentId = d.CommentId,
                TimeStamp=d.TimeStamp,
            }).ToList();
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
                TimeStamp = DateTime.Now,
                CommentId = comments.CommentId,
                CommentDesc = comments.CommentDesc,
                UserId = comments.UserId,
                MovieId=comments.MovieId,

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
