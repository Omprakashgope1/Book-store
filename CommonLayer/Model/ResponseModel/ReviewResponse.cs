using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model.ResponseModel
{
    public class ReviewResponse
    {
        public string name { get; set; }
        public string review { get; set; }
        public int star { get; set; }
        public int bookId { get; set; }
    }
}
