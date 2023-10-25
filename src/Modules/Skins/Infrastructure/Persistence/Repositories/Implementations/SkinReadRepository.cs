using Microsoft.EntityFrameworkCore;
using OpenSkinsApi.Domain;
using OpenSkinsApi.Infrastructure.Persistence;
using OpenSkinsApi.Modules.Skins.Domain.Entities;
using OpenSkinsApi.Modules.Skins.Domain.Repositories;

namespace OpenSkinsApi.Modules.Skins.Infrastructure.Persistence.Repositories.Implementations
{
    public class SkinReadRepository : ISkinReadRepository
    {
        private readonly Database _context;

        public SkinReadRepository(Database context)
        {
            _context = context;
        }

        public async Task<Skin?> Get(UniqueIdentity id)
        {
            return await _context.Skins.FindAsync(id);
        }

        public async Task<List<Skin>> GetAvailable()
        {
            return await _context.Skins.Where(skin => skin.IsAvailable).ToListAsync();
        }

        public async Task<List<Skin>> GetOwnedSkins(UniqueIdentity ownerId)
        {
            return await _context.Skins
                            .Where(skin => skin.Purchases.Any(us => us.OwnerId == ownerId))
                            .ToListAsync();
        }
    }
}