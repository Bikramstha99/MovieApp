using Microsoft.Data.SqlClient;
using MovieApp.Models.Domain;
using MovieApp.Models.Dto.Movie;
using MovieApp.Repository.Interface;
using System.Data;

namespace MovieApp.Repository.SPImplementation
{
    public class SPMovieRepository : IMovieRepository
    {
        private readonly IConfiguration _iConfiguration;
        private string connectionString;
        public SPMovieRepository(IConfiguration iConfiguration)
        {
            _iConfiguration = iConfiguration;
            connectionString = _iConfiguration.GetConnectionString("MvcConnectionString");
        }

        public bool AddMovies(AddMovie addmovie)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("spAddMovie", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", addmovie.Name);
                    command.Parameters.AddWithValue("@Genre", addmovie.Genre);
                    command.Parameters.AddWithValue("@MoviePhoto", addmovie.MoviePhoto);
                    command.Parameters.AddWithValue("@Director", addmovie.Director);
                    command.Parameters.AddWithValue("@Description", addmovie.Description);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return true;
        }

        public bool DeleteMovies(int Id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("spDeleteMovie", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                return true;
            }
        }

        public bool UpdateMovies(UpdateMovie updatemovie)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("spUpdateMovie", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", updatemovie.Id);
                    command.Parameters.AddWithValue("@Name", updatemovie.Name);
                    command.Parameters.AddWithValue("@Genre", updatemovie.Genre);
                    command.Parameters.AddWithValue("@Director", updatemovie.Director != null ? updatemovie.Director : DBNull.Value);
                    command.Parameters.AddWithValue("@MoviePhoto", updatemovie.MoviePhoto != null ? updatemovie.MoviePhoto : DBNull.Value);
                    command.Parameters.AddWithValue("@AverageRating", updatemovie.AverageRating != null ? updatemovie.AverageRating : DBNull.Value);
                    command.Parameters.AddWithValue("@Description", updatemovie.Description);

                    // Add any other parameters required by the stored procedure, if applicable

                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public List<UpdateMovie> GetAllMovies()
            {
                List<UpdateMovie> movies = new List<UpdateMovie>();

                // string connectionString = _configuration.GetConnectionString("DefaultConnection");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("spGetAllMovies", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UpdateMovie movie = new UpdateMovie
                                {
                                    Id = (int)reader["Id"],
                                    Name = reader["Name"].ToString(),
                                    Director = reader["Director"].ToString(),
                                    Genre = reader["Genre"].ToString(),
                                    MoviePhoto = reader["MoviePhoto"].ToString(),
                                    AverageRating = Convert.ToDouble(reader["AverageRating"]),
                                    Description= reader["Description"].ToString(),
                                };
                                movies.Add(movie);
                            }
                        }
                    }
                }

                return movies;
            }

            public UpdateMovie GetByID(int Id)
            {
                UpdateMovie movie = new UpdateMovie();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("spGetMovieById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Id", Id);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                movie = new UpdateMovie
                                {
                                    Id = (int)reader["Id"],
                                    Name = reader["Name"].ToString(),
                                    Director = reader["Director"].ToString(),
                                    Genre = reader["Genre"].ToString(),
                                    MoviePhoto = reader["MoviePhoto"].ToString(),
                                    AverageRating = Convert.ToDouble(reader["AverageRating"]),
                                    Description= reader["Description"].ToString(),
                                };
                            }
                        }
                    }
                }
                return movie;
            }      
    }
}
        
    

