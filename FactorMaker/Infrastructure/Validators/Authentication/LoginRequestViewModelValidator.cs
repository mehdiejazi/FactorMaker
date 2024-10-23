 using FluentValidation;
using Resources;
using ViewModels.Authentication;

namespace FactorMaker.Infrastructure.Validators.Authentication
{
    public class LoginRequestViewModelValidator : AbstractValidator<LoginRequestViewModel>
    {
        public LoginRequestViewModelValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage(x=> string.Format(ErrorMessages.Required,nameof(x.UserName)));

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(x => string.Format(ErrorMessages.Required,nameof(x.Password)));
        }
    }
}
