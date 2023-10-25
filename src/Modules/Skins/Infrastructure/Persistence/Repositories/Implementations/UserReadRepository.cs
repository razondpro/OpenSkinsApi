using Microsoft.EntityFrameworkCore;
using OpenSkinsApi.Infrastructure.Persistence;
using OpenSkinsApi.Modules.Skins.Domain.Entities;
using OpenSkinsApi.Modules.Skins.Domain.Repositories;
using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;

namespace OpenSkinsApi.Modules.Skins.Infrastructure.Persistence.Repositories.Implementations
{
    public class UserReadRepository : IUserReadRepository
    {
        private readonly Database _context;

        public UserReadRepository(Database context)
        {
            _context = context;
        }

        public async Task<User?> FindByEmail(Email email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => ((string)u.Email).Equals(email.Value));
        }
    }
}