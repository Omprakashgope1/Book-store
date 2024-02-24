using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model.RequestModel
{
    public class UserRequest
    {
        [Required(ErrorMessage = "{0} should not be empty")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "First name start with Cap and Should have minimum 3 character")]
        [DataType(DataType.Text)]
        public string fullName { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(320, ErrorMessage = "Email address should not exceed 320 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address.")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,10}$", ErrorMessage = "Password must contain at least one lowercase letter," +
            " one uppercase letter, one digit, and one special character.")]
        public string password { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        [RegularExpression(@"[0-9]{10}", ErrorMessage = "Mobile number should contain 10 numbers")]
        public string mobnum { get; set; }
    }
}
