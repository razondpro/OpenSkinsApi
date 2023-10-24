using OpenSkinsApi.Modules.Skins.Domain.Entities;

namespace OpenSkinsApi.Modules.Skins.Domain.Repositories
{
    public interface ISkinWriteRepository
    {
        Task Update(Skin skin);
        Task Delete(Skin skin);
    }
}