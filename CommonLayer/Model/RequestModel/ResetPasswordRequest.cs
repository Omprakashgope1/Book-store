using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model.RequestModel
{
    public class ResetPasswordRequest
    { 
        public string newPassword {  get; set; }
        public string confirmPassword { get; set; }
    }
}
