using Common;
using FluentValidation;
using Resources;
using ViewModels.Customer;

namespace FactorMaker.Infrastructure.Validators.Customer
{
    public class CustomerViewModelValidator : AbstractValidator<CustomerViewModel>
    {
        public CustomerViewModelValidator()
        {
            //RuleFor(x => x.OwnerId)
            //  .NotEmpty()
            //  .WithMessage(x => string.Format(ErrorMessages.Required, nameof(x.OwnerId)));

            RuleFor(x => x.FirstName)
               .NotEmpty()
               .WithMessage(x => string.Format(ErrorMessages.Required,nameof(x.FirstName)));

            RuleFor(x => x.LastName)
               .NotEmpty()
               .WithMessage(x => string.Format(ErrorMessages.Required, nameof(x.LastName)));

            //RuleFor(x => x.NationalCode)
            //    .Must(BeAValidNationalCode).WithMessage(ErrorMessages.NationalCodeInvalid);

        }

        private bool BeAValidNationalCode(string nationalCode)
        {
            return Utilities.IsValidNationalCode(nationalCode);
        }
    }
}
