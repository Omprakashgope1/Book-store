using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model.ResponseModel
{
    public class BookResponse
    {
        //Pid,author,title,image,quantity,price,description
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public double price { get; set; }
        public long quantity {  get; set; }
        public string image { get; set; }
    }
}
