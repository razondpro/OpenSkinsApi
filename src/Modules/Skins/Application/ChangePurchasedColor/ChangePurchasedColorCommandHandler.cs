using LanguageExt;
using OpenSkinsApi.Application.Commands;
using OpenSkinsApi.Domain;
using OpenSkinsApi.Infrastructure.Persistence.Core.UnitOfWork;
using OpenSkinsApi.Modules.Skins.Application.Abstractions;
using OpenSkinsApi.Modules.Skins.Domain.Enums;
using OpenSkinsApi.Modules.Skins.Domain.Repositories;
using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;
using OpenSkinsApi.Modules.Skins.Infrastructure.Persistence;

namespace OpenSkinsApi.Modules.Skins.Application.ChangePurchasedColor
{
    public class ChangePurchasedColorCommandHandler :
        ICommandHandler<ChangePurchasedColorCommand, Either<Exception, Unit>>
    {
        private readonly IPurchaseReadRepository _purchaseReadRepository;
        private readonly ISkinUnitOfWork _unitOfWork;

        public ChangePurchasedColorCommandHandler(
            IPurchaseReadRepository purchaseReadRepository,
            ISkinUnitOfWork unitOfWork)
        {
            _purchaseReadRepository = purchaseReadRepository;
            _unitOfWork = unitOfWork;
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

            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Default;
        }
    }
}