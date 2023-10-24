using OpenSkinsApi.Domain;
using OpenSkinsApi.Modules.Skins.Domain.Entities;

namespace OpenSkinsApi.Modules.Skins.Domain.Repositories
{
    public interface ISkinReadRepository
    {
        Task<Skin?> Get(UniqueIdentity id);
        Task<List<Skin>> GetAvailable();
        Task<List<Skin>> GetSkinsOwnedByUser(UniqueIdentity userId);

    }
}