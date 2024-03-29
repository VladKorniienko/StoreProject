using FluentValidation;
using StoreProject.DAL.Models;

namespace StoreProject.BLL.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Username).NotEmpty().WithMessage("Username is required.");
            RuleFor(user => user.Password).MinimumLength(6).WithMessage("Password has to be longer than 6 symbols.");
            RuleFor(user => user.Password).NotEmpty().WithMessage("Password is required.");
            RuleFor(user => user.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(user => user.Email).EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).WithMessage("Email has unknown format.");
        }
    }
}
