using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model.RequestModel
{
    public class AddReview
    {
        public string review {  get; set; }
        public int star { get; set; }
        public int bookId {  get; set; }    
    }
}
