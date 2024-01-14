using FluentValidation;
using Resources;
using ViewModels.Product;

namespace FactorMaker.Infrastructure.Validators.Product
{
    public class ProductViewModelValidator : AbstractValidator<ProductViewModel>
    {
        public ProductViewModelValidator()
        {
            RuleFor(x=>x.Name)
               .NotEmpty()
               .WithMessage(x => string.Format(ErrorMessages.Required, nameof(x.Name)));

            RuleFor(x => x.Price)
              .NotEmpty()
              .WithMessage(x => string.Format(ErrorMessages.Required, nameof(x.Price)))
              .GreaterThan(0)
              .WithMessage(x => string.Format(ErrorMessages.GreaterThan, nameof(x.Price), 0));

        }
    }
}
