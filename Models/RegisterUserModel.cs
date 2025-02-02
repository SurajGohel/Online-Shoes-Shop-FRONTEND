using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Online_Shoes_Shop.Models
{
    public class RegisterUserModel
    {
        [Required(ErrorMessage = "Please enter Username")]
        [Display(Name = "User Name")]
        [MinLength(4, ErrorMessage = "Username must be at least 4 characters long")]
        [MaxLength(40, ErrorMessage = "Username cannot exceed 40 characters")]
        [RegularExpression(@"^(?!\d+$)[a-zA-Z0-9]+$", ErrorMessage = "Username cannot be only digits and must contain letters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter Email")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [Compare("ConfirmPassword", ErrorMessage = "Password does not match")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$", ErrorMessage = "Password must contain at least one letter and one number")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Confirm your Password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
