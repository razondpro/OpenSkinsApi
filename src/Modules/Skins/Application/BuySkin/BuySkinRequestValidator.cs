using FluentValidation;
using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;

namespace OpenSkinsApi.Modules.Skins.Application.BuySkin
{
    public class BuySkinRequestValidator : AbstractValidator<BuySkinRequestDto>
    {
        public BuySkinRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(Email.MaxLength)
                .Matches(Email.EmailRegex);

            RuleFor(x => x.SkinId)
                .NotEmpty()
                .Must(id => Guid.TryParse(id, out _))
                .WithMessage("The SkinId must be a valid GUID");
        }
    }
}