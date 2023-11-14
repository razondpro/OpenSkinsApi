using Microsoft.EntityFrameworkCore;
using OpenSkinsApi.Domain;
using OpenSkinsApi.Infrastructure.Persistence;
using OpenSkinsApi.Modules.Skins.Domain.Entities;
using OpenSkinsApi.Modules.Skins.Domain.Repositories;

namespace OpenSkinsApi.Modules.Skins.Infrastructure.Persistence.Repositories.Implementations
{
    public class SkinReadRepository : ISkinReadRepository
    {
        private readonly SkinDatabase _context;

        public SkinReadRepository(SkinDatabase context)
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
    }
}