using Microsoft.Data.SqlClient;
using MovieApp.Models.Domain;
using MovieApp.Models.Dto.Comment;
using MovieApp.Models.Dto.Movie;
using MovieApp.Models.Dto.Rating;
using MovieApp.Repository.Interface;
using System.Data;

namespace MovieApp.Repository.SPImplementation
{
    public class SPRatingRepository : IRatingRepository
    {
        private readonly IConfiguration _iConfiguration;
        private string connectionString;

        public SPRatingRepository(IConfiguration iConfiguration)
        {
            _iConfiguration = iConfiguration;
            connectionString = _iConfiguration.GetConnectionString("MvcConnectionString");
        }

        public bool AddRating(AddRating addrating)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("spAddRating", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    
                    command.Parameters.AddWithValue("@Ratings", addrating.Ratings);
                    command.Parameters.AddWithValue("@UserId", addrating.UserId);
                    command.Parameters.AddWithValue("@MovieId", addrating.MovieId);


                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return true;
        }

        public double GetAverageRating(int MovieId)
        {
            UpdateMovie avgrating= new UpdateMovie();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("spGetAverageRating", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MovieId", MovieId);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            avgrating = new UpdateMovie
                            {
                                AverageRating = Convert.ToDouble(reader["AverageRating"]),
                            };
                        }
                    }
                }
            }
            return avgrating.AverageRating;
        }
           

        public int GetRatingByUserIdAndMovieId(string UserId, int MovieId)
        {
            AddRating rating = new AddRating();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("spGetRatingOnIdAndMovie", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@MovieId", MovieId);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            rating = new AddRating
                            {
                                Ratings = (int)reader["Ratings"],
                            };
                        }
                    }
                }
            }
            return rating.Ratings;
        }



        public List<AddRating> GetRatings(int MovieId)
        {
            List<AddRating> ratings = new List<AddRating>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("spGetAllRatings", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MovieId", MovieId);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AddRating rating = new AddRating
                            {

                                RatingId = (int)reader["RatingId"],
                                Ratings = (int)reader["Ratings"],
                                UserId = reader["UserId"].ToString(),
                                MovieId = (int)reader["MovieId"],
                            };
                            ratings.Add(rating);
                        }
                    }
                }
            }

            return ratings;
        }

        public bool UpdateRating(AddRating addrating)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("spUpdateRating", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Ratings", addrating.Ratings);
                    command.Parameters.AddWithValue("@UserId", addrating.UserId);
                    command.Parameters.AddWithValue("@MovieId", addrating.MovieId);


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
    }
}