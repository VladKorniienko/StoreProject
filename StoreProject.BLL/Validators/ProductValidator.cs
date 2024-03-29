using FluentValidation;
using StoreProject.DAL.Models;


namespace StoreProject.BLL.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator() 
        {
            RuleFor(product => product.Name).NotEmpty().WithMessage("Product name is required.");
            //RuleFor(product => product.PriceUSD).GreaterThanOrEqualTo(0).W
        }
    }
}
