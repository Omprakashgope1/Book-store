using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model.RequestModel
{
    public class ResetPasswordRequest
    {
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,10}$", ErrorMessage = "Password must contain at least one lowercase letter," +
            " one uppercase letter, one digit, and one special character.")]
        public string newPassword {  get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,10}$", ErrorMessage = "Password must contain at least one lowercase letter," +
            " one uppercase letter, one digit, and one special character.")]
        public string confirmPassword { get; set; }
    }
}
