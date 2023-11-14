using LanguageExt;
using OpenSkinsApi.Application.Commands;
using OpenSkinsApi.Domain;
using OpenSkinsApi.Infrastructure.Persistence.Core.UnitOfWork;
using OpenSkinsApi.Modules.Skins.Application.Abstractions;
using OpenSkinsApi.Modules.Skins.Domain.Repositories;
using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;
using OpenSkinsApi.Modules.Skins.Infrastructure.Persistence;

namespace OpenSkinsApi.Modules.Skins.Application.DeletePurchase
{
    public class DeleteOwnedSkinCommandHandler :
        ICommandHandler<DeleteOwnedSkinCommand, Either<Exception, Unit>>
    {
        private readonly IPurchaseReadRepository _purchaseReadRepository;
        private readonly ISkinUnitOfWork _unitOfWork;

        public DeleteOwnedSkinCommandHandler(
            IPurchaseReadRepository purchaseReadRepository,
            ISkinUnitOfWork unitOfWork)
        {
            _purchaseReadRepository = purchaseReadRepository;
            _unitOfWork = unitOfWork;
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

            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Default;
        }
    }
}