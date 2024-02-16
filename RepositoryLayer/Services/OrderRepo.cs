using CommonLayer.Model.RequestModel;
using CommonLayer.Model.ResponseModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class OrderRepo:IOrderRepo
    {
        private string ConnectionString;
        public OrderRepo(IConfiguration configuration)
        {
            this.ConnectionString = configuration.GetConnectionString("DefaultString");
        }
        public List<BookResponse> Addorder(AddOrder orders,long userId)
        {
            using(SqlConnection conn = new SqlConnection(this.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("add_order", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@bookId", orders.bookId);
                cmd.Parameters.AddWithValue("@quantity",orders.quantity);
                cmd.ExecuteNonQuery();
                return GetAllOrders(userId);
            }
        }
        public List<BookResponse> GetAllOrders(long userId)
        {
            using(SqlConnection conn = new SqlConnection(this.ConnectionString)) 
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("get_all_order", conn);
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                SqlDataReader reader = cmd.ExecuteReader();
                List<BookResponse> orders = new List<BookResponse>();
                while(reader.Read()) 
                {
                    BookResponse book = new BookResponse();
                    book.Id = Convert.ToInt32(reader["bookId"]);
                    book.Author = (reader["author"]).ToString();
                    book.Title = reader.GetString("Title");
                    book.Description = reader.GetString("detail");
                    book.quantity = reader.GetInt32("order_quantity");
                    book.price = Convert.ToDouble(reader["price"]);
                    book.image = reader.GetString("image");
                    orders.Add(book);
                }
                return orders;
            }
        }
        public async Task<string> SendTokKen(string emailTo,string toSendList,double toSendAmount)
        {
            string fromAddress = "omgope123@gmail.com";
            string toAddress = emailTo;
            string subject = "Your Order List with price";
            string body = toSendList + "/n"+ toSendAmount;

            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
            {
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential("omgope123@gmail.com", "unvu ubah dhvn xbdk");
                smtpClient.EnableSsl = true;

                using (MailMessage mailMessage = new MailMessage(fromAddress, toAddress, subject, body))
                {
                    try
                    {
                        await smtpClient.SendMailAsync(mailMessage);
                        return "Email sent successfully.";
                    }
                    catch (Exception ex)
                    {
                        return $"Error sending email: {ex.Message}";
                    }
                }
            }
        }
        public List<BookResponse> OrderCart(List<AddOrder> orders,long userId,string email) 
        {
            foreach(var order in orders) 
            {
                Addorder(order, userId);  
            }
            double TotalPrice = GetPriceInOrder(userId);
            List<BookResponse> bookList = GetAllOrders(userId);
            string stringList = bookList.ToString();
            SendTokKen(email, stringList, TotalPrice);
            return bookList;
        }
        public double GetPriceInOrder(long userId)
        {
            List<BookResponse> bookList = GetAllOrders(userId);
            double totalPrice = 0;
            foreach (var book in bookList)
            {
                totalPrice += (book.quantity * book.price);
            }
            return totalPrice;
        }
    }
}
