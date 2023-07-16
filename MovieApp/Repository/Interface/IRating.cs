using MovieApp.Models.Domain;
using MovieApp.Models.Dto.Rating;

namespace MovieApp.Repository.Interface
{
    public interface IRating
    {
        Task AddRating(AddRating addrating);
        
        int RatingCount(Guid MovieId);
    }
}
