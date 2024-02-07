using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model.RequestModel
{
    public class QuantityUpdateRequest
    {
        public int quantity {  get; set; }
        public long bookId {  get; set; }   
    }
}
