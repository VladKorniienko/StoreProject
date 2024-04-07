using FluentValidation;
using StoreProject.BLL.Dtos.Category;
using StoreProject.BLL.Dtos.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.BLL.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryCreateOrUpdateDto>
    {
        public CategoryValidator()
        {
            RuleFor(product => product.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .Length(1, 100).WithMessage("Category name must be between 1 and 100 characters.");
        }
    }
}
