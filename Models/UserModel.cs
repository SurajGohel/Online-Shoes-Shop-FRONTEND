using System.ComponentModel.DataAnnotations;

namespace Online_Shoes_Shop.Models
{
    public class UserModel
    {
        public class UserLoginModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
        }

        public class RegisterModel
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        

    }
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
