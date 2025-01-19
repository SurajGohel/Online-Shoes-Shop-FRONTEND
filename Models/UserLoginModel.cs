using System.ComponentModel.DataAnnotations;

namespace Online_Shoes_Shop.Models
{
    public class UserLoginModel
    {
        //[Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
