using LanguageExt;
using OpenSkinsApi.Application.Commands;
using OpenSkinsApi.Domain;
using OpenSkinsApi.Modules.Skins.Domain.Repositories;
using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;

namespace OpenSkinsApi.Modules.Skins.Application.DeletePurchase
{
    public class DeleteOwnedSkinCommandHandler :
        ICommandHandler<DeleteOwnedSkinCommand, Either<Exception, Unit>>
    {
        private readonly IPurchaseReadRepository _purchaseReadRepository;

        public DeleteOwnedSkinCommandHandler(IPurchaseReadRepository purchaseReadRepository)
        {
            _purchaseReadRepository = purchaseReadRepository;
        }
        public async Task<Either<Exception, Unit>> Handle(DeleteOwnedSkinCommand request, CancellationToken cancellationToken)
        {
            var purchaseGuid = Guid.Parse(request.PurchaseId);
            var email = Email.Create(request.OwnerEmail);

            var purchase = await _purchaseReadRepository.Get(new UniqueIdentity(purchaseGuid));

            if (purchase is null || purchase.Owner.Email != email)
            {
                return new SkinNotOwnedError();
            }

            purchase.SoftDelete();

            return Unit.Default;
        }
    }
}