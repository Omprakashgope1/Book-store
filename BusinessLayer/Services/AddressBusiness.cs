using BusinessLayer.Interface;
using CommonLayer.Model.RequestModel;
using CommonLayer.Model.ResponseModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class AddressBusiness:IAddressBusiness
    {
        private IAddressRepo _addressRepo;
        public AddressBusiness(IAddressRepo addressRepo)
        {
            _addressRepo = addressRepo;
        }
        public IEnumerable<AddressResponse> AddAddress(AddAddressRequest address)
        {
            return _addressRepo.AddAddress(address);
        }
        public IEnumerable<AddressResponse> GetAddress(long userId)
        {
            return _addressRepo.GetAddress(userId);
        }
        public IEnumerable<AddressResponse> UpdateAddress(updateAddressRequest req, long userId)
        {
            return _addressRepo.UpdateAddress(req, userId);
        }
    }
}
