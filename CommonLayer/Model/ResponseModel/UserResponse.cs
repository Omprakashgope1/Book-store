using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model.ResponseModel
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }      
        public string mobnum { get; set; }
    }
}
