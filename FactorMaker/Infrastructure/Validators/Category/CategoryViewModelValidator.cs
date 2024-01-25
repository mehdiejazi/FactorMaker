using FluentValidation;
using Resources;
using ViewModels.Category;

namespace FactorMaker.Infrastructure.Validators.Category
{
    public class CategoryViewModelValidator : AbstractValidator<CategoryViewModel>
    {
        public CategoryViewModelValidator()
        {
            RuleFor(x=>x.StoreId)
               .NotEmpty()
               .WithMessage(x => string.Format(ErrorMessages.Required,nameof(x.StoreId)));

            RuleFor(x=>x.Name)
               .NotEmpty()
               .WithMessage(x => string.Format(ErrorMessages.Required,nameof(x.Name)));
        }
    }
}
