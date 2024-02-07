using CommonLayer.Model.RequestModel;
using CommonLayer.Model.ResponseModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class CartRepo:ICartRepo
    {
        private string connectionString;
        public CartRepo(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultString");
        }
        public List<BookResponse> AddToCart(AddToCartRequest cartRequest,long userId)
        {
            using(SqlConnection conn =  new SqlConnection(connectionString)) 
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("add_to_cart", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@bookId", cartRequest.bookId);
                cmd.Parameters.AddWithValue("@quantity", cartRequest.quantity);
                SqlParameter cartId = new SqlParameter("@cartId", DbType.Int64);
                cartId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(cartId);
                cmd.ExecuteNonQuery();
                return GetCartBooks(userId);
            }
        }
        public double GetPriceInCart(long userId)
        {
           List<BookResponse> bookList = GetCartBooks(userId);
            double totalPrice = 0;
            foreach(var book in bookList)
            {
                totalPrice += (book.quantity * book.price);
            }
            return totalPrice;
        }
        public List<BookResponse> GetCartBooks(long userId) 
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("get_cart", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                SqlDataReader reader = cmd.ExecuteReader();
                List<BookResponse> boolList = new List<BookResponse>();
                while(reader.Read()) 
                {
                    BookResponse book = new BookResponse();
                    book.Id = Convert.ToInt32(reader["bookId"]);
                    book.Author = (reader["author"]).ToString();
                    book.Title = reader.GetString("Title");
                    book.Description = reader.GetString("detail");
                    book.quantity = reader.GetInt32("cart_quantity");
                    book.price = Convert.ToDouble(reader["price"]);
                    boolList.Add(book); 
                }
                return boolList;
            }
        }
        public void UpdateBookQuantity(long userId,QuantityUpdateRequest req)
        {
            using(SqlConnection conn = new SqlConnection(this.connectionString)) 
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update_quantity_cart", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@quantity", req.quantity);
                cmd.Parameters.AddWithValue("@bookId", req.bookId);
                SqlParameter status = new SqlParameter("@status",DbType.Int32);
                status.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(status);
                cmd.ExecuteNonQuery();
                int value = Convert.ToInt32(status.Value);
                if(value == 0)
                {
                    throw new Exception("User with given");
                }
            }
        }
       
    }
}
