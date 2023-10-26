using LanguageExt;
using OpenSkinsApi.Application.Commands;
using OpenSkinsApi.Domain;
using OpenSkinsApi.Modules.Skins.Domain.Enums;
using OpenSkinsApi.Modules.Skins.Domain.Repositories;
using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;

namespace OpenSkinsApi.Modules.Skins.Application.ChangePurchasedColor
{
    public class ChangePurchasedColorCommandHandler :
        ICommandHandler<ChangePurchasedColorCommand, Either<Exception, Unit>>
    {
        private readonly IPurchaseReadRepository _purchaseReadRepository;

        public ChangePurchasedColorCommandHandler(IPurchaseReadRepository purchaseReadRepository)
        {
            _purchaseReadRepository = purchaseReadRepository;
        }

        public async Task<Either<Exception, Unit>> Handle(ChangePurchasedColorCommand request, CancellationToken cancellationToken)
        {
            Color color = (Color)request.ColorNumber;
            var purchaseGuid = Guid.Parse(request.PurchaseId);
            var email = Email.Create(request.OwnerEmail);

            var purchase = await _purchaseReadRepository.Get(new UniqueIdentity(purchaseGuid));

            if (purchase is null || purchase.Owner.Email != email)
            {
                return new SkinNotOwnedError();
            }

            if (purchase.Color == color)
            {
                return new SkinAlreadyHasSameColorError();
            }

            purchase.ChangeColor(color);

            return Unit.Default;
        }
    }
}