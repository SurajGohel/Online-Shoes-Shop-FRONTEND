using System.ComponentModel.DataAnnotations;

namespace Online_Shoes_Shop.Models
{
    public class RegisterUserModel
    {
        [Required(ErrorMessage ="Please enter Username")]
        [Display(Name ="User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Please Enter Password")]
        [Compare("ConfirmPassword",ErrorMessage ="Password Does Not Match")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Confirm your Password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
