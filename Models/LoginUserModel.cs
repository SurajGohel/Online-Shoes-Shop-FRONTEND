using System.ComponentModel.DataAnnotations;

namespace Online_Shoes_Shop.Models
{
    public class LoginUserModel
    {
        [Required]
        public string UserName {  get; set; }
        [Required]
        [DataType(DataType.Password)]   
        public string Password { get; set; }
        [Display(Name ="Remember Me")]
        public bool RememberMe {  get; set; }
    }
}
