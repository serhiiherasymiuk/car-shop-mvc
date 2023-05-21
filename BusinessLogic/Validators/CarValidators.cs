using DataBase.Entities;
using FluentValidation;

namespace BusinessLogic.Validators
{
    public class CarValidators : AbstractValidator<Car>
    {
        public CarValidators()
        {
            RuleFor(x => x.Model)
                .NotNull()
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(100);

            RuleFor(x => x.Color)
                .NotNull()
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(100);

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Value must be greater than null");

            RuleFor(x => x.ReleaseYear)
                .GreaterThan(0).WithMessage("Release year must be greater than null");

            RuleFor(x => x.ImagePath)
                .Must(LinkMustBeAUri).WithMessage("{PropertyName} has incorrect URL format");
        }

        private static bool LinkMustBeAUri(string? link)
        {
            if (string.IsNullOrWhiteSpace(link))
            {
                return false;
            }

            Uri outUri;
            return Uri.TryCreate(link, UriKind.Absolute, out outUri)
                   && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps);
        }
    }
}
