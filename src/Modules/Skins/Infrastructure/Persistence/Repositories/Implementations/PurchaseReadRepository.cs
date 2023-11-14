using Microsoft.EntityFrameworkCore;
using OpenSkinsApi.Domain;
using OpenSkinsApi.Infrastructure.Persistence;
using OpenSkinsApi.Modules.Skins.Domain.Entities;
using OpenSkinsApi.Modules.Skins.Domain.Repositories;

namespace OpenSkinsApi.Modules.Skins.Infrastructure.Persistence.Repositories.Implementations
{
    public class PurchaseReadRepository : IPurchaseReadRepository
    {
        private readonly SkinDatabase _database;

        public PurchaseReadRepository(SkinDatabase database)
        {
            _database = database;
        }

        public async Task<Purchase?> Get(UniqueIdentity purchaseId)
        {
            return await _database.Purchases
                .Include(p => p.Owner)
                .Include(p => p.Skin)
                .FirstOrDefaultAsync(p => p.Id == purchaseId && !p.DeletedAt.HasValue);
        }

        public async Task<IReadOnlyList<Purchase>> GetByOwner(UniqueIdentity ownerId)
        {
            return await _database.Purchases
                .Include(p => p.Skin)
                .Where(p => p.Owner.Id == ownerId && !p.DeletedAt.HasValue)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}