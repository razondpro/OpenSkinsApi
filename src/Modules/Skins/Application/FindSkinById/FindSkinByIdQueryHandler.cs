using LanguageExt;
using OpenSkinsApi.Application.Queries;
using OpenSkinsApi.Domain;
using OpenSkinsApi.Modules.Skins.Domain.Entities;
using OpenSkinsApi.Modules.Skins.Domain.Repositories;

namespace OpenSkinsApi.Modules.Skins.Application.FindSkinById
{
    public class FindSkinByIdQueryHandler : IQueryHandler<FindSkinByIdQuery, Either<Exception, Skin>>
    {
        private readonly ISkinReadRepository _skinReadRepository;

        public FindSkinByIdQueryHandler(ISkinReadRepository skinReadRepository)
        {
            _skinReadRepository = skinReadRepository;
        }
        public async Task<Either<Exception, Skin>> Handle(FindSkinByIdQuery request, CancellationToken cancellationToken)
        {
            var guid = Guid.Parse(request.Id);
            var skin = await _skinReadRepository.Get(new UniqueIdentity(guid));

            if (skin is null)
            {
                return new SkinNotFoundByIdError();
            }

            return skin;
        }
    }
}