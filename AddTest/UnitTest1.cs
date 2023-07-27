using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MovieApp.Controllers;
using MovieApp.Repository.Interface;
using MovieApp.Models.Domain;
using MovieApp.Models.Dto.Movie;
using Microsoft.AspNetCore.Hosting;

namespace MovieApp.Tests
{
    public class MovieControllerTests
    {
        [Fact]
        public void Create_Post_RedirectsToIndex()
        {
            // Arrange
            var movieRepositoryMock = new Mock<IMovieRepository>();
            var hostingEnvironmentMock = new Mock<IWebHostEnvironment>();
            var commentRepositoryMock = new Mock<ICommentRepository>();
            var ratingRepositoryMock = new Mock<IRatingRepository>();

            var movieController = new MovieController(movieRepositoryMock.Object, hostingEnvironmentMock.Object, commentRepositoryMock.Object, ratingRepositoryMock.Object);
            ////movieController.ControllerContext = new ControllerContext
            ////{
            ////    HttpContext = new DefaultHttpContext()
            ////};
            var addMovie = new AddMovie
            {
                Name = "Test Movie",
                Genre="anIme",
                Director = "John Doe",
                Description = "This is a test movie for unit testing.",
                MoviePhoto ="~/images/testmovie.jpg"

            };
            // Set up the mock to verify the AddMovies method is called with the expected parameters
            movieRepositoryMock.Setup(x => x.AddMovies(It.Is<AddMovie>(p => p.Name == addMovie.Name)));


            // Act
            var result = movieController.Create(addMovie);

            // Assert
            movieRepositoryMock.Verify(x => x.AddMovies(addMovie), Times.Once);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
        [Fact]
        public void Create_Post_InvalidModel_ReturnsViewWithInvalidModel()
        {
            // Arrange
            var movieRepositoryMock = new Mock<IMovieRepository>();
            var hostingEnvironmentMock = new Mock<IWebHostEnvironment>();
            var commentRepositoryMock = new Mock<ICommentRepository>();
            var ratingRepositoryMock = new Mock<IRatingRepository>();

            var movieController = new MovieController(movieRepositoryMock.Object, hostingEnvironmentMock.Object, commentRepositoryMock.Object, ratingRepositoryMock.Object);

            // Invalid model with missing required fields
            var invalidAddMovie = new AddMovie
            {
                // Missing the "Name" and "Genre" properties
                Director = "John Doe",
                Description = "This is a test movie for unit testing.",
                MoviePhoto = "~/images/testmovie.jpg"
            };

            // Act
            var result = movieController.Create(invalidAddMovie);

            // Assert
            movieRepositoryMock.Verify(x => x.AddMovies(It.IsAny<AddMovie>()), Times.Never);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Create", viewResult.ViewName);
            Assert.NotNull(viewResult.Model); // Ensure the view model is not null
            Assert.IsType<AddMovie>(viewResult.Model); // Ensure the view model is of type AddMovie
        }
    }

}

