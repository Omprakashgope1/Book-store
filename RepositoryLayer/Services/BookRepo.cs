using CommonLayer.Model.RequestModel;
using CommonLayer.Model.ResponseModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class BookRepo:IBookRepo
    {
        private string connectionString;
        public BookRepo(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultString");   
        }

        public List<BookResponse> getAllBooks()
        {
            using(SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("get_all_books", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                List<BookResponse> books = new List<BookResponse>();
                while(reader.Read())
                {
                    BookResponse book = new BookResponse();
                    book.Id = reader.GetInt32("bookId");
                    book.Title = reader.GetString("title");
                    book.price = Convert.ToDouble(reader["price"]);
                    book.Author = reader.GetString("author");
                    book.Description = reader.GetString("detail");
                    book.quantity = reader.GetInt32("quantity");
                    book.image = reader.GetString("image");
                    books.Add(book);    
                }
                return books;
            }
        }

        public BookResponse GetBook(int id)
        {
            using(SqlConnection sql = new SqlConnection(connectionString)) 
            {
                sql.Open();
                BookResponse book = new BookResponse();
                SqlCommand cmd = new SqlCommand("get_book",sql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows) throw new Exception("Book does not exists for given id"); 
                while(reader.Read()) 
                {
                    book.Id = reader.GetInt32("bookId");
                    book.Title = reader.GetString("title");
                    book.price = Convert.ToDouble(reader["price"]);
                    book.Author = reader.GetString("author");
                    book.Description = reader.GetString("detail");
                    book.quantity = reader.GetInt32("quantity");
                    book.image = reader.GetString("image");
                }
                return book;
            }
        }
        public BookResponse bookDetailsByAuthor(BookAuthorRequest req)
        {
            using(SqlConnection conn = new SqlConnection(connectionString)) 
            {
                conn.Open();
                BookResponse book = new BookResponse();
                SqlCommand cmd = new SqlCommand("book_by_author", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@author", req.Author);
                cmd.Parameters.AddWithValue("@title", req.Title);
                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows) throw new Exception("Book does not exists for given id");
                while (reader.Read())
                {
                    book.Id = reader.GetInt32("bookId");
                    book.Title = reader.GetString("title");
                    book.price = Convert.ToDouble(reader["price"]);
                    book.Author = reader.GetString("author");
                    book.Description = reader.GetString("detail");
                    book.quantity = reader.GetInt32("quantity");
                }
                return book;
            }
        }
        
    }
}
