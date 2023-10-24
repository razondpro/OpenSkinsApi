using OpenSkinsApi.Application.Queries;
using OpenSkinsApi.Modules.Skins.Domain.Entities;
using OpenSkinsApi.Modules.Skins.Domain.Repositories;

namespace OpenSkinsApi.Modules.Skins.Application.FindAvailableSkins
{
    public class FindAvailableSkinsQueryHandler : IQueryHandler<FindAvailableSkinsQuery, List<Skin>>
    {
        private readonly ISkinReadRepository _skinReadRepository;

        public FindAvailableSkinsQueryHandler(ISkinReadRepository skinReadRepository)
        {
            _skinReadRepository = skinReadRepository;
        }

        public async Task<List<Skin>> Handle(FindAvailableSkinsQuery request, CancellationToken cancellationToken)
        {
            var skins = await _skinReadRepository.GetAvailable();

            return skins;
        }
    }
}