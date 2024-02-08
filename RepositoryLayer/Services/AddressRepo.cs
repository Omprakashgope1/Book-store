using CommonLayer.Model.RequestModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using CommonLayer.Model.ResponseModel;
using System.Net;

namespace RepositoryLayer.Services
{
    public class AddressRepo:IAddressRepo
    {
        private string connectionString;
        public AddressRepo(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultString");
        }
        public void AddAddress(AddAddressRequest address)
        {
            using (SqlConnection sql = new SqlConnection(this.connectionString))
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
        public IEnumerable<AddressResponse> GetAddress(long userId) 
        {
            using (SqlConnection sql = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("get_address", sql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userId);
                sql.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<AddressResponse> addresses = new List<AddressResponse>();  
                while (reader.Read()) 
                {
                    AddressResponse response = new AddressResponse();
                    response.fullAddress = reader.GetString("fullAddress");
                    response.state = reader.GetString("state");
                    response.city = reader.GetString("city");
                    addresses.Add(response);
                }
                return addresses;
            }
        }
    }
}
