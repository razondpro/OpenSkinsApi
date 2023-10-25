using OpenSkinsApi.Domain;
using OpenSkinsApi.Modules.Skins.Domain.Entities;

namespace OpenSkinsApi.Modules.Skins.Domain.Repositories
{
    public interface IPurchaseReadRepository
    {
        Task<Purchase?> Get(UniqueIdentity purchaseId);
    }
}