using FluentValidation;

namespace OpenSkinsApi.Modules.Skins.Application.PurchaseSkin
{
    public class PurchaseSkinRequestValidator : AbstractValidator<PurchaseSkinRequestDto>
    {
        public PurchaseSkinRequestValidator()
        {
            RuleFor(x => x.SkinId)
                .NotEmpty()
                .Must(id => Guid.TryParse(id, out _))
                .WithMessage("The SkinId must be a valid GUID");
        }
    }
}