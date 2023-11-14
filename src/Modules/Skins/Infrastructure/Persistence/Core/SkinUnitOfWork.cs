using OpenSkinsApi.Infrastructure.Persistence.Core.UnitOfWork;
using OpenSkinsApi.Modules.Skins.Application.Abstractions;

namespace OpenSkinsApi.Modules.Skins.Infrastructure.Persistence.Core
{
    public class SkinUnitOfWork : UnitOfWork<SkinDatabase>, ISkinUnitOfWork
    {
        public SkinUnitOfWork(SkinDatabase database) : base(database) { }
    }
}