using MovieApp.Models.Domain;
using MovieApp.Models.Dto.Comment;
using MovieApp.Repository.Implementation;

namespace MovieApp.Repository.Interface
{
    public interface ICommentRepository
    {
        bool AddComments(AddComment addcomment);
        bool DeleteComments(UpdateComment deletecomment);
        bool UpdateComments(UpdateComment updatecomment);
        UpdateComment GetById(int CommentId);
        List<UpdateComment> GetComments(int MovieId);
    }
}
