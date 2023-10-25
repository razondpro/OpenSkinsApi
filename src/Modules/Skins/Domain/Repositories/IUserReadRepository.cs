using OpenSkinsApi.Modules.Skins.Domain.Entities;
using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;

namespace OpenSkinsApi.Modules.Skins.Domain.Repositories
{
    public interface IUserReadRepository
    {
        Task<User?> FindByEmail(Email email);
    }
}