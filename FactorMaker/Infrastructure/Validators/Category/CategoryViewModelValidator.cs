using FluentValidation;
using Models;
using Resources;
using ViewModels.Authentication;

namespace FactorMaker.Infrastructure.Validators.Category
{
    public class CategoryViewModelValidator : AbstractValidator<CategoryViewModel>
    {
        public CategoryViewModelValidator()
        {
            RuleFor(x=>x.OwnerId)
               .NotEmpty()
               .WithMessage(x => string.Format(ErrorMessages.Required,nameof(x.OwnerId)));

            RuleFor(x=>x.Name)
               .NotEmpty()
               .WithMessage(x => string.Format(ErrorMessages.Required,nameof(x.Name)));
        }
    }
}
