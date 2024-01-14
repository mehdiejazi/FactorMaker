using Common;
using FluentValidation;
using Resources;
using ViewModels.User;

namespace FactorMaker.Infrastructure.Validators.User
{
    public class UserRegisterViewModelValidator : AbstractValidator<UserRegisterViewModel>
    {
        public UserRegisterViewModelValidator()
        {

            RuleFor(x => x.UserName)
              .NotEmpty()
              .WithMessage(x => string.Format(ErrorMessages.Required, nameof(x.UserName)));

            RuleFor(x => x.Password)
              .NotEmpty()
              .WithMessage(x => string.Format(ErrorMessages.Required, nameof(x.Password)));

            RuleFor(x => x.FirstName)
              .NotEmpty()
              .WithMessage(x => string.Format(ErrorMessages.Required, nameof(x.FirstName)));

            RuleFor(x => x.LastName)
               .NotEmpty()
               .WithMessage(x => string.Format(ErrorMessages.Required, nameof(x.LastName)));

            RuleFor(x => x.NationalCode)
                .Must(BeAValidNationalCode).WithMessage(ErrorMessages.NationalCodeInvalid);

        }

        private bool BeAValidNationalCode(string nationalCode)
        {
            return Utilities.IsValidNationalCode(nationalCode);
        }
    }
}
