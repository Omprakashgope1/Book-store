﻿using CommonLayer.Model.RequestModel;
using CommonLayer.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IAddressBusiness
    {
        public IEnumerable<AddressResponse> AddAddress(AddAddressRequest address);
        public IEnumerable<AddressResponse> GetAddress(long userId);
        public IEnumerable<AddressResponse> UpdateAddress(updateAddressRequest req, long userId);
    }
}
