using CommonLayer.Model.RequestModel;
using CommonLayer.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IAddressRepo
    {
        public IEnumerable<AddressResponse> AddAddress(AddAddressRequest address);
        public IEnumerable<AddressResponse> GetAddress(long userId);
        public IEnumerable<AddressResponse> UpdateAddress(updateAddressRequest req, long userId);
    }
}
