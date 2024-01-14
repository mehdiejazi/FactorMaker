using FluentValidation;
using Resources;
using ViewModels.Role;

namespace FactorMaker.Infrastructure.Validators.Role
{
    public class RoleViewModelValidator : AbstractValidator<RoleViewModel>
    {
        public RoleViewModelValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .WithMessage(x => string.Format(ErrorMessages.Required, nameof(x.Name)));

        }
    }
}
