using FluentValidation;
using Resources;
using ViewModels.Customer;
using ViewModels.Factor;

namespace FactorMaker.Infrastructure.Validators.Factor
{
    public class FactorViewModelValidator : AbstractValidator<FactorViewModel>
    {
        public FactorViewModelValidator()
        {
            RuleFor(x => x.OwnerId)
              .NotEmpty()
              .WithMessage(x => string.Format(ErrorMessages.Required,nameof(x.OwnerId)));

        }
    }
}
