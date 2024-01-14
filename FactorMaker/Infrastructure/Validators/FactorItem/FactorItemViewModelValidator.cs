using FluentValidation;
using Resources;
using ViewModels.FactorItem;

namespace FactorMaker.Infrastructure.Validators.FactorItem
{
    public class FactorItemViewModelValidator : AbstractValidator<FactorItemViewModel>
    {
        public FactorItemViewModelValidator()
        {
            RuleFor(x => x.FactorId)
               .NotEmpty()
               .WithMessage(x => string.Format(ErrorMessages.Required, nameof(x.FactorId)));

            RuleFor(x => x.ProductId)
               .NotEmpty()
               .WithMessage(x => string.Format(ErrorMessages.Required, nameof(x.ProductId)));

            RuleFor(x => x.OffPercent)
                .NotEmpty()
                .WithMessage(x => string.Format(ErrorMessages.Required, nameof(x.OffPercent)))
                .GreaterThanOrEqualTo(0)
                .WithMessage(x => string.Format(ErrorMessages.GreaterThan, nameof(x.OffPercent), 0))
                .LessThanOrEqualTo(100)
                .WithMessage(x => string.Format(ErrorMessages.GreaterThan, nameof(x.OffPercent), 100));

            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage(x => string.Format(ErrorMessages.Required, nameof(x.Price)))
                .GreaterThan(0)
                .WithMessage(x => string.Format(ErrorMessages.GreaterThan, nameof(x.Price), 0));

            RuleFor(x => x.Quantity)
               .NotEmpty()
               .WithMessage(x => string.Format(ErrorMessages.Required,nameof(x.Quantity)))
               .GreaterThan(0)
               .WithMessage(x => string.Format(ErrorMessages.GreaterThan,nameof(x.Quantity), 0));


        }
    }
}
