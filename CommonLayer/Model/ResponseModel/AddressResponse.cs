﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model.ResponseModel
{
    public class AddressResponse
    {
        public long addressId {  get; set; }
        public string fullAddress {  get; set; }
        public string city {  get; set; }
        public string state { get; set; }   
        public int type { get; set; }
    }
}
