using OpenSkinsApi.Modules.Auth.Domain.Entities;

namespace OpenSkinsApi.Modules.Auth.Domain.Repositories
{
    public interface IAuthRefreshTokenReadRepository
    {
        Task<RefreshToken?> Get(string token);
    }
}