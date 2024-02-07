using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model.RequestModel
{
    public class AddToCartRequest
    {
        public int bookId {  get; set; }
        public int quantity { get; set; }
    }
}
