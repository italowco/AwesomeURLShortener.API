using AweSomeURLShortener.Application.DTO;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace AweSomeURLShortener.API.Validators
{
    public class AddUrlValidator : AbstractValidator<AddUrlDTO>
    {
        public AddUrlValidator()
        {
            RuleFor(dto => dto.Url)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("URL is required.")
                .Must(BeValidUri).WithMessage("Invalid URL format.");
        }

        private bool BeValidUri(string uri)
        {
            try
            {
                new Uri(uri);
                return true;
            }
            catch (UriFormatException)
            {
                return false;
            }   
        }
    }
}
