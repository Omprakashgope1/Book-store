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
        public IEnumerable<AddressResponse> AddAddress(AddAddressRequest address)
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
                return GetAddress(address.userId);
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
                    response.type = reader.GetInt32("type");
                    response.addressId = reader.GetInt32("addressId");
                    addresses.Add(response);
                }
                return addresses;
            }
        }
        public IEnumerable<AddressResponse> UpdateAddress(updateAddressRequest req,long userId)
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update_address",conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@addressId", req.addressId);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@fullAddress", req.fullAddress);
                cmd.Parameters.AddWithValue("@state", req.state);
                cmd.Parameters.AddWithValue("@city", req.city);
                cmd.Parameters.AddWithValue("@type",req.type);
                cmd.ExecuteNonQuery();
                return GetAddress(userId);
            }
        }
    }
}
