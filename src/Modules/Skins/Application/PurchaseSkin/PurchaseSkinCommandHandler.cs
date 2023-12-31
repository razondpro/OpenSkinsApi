using LanguageExt;
using OpenSkinsApi.Application.Commands;
using OpenSkinsApi.Domain;
using OpenSkinsApi.Modules.Skins.Domain.Repositories;
using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;

namespace OpenSkinsApi.Modules.Skins.Application.PurchaseSkin
{
    public class PurchaseSkinCommandHandler : ICommandHandler<PurchaseSkinCommand, Either<Exception, Unit>>
    {
        private readonly ISkinReadRepository _skinReadRepository;
        private readonly ISkinWriteRepository _skinWriteRepository;
        private readonly IOwnerReadRepository _ownerReadRepository;

        public PurchaseSkinCommandHandler(
            ISkinReadRepository skinReadRepository,
            ISkinWriteRepository skinWriteRepository,
            IOwnerReadRepository ownerReadRepository)
        {
            _skinReadRepository = skinReadRepository;
            _skinWriteRepository = skinWriteRepository;
            _ownerReadRepository = ownerReadRepository;
        }
        public async Task<Either<Exception, Unit>> Handle(PurchaseSkinCommand request, CancellationToken cancellationToken)
        {
            var email = Email.Create(request.Email);
            var owner = await _ownerReadRepository.FindByEmail(email);

            if (owner is null)
            {
                return new OwnerNotFoundError();
            }

            var guid = Guid.Parse(request.SkinId);
            var skin = await _skinReadRepository.Get(new UniqueIdentity(guid));

            if (skin is null)
            {
                return new SkinNotFoundError();
            }

            if (!skin.IsAvailable)
            {
                return new SkinNotAvailableError();
            }

            skin.Buy(owner);
            await _skinWriteRepository.Update(skin);

            return Unit.Default;
        }
    }
}