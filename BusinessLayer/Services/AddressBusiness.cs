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
        public void AddAddress(AddAddressRequest address)
        {
            _addressRepo.AddAddress(address);
        }
        public IEnumerable<AddressResponse> GetAddress(long userId)
        {
            return _addressRepo.GetAddress(userId);
        }
    }
}
