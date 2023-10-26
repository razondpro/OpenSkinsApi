using LanguageExt;
using OpenSkinsApi.Application.Queries;
using OpenSkinsApi.Modules.Skins.Domain.Entities;
using OpenSkinsApi.Modules.Skins.Domain.Repositories;
using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;

namespace OpenSkinsApi.Modules.Skins.Application.FindMySkins
{
    public class FindMySkinsQueryHandler : IQueryHandler<FindMySkinsQuery, Either<Exception, List<Purchase>>>
    {
        private readonly IOwnerReadRepository _ownerReadRepository;
        private readonly IPurchaseReadRepository _purchaseReadRepository;
        public FindMySkinsQueryHandler(IOwnerReadRepository ownerReadRepository, IPurchaseReadRepository purchaseReadRepository)
        {
            _ownerReadRepository = ownerReadRepository;
            _purchaseReadRepository = purchaseReadRepository;
        }

        public async Task<Either<Exception, List<Purchase>>> Handle(FindMySkinsQuery request, CancellationToken cancellationToken)
        {
            var email = Email.Create(request.OwnerEmail);
            var owner = await _ownerReadRepository.FindByEmail(email);

            if (owner is null)
            {
                return new OwnerNotFoundError();
            }

            var purchases = await _purchaseReadRepository.GetByOwner(owner.Id);

            return new List<Purchase>(purchases);
        }
    }
}