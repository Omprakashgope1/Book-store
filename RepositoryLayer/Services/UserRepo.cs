using CommonLayer.Model.RequestModel;
using CommonLayer.Model.ResponseModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace RepositoryLayer.Services
{
    public class UserRepo:IUserRepo
    {
        private string connectionString;
        private readonly IConfiguration config;
        public UserRepo(IConfiguration configuration) 
        {
            this.connectionString = configuration.GetConnectionString("DefaultString");
            this.config = configuration;
        }
        public UserResponse Register(UserRequest user)
        {
            using(SqlConnection sql = new SqlConnection(this.connectionString))
            {
                sql.Open();
                SqlCommand sqlCommand = new SqlCommand("Register_User", sql);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@password", user.password);
                sqlCommand.Parameters.AddWithValue("@fullname", user.fullName);
                sqlCommand.Parameters.AddWithValue("@email", user.email);
                sqlCommand.Parameters.AddWithValue("@mobNum", user.mobnum);
                SqlParameter userId = new SqlParameter("@userId", DbType.Int32);
                userId.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(userId);
                sqlCommand.ExecuteNonQuery();
                long val = Convert.ToInt64(userId.Value);
                return GetUserById(val);
            }
            
        }
        public UserResponse GetUserById(long id) 
        {
            using(SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("get_user_by_id", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", id);
                SqlDataReader reader = cmd.ExecuteReader(); 
                UserResponse userResponse = new UserResponse();
                while(reader.Read()) 
                {
                    userResponse.Id = reader.GetInt32("userId");
                    userResponse.fullName = reader.GetString("fullname");
                    userResponse.email = reader.GetString("email");
                    userResponse.mobnum = reader.GetString("mobnum");
                }
                return userResponse;
            }
        }
        public string Login(loginRequest login)
        {
            using(SqlConnection sql = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("login_user",sql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", login.email);
                cmd.Parameters.AddWithValue("@password",login.password);
                sql.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Boolean successful = false;
                int val = 0;
                while(reader.Read())
                {
                    val = Convert.ToInt32(reader["LoginSuccess"]);
                    if (val != 0) successful = true;
                    else
                        throw new Exception("email and password does not match");
                }
                string token = GenerateToken(login.email, val);
                return token;
            }
        }
        public void AddAddress(AddAddressRequest address)
        {
            using(SqlConnection sql = new SqlConnection(this.connectionString)) 
            {
                SqlCommand cmd = new SqlCommand("add_Address", sql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", address.userId);
                cmd.Parameters.AddWithValue("@fullAddress", address.fullAddress);
                cmd.Parameters.AddWithValue("@city", address.city);
                cmd.Parameters.AddWithValue("@state", address.state);
                cmd.Parameters.AddWithValue("@type", address.type);
                sql.Open();
                cmd.ExecuteNonQuery();
            }
        }
        private string GenerateToken(string Email, long UserId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email", Email),
                new Claim("UserId", UserId.ToString())
            };
            var token = new JwtSecurityToken(config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public string ForgetPassword(ForgetPasswordRequest request)
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("forget_password", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", request.email);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                int val = 0;
                while (reader.Read())
                {
                    val = Convert.ToInt32(reader["Success"]);
                    if (val == 0)
                    {
                        throw new Exception("email and password does not match");
                    }
                }
                string token = GenerateToken(request.email, val);
                return token;
            }
        }

        public bool ResetPassword(ResetPasswordRequest request,long userId)
        {
            using(SqlConnection conn = new SqlConnection(connectionString)) 
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("reset_password", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (request.newPassword != request.confirmPassword)
                    throw new Exception("New and confirm password does not match");
                cmd.Parameters.AddWithValue("@password", request.newPassword);
                cmd.Parameters.AddWithValue("@userId", userId);
                SqlParameter status = new SqlParameter("@status",SqlDbType.Int);
                status.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(status);
                cmd.ExecuteNonQuery();
                int val = Convert.ToInt32(status.Value);
                if(val == 0)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
