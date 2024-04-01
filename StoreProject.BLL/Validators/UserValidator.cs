using FluentValidation;
using StoreProject.DAL.Models;

namespace StoreProject.BLL.Validators
{
    //It's useless, due to the usage of IdentityOptions. Might as well delete it later.
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.UserName).NotEmpty().WithMessage("Username is required.");
            //RuleFor(user => user.Password).MinimumLength(6).WithMessage("Password has to be longer than 6 symbols.");
            //RuleFor(user => user.Password).NotEmpty().WithMessage("Password is required.");
            RuleFor(user => user.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(user => user.Email).EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).WithMessage("Email has unknown format.");
        }
    }
}
