using FluentValidation;
using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;

namespace OpenSkinsApi.Modules.Skins.Application.DeletePurchase
{
    public class DeletePurchaseRequestValidator : AbstractValidator<DeletePurchaseRequestDto>
    {
        public DeletePurchaseRequestValidator()
        {
            RuleFor(x => x.PurchaseId)
                .NotEmpty()
                .Must(id => Guid.TryParse(id, out _))
                .WithMessage("The PurchaseId must be a valid GUID");
            RuleFor(x => x.OwnerEmail)
                .NotEmpty()
                .MaximumLength(Email.MaxLength)
                .Matches(Email.EmailRegex);
        }
    }
}