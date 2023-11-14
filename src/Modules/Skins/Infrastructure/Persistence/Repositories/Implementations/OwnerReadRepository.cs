using Microsoft.EntityFrameworkCore;
using OpenSkinsApi.Infrastructure.Persistence;
using OpenSkinsApi.Modules.Skins.Domain.Entities;
using OpenSkinsApi.Modules.Skins.Domain.Repositories;
using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;

namespace OpenSkinsApi.Modules.Skins.Infrastructure.Persistence.Repositories.Implementations
{
    public class OwnerReadRepository : IOwnerReadRepository
    {
        private readonly SkinDatabase _context;

        public OwnerReadRepository(SkinDatabase context)
        {
            _context = context;
        }

        public async Task<Owner?> FindByEmail(Email email)
        {
            return await _context.Owners.FirstOrDefaultAsync(u => ((string)u.Email).Equals(email.Value));
        }
    }
}