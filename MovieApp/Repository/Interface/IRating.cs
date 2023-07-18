using MovieApp.Models.Domain;
using MovieApp.Models.Dto.Rating;

namespace MovieApp.Repository.Interface
{
    public interface IRating
    {
        bool AddRating(AddRating addrating);

        List<AddRating> Ratings(int MovieId);
    }
}
