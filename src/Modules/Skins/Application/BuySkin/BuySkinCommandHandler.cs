using LanguageExt;
using OpenSkinsApi.Application.Commands;
using OpenSkinsApi.Domain;
using OpenSkinsApi.Modules.Skins.Domain.Repositories;
using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;

namespace OpenSkinsApi.Modules.Skins.Application.BuySkin
{
    public class BuySkinCommandHandler : ICommandHandler<BuySkinCommand, Either<Exception, Unit>>
    {
        private readonly ISkinReadRepository _skinReadRepository;
        private readonly ISkinWriteRepository _skinWriteRepository;
        private readonly IUserReadRepository _userReadRepository;

        public BuySkinCommandHandler(
            ISkinReadRepository skinReadRepository,
            ISkinWriteRepository skinWriteRepository,
            IUserReadRepository userReadRepository)
        {
            _skinReadRepository = skinReadRepository;
            _skinWriteRepository = skinWriteRepository;
            _userReadRepository = userReadRepository;
        }
        public async Task<Either<Exception, Unit>> Handle(BuySkinCommand request, CancellationToken cancellationToken)
        {
            var email = Email.Create(request.Email);
            var user = await _userReadRepository.FindByEmail(email);

            if (user is null)
            {
                return new UserNotFoundError();
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

            skin.AddOwner(user);
            await _skinWriteRepository.Update(skin);

            return Unit.Default;
        }
    }
}