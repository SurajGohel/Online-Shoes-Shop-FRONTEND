
using FluentValidation;
using Online_Shoes_Shop.Models;

namespace Online_Shoes_Shop.Validator
{
    public class UserLoginValidator : AbstractValidator<UserLoginModel>
    {
        
        public UserLoginValidator() 
        {
            RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username is required")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters long");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
        }
    }
}
