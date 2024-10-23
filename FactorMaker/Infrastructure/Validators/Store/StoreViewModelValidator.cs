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
                .WithMessage(x => string.Format(ErrorMessages.IsNotValid, nameof(x.Url)));

            RuleFor(x => x.Name)
               .NotEmpty()
               .WithMessage(x => string.Format(ErrorMessages.Required, nameof(x.Name)));

            //RuleFor(x => x.OwnerId)
            //   .NotEmpty()
            //   .WithMessage(x => string.Format(ErrorMessages.Required, nameof(x.OwnerId)));

            RuleFor(x => x.StoreEnglishName)
                .Must(BeLatinAlphanumeric)
                .WithMessage(x => string.Format(ErrorMessages.MustBeLatinAlphanumeric, nameof(x.StoreEnglishName)));

            RuleFor(x => x.StoreEnglishName)
               .NotEmpty()
               .WithMessage(x => string.Format(ErrorMessages.Required, nameof(x.StoreEnglishName)));

        }
        private bool BeValidUrl(string myUrl)
        {
            if (string.IsNullOrEmpty(myUrl)) return true;
            return Uri.IsWellFormedUriString(myUrl, UriKind.Absolute);

        }
        static bool BeLatinAlphanumeric(string input)
        {
            return Regex.IsMatch(input, "^[a-zA-Z0-9]+$");
        }
    }
}
