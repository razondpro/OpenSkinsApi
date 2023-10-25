using OpenSkinsApi.Modules.Skins.Domain.Entities;
using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;

namespace OpenSkinsApi.Modules.Skins.Domain.Repositories
{
    public interface IOwnerReadRepository
    {
        Task<Owner?> FindByEmail(Email email);
    }
}