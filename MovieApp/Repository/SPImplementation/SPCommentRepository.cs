using Microsoft.Data.SqlClient;
using MovieApp.Data;
using MovieApp.Models.Domain;
using MovieApp.Models.Dto.Comment;
using MovieApp.Models.Dto.Movie;
using MovieApp.Repository.Interface;
using System.ComponentModel.Design;
using System.Data;

namespace MovieApp.Repository.SPImplementation
{
    public class SPCommentRepository : ICommentRepository
    {
        private readonly IConfiguration _iConfiguration;
        private string connectionString;

        public SPCommentRepository(IConfiguration iConfiguration)
        {
            _iConfiguration = iConfiguration;
            connectionString = _iConfiguration.GetConnectionString("MvcConnectionString");
        }

        public bool AddComments(AddComment addcomment)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("spAddComment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserName", addcomment.UserName);
                    command.Parameters.AddWithValue("@CommentDesc", addcomment.CommentDesc);
                    command.Parameters.AddWithValue("@UserId", addcomment.UserId);
                    command.Parameters.AddWithValue("@MovieId", addcomment.MovieId);
                    command.Parameters.AddWithValue("@CreatedAt", addcomment.TimeStamp);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return true;
        }

        public bool DeleteComments(int CommentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("spDeleteComment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CommentId", CommentId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                return true;
            }
        }

        public UpdateComment GetById(int CommentId)
        {
            UpdateComment comment = new UpdateComment();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("spGetCommentById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CommentId", CommentId);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            comment = new UpdateComment
                            {
                                CommentId = (int)reader["CommentId"],
                                CommentDesc = reader["COmmentDesc"].ToString(),
                                UserName = reader["UserName"].ToString(),
                                MovieId = (int)reader["MovieId"],
                                UserId = reader["UserId"].ToString(),
                            };
                        }
                    }
                }
            }
            return comment;
        }

        public List<UpdateComment> GetComments(int MovieId)
        {
            List<UpdateComment> comments = new List<UpdateComment>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("spGetAllComment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MovieId", MovieId);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UpdateComment comment = new UpdateComment
                            {
                                UserName = reader["UserName"].ToString(),
                                CommentId = (int)reader["CommentId"],
                                CommentDesc = reader["CommentDesc"].ToString(),
                                UserId = reader["UserId"].ToString(),
                                MovieId = (int)reader["MovieId"],
                                TimeStamp = Convert.ToDateTime(reader["TimeStamp"]),


                            };
                            comments.Add(comment);
                        }
                    }
                }
            }

            return comments;
        }

        public bool UpdateComments(UpdateComment updatecomment)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("spUpdateComment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CommentId", updatecomment.CommentId);
                    command.Parameters.AddWithValue("@CommentDesc", updatecomment.CommentDesc);
                    command.Parameters.AddWithValue("@UserId", updatecomment.UserId);
                    command.Parameters.AddWithValue("@UserName", updatecomment.UserName != null ? updatecomment.UserName : DBNull.Value);
                    command.Parameters.AddWithValue("@MovieId", updatecomment.MovieId != null ? updatecomment.MovieId : DBNull.Value);
                    command.Parameters.AddWithValue("@CreatedAt", updatecomment.TimeStamp);


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

