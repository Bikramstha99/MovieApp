using MovieApp.Models.Domain;
using MovieApp.Models.Dto.Rating;

namespace MovieApp.Repository.Interface
{
    public interface IRatingRepository
    {
        bool AddRating(AddRating addrating);
        bool UpdateRating(AddRating addrating);
        int GetRatingByUserIdAndMovieId(string UserId, int MovieId);
        double GetAverageRating(int MovieId);
        List<AddRating> GetRatings(int MovieId);

    }
}
