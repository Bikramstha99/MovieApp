using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Models.Domain;
using MovieApp.Models.Dto.Movie;
using MovieApp.Repository.Interface;


namespace MovieApplication.Repository.Implementations
{
    public class MovieRepo : IMovieRepo
    {
        private readonly MovieDbContext _moviedbcontext;
        public MovieRepo(MovieDbContext moviedbcontext)
        {
            _moviedbcontext = moviedbcontext;

        }

        public bool AddMovies(AddMovie addmovie)
        {
            var movie = new Movies()
            {
                Name = addmovie.Name,
                Genre = addmovie.Genre,
                MoviePhoto = addmovie.MoviePhoto,
                Director = addmovie.Director,


            };
            _moviedbcontext.Movies.Add(movie);
            _moviedbcontext.SaveChanges();
            return true;
        }

        public Movies GetByID(int Id)
        {
            var movie = _moviedbcontext.Movies.Find(Id);
            var viewmodel = new UpdateMovie()
            {
                Id = movie.Id,
                Name = movie.Name,
                Genre = movie.Genre,
                MoviePhoto = movie.MoviePhoto,
                Director = movie.Director,
            };
            return movie;
        }

        public bool UpdateMovies(UpdateMovie updatemovie)
        {
            var movie = _moviedbcontext.Movies.Find(updatemovie.Id);
            movie.Id = updatemovie.Id;
            movie.Name = updatemovie.Name;
            movie.Genre = updatemovie.Genre;
            movie.MoviePhoto = updatemovie.MoviePhoto;
            movie.Director = updatemovie.Director;
            _moviedbcontext.SaveChanges();
            return true;
        }

        public bool DeleteMovies(UpdateMovie deletemovie)
        {
            var movie = _moviedbcontext.Movies.Find(deletemovie.Id);
            _moviedbcontext.Movies.Remove(movie);
            _moviedbcontext.SaveChanges();
            return true;
        }

        public List<Movies> GetAllMovies()
        {
            var movies = _moviedbcontext.Movies.ToList();
            return movies;
        }
      
    }
}

