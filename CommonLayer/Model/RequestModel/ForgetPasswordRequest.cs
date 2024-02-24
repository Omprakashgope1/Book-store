using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model.RequestModel
{
    public class ForgetPasswordRequest
    {
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(320, ErrorMessage = "Email address should not exceed 320 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address.")]
        [DataType(DataType.EmailAddress)]
        public string email {  get; set; }
    }
}
