using OpenSkinsApi.Infrastructure.Persistence.Core.UnitOfWork;
using OpenSkinsApi.Modules.Auth.Application.Abstractions;

namespace OpenSkinsApi.Modules.Auth.Infrastructure.Persistence.Core
{
    public class AuthUnitOfWork : UnitOfWork<AuthDatabase>, IAuthUnitOfWork
    {
        public AuthUnitOfWork(AuthDatabase database) : base(database) { }
    }
}