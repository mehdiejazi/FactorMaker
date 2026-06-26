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

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(x => string.Format(ErrorMessages.Required, nameof(x.Email)))
                .EmailAddress()
                .WithMessage("Email is invalid");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage(x => string.Format(ErrorMessages.Required, nameof(x.FirstName)));

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage(x => string.Format(ErrorMessages.Required, nameof(x.LastName)));

            RuleFor(x => x.StoreName)
                .NotEmpty()
                .WithMessage(x => string.Format(ErrorMessages.Required, nameof(x.StoreName)));

            RuleFor(x => x.StoreEnglishName)
                .NotEmpty()
                .WithMessage(x => string.Format(ErrorMessages.Required, nameof(x.StoreEnglishName)));
        }
    }
}
