using FluentValidation;
using OpenSkinsApi.Modules.Skins.Domain.Enums;

namespace OpenSkinsApi.Modules.Skins.Application.ChangePurchasedColor
{
    public class ChangePurchasedColorRequestValidator : AbstractValidator<ChangePurchasedColorRequestDto>
    {
        public ChangePurchasedColorRequestValidator()
        {
            RuleFor(x => x.ColorNumber)
                .NotNull()
                .Must(color => Enum.IsDefined(typeof(Color), color))
                .WithMessage("Invalid color value.");
            RuleFor(x => x.PurchaseId)
                .NotEmpty()
                .Must(id => Guid.TryParse(id, out _))
                .WithMessage("The PurchaseId must be a valid GUID");
        }

    }
}