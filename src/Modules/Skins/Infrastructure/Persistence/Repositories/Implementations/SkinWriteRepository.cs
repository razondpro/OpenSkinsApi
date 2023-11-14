using OpenSkinsApi.Infrastructure.Persistence;
using OpenSkinsApi.Modules.Skins.Domain.Entities;
using OpenSkinsApi.Modules.Skins.Domain.Repositories;

namespace OpenSkinsApi.Modules.Skins.Infrastructure.Persistence.Repositories.Implementations
{
    public class SkinWriteRepository : ISkinWriteRepository
    {
        private readonly SkinDatabase _context;
        public SkinWriteRepository(SkinDatabase context)
        {
            _context = context;
        }
        public Task Update(Skin skin)
        {
            _context.Skins.Update(skin);
            return Task.CompletedTask;
        }
    }
}