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
        public void AddAddress(AddAddressRequest address);
        public IEnumerable<AddressResponse> GetAddress(long userId);
    }
}
