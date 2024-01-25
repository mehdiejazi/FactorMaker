using FluentValidation;
using Resources;
using System;
using System.Text.RegularExpressions;
using ViewModels.Store;

namespace FactorMaker.Infrastructure.Validators.Store
{
    public class StoreViewModelValidator : AbstractValidator<StoreViewModel>
    {
        public StoreViewModelValidator()
        {
            RuleFor(x => x.Url)
                .Must(BeValidUrl)
                .WithMessage(x => string.Format(ErrorMessages.IsNotValid, x.Url));

            RuleFor(x => x.Name)
               .NotEmpty()
               .WithMessage(x => string.Format(ErrorMessages.Required, nameof(x.Name)));

            RuleFor(x => x.OwnerId)
               .NotEmpty()
               .WithMessage(x => string.Format(ErrorMessages.Required, nameof(x.OwnerId)));

            RuleFor(x => x.EnglishName)
                .Must(BeLatinAlphanumeric)
                .WithMessage(x => string.Format(ErrorMessages.MustBeLatinAlphanumeric, nameof(x.EnglishName)));

            RuleFor(x => x.EnglishName)
               .NotEmpty()
               .WithMessage(x => string.Format(ErrorMessages.Required, nameof(x.EnglishName)));

        }
        private bool BeValidUrl(string myUrl)
        {
            return Uri.IsWellFormedUriString(myUrl, UriKind.Absolute);

        }
        static bool BeLatinAlphanumeric(string input)
        {
            return Regex.IsMatch(input, "^[a-zA-Z0-9]+$");
        }
    }
}
