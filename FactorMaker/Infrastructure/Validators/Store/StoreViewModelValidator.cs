using FluentValidation;
using Resources;
using System;
using ViewModels.Store;

namespace FactorMaker.Infrastructure.Validators.Store
{
    public class StoreViewModelValidator : AbstractValidator<StoreViewModel>
    {
        public StoreViewModelValidator()
        {
            RuleFor(x => x.Url)
                .Must(BeAValidUrl)
                .WithMessage(x => string.Format(ErrorMessages.IsNotValid, x.Url));

            RuleFor(x => x.Name)
               .NotEmpty()
               .WithMessage(x => string.Format(ErrorMessages.Required, nameof(x.Name)));

            RuleFor(x => x.OwnerId)
               .NotEmpty()
               .WithMessage(x => string.Format(ErrorMessages.Required, nameof(x.OwnerId)));

            RuleFor(x => x.StoreId)
               .NotEmpty()
               .WithMessage(x => string.Format(ErrorMessages.Required, nameof(x.StoreId)));

            RuleFor(x => x.LogoUrl)
                .Must(BeAValidUrl)
                .WithMessage(x => string.Format(ErrorMessages.IsNotValid, x.Url));
        }
        private bool BeAValidUrl(string myUrl)
        {
            return Uri.IsWellFormedUriString(myUrl, UriKind.Absolute);

        }
    }
}
