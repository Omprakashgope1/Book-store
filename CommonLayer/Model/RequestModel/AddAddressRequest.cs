﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CommonLayer.Model.RequestModel
{
    public class AddAddressRequest
    {
        [JsonIgnore]
        public long userId {  get; set; }   
        public string fullAddress {  get; set; }
        public string city {  get; set; }
        public string state { get; set; }
        public int type { get; set; }
    }
}
