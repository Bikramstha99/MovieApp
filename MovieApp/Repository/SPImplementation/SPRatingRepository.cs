using Microsoft.Data.SqlClient;
using MovieApp.Models.Dto.Comment;
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
                using (SqlCommand command = new SqlCommand("spAddComment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RatingId", addrating.RatingId);
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
            throw new NotImplementedException();

        }

        public int GetRatingByUserIdAndMovieId(string UserId, int MovieId)
        {
            throw new NotImplementedException();
        }

        public List<AddRating> GetRatings(int MovieId)
        {
            List<AddRating> ratings = new List<AddRating>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("spGetAllRatings", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AddRating rating= new AddRating
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
            throw new NotImplementedException();
        }
    }
}
