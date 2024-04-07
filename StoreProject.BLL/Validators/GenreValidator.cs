using FluentValidation;
using StoreProject.BLL.Dtos.Genre;
using StoreProject.BLL.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.BLL.Validators
{
    public class GenreValidator : AbstractValidator<GenreCreateOrUpdateDto>
    {
        public GenreValidator()
        {
            RuleFor(product => product.Name)
                .NotEmpty().WithMessage("Genre name is required.")
                .Length(1, 100).WithMessage("Genre name must be between 1 and 100 characters.");
        }
    }
}
