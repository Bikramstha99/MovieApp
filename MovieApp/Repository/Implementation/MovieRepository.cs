using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Models.Domain;
using MovieApp.Models.Dto.Comment;
using MovieApp.Models.Dto.Movie;
using MovieApp.Repository.Interface;


namespace MovieApplication.Repository.Implementations
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieDbContext _moviedbcontext;
        public MovieRepository(MovieDbContext moviedbcontext)
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

        public UpdateMovie GetByID(int Id)
        {
            var movie = _moviedbcontext.Movies.Find(Id);
            var viewmodel = new UpdateMovie()
            {
                Id = movie.Id,
                Name = movie.Name,
                Genre = movie.Genre,
                MoviePhoto = movie.MoviePhoto,
                Director = movie.Director,
                AverageRating = movie.AverageRating,
            };
            return viewmodel;
        }

        public bool UpdateMovies(UpdateMovie updatemovie)
        {
            var movie = _moviedbcontext.Movies.Find(updatemovie.Id);
            movie.Id = updatemovie.Id;
            movie.Name = updatemovie.Name;
            movie.Genre = updatemovie.Genre;
            movie.MoviePhoto = updatemovie.MoviePhoto;
            movie.Director = updatemovie.Director;
            movie.AverageRating=updatemovie.AverageRating;
            _moviedbcontext.SaveChanges();
            return true;
        }

        public bool DeleteMovies(int Id)
        {
            var movie = _moviedbcontext.Movies.Find(Id);
            _moviedbcontext.Movies.Remove(movie);
            _moviedbcontext.SaveChanges();
            return true;
        }

        public List<UpdateMovie> GetAllMovies()
        {
            var data = _moviedbcontext.Movies.Select(d => new UpdateMovie
            {
                Id = d.Id,
                Name = d.Name,
                Genre = d.Genre,
                Director = d.Director,
                Year = d.Year,
                MoviePhoto = d.MoviePhoto
            }).ToList();
            return data;
        }

        
    }
}

