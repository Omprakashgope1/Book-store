using CommonLayer.Model.RequestModel;
using CommonLayer.Model.ResponseModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    
    public class ReviewRepo:IReviewRepo
    {
        private string connectionString;
        public ReviewRepo(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultString");
        }
        public AddReview AddReviews(long userId, AddReview reviews)
        {
            using(SqlConnection conn = new SqlConnection(connectionString)) 
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("add_review", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@review", reviews.review);
                cmd.Parameters.AddWithValue("@star", reviews.star);
                cmd.Parameters.AddWithValue("@bookId", reviews.bookId);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.ExecuteNonQuery();
                return reviews;
            }
        }
        public IEnumerable<ReviewResponse> GetAllReviews(long bookId)
        {
            using(SqlConnection conn = new SqlConnection(connectionString)) 
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("get_review", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bookId", bookId);
                SqlDataReader reader = cmd.ExecuteReader();
                List<ReviewResponse> reviews = new List<ReviewResponse>();
                while(reader.Read()) 
                {
                    ReviewResponse review = new ReviewResponse();
                    review.name = reader.GetString("name");
                    review.star = reader.GetInt32("rating");
                    review.review = reader.GetString("review");
                    review.bookId = reader.GetInt32("bookId");
                    reviews.Add(review);
                }
                return reviews;
            }
        }
    }
}
