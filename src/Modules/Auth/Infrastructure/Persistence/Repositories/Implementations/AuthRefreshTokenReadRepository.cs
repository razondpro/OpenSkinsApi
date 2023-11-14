using OpenSkinsApi.Modules.Auth.Domain.Entities;
using OpenSkinsApi.Modules.Auth.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace OpenSkinsApi.Modules.Auth.Infrastructure.Persistence.Repositories.Implementations
{
    public class AuthRefreshTokenReadRepository : IAuthRefreshTokenReadRepository
    {
        private readonly AuthDatabase _dbContext;

        public AuthRefreshTokenReadRepository(AuthDatabase dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RefreshToken?> Get(string token)
        {
            return await _dbContext.RefreshTokens.
            Include(t => t.User).
            FirstOrDefaultAsync(t => t.Token == token);
        }
    }
}