using MovieApp.Models.Domain;
using MovieApp.Models.Dto.Movie;

namespace MovieApp.Repository.Interface
{
    public interface IMovieRepository
    {
        bool AddMovies(AddMovie addmovie);
        bool UpdateMovies(UpdateMovie updatemovie);
        bool DeleteMovies(int Id);
        UpdateMovie GetByID(int Id);
        List<UpdateMovie> GetAllMovies();
       
    }
}
