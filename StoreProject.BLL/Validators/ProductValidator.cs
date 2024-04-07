using FluentValidation;
using StoreProject.BLL.Dtos.Product;


namespace StoreProject.BLL.Validators
{
    public class ProductValidator : AbstractValidator<ProductCreateOrUpdateDto>
    {
        public ProductValidator() 
        {
            RuleFor(product => product.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .Length(1, 100).WithMessage("Product name must be between 1 and 100 characters.");

            RuleFor(product => product.PriceUSD)
             .GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative.")
             .LessThanOrEqualTo(10000).WithMessage("Price cannot exceed 10000.");

            RuleFor(product => product.GenreId)
            .NotNull().WithMessage("GenreId is required.");

            RuleFor(product => product.CategoryId)
                .NotNull().WithMessage("CategoryId is required.");

            RuleFor(product => product.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

        }
    }
}
