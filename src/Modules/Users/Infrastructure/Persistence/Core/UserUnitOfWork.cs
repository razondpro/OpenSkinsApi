using OpenSkinsApi.Infrastructure.Persistence.Core.UnitOfWork;
using OpenSkinsApi.Modules.Users.Application.Abstractions;

namespace OpenSkinsApi.Modules.Users.Infrastructure.Persistence.Core
{
    public class UserUnitOfWork : UnitOfWork<UserDatabase>, IUserUnitOfWork
    {
        public UserUnitOfWork(UserDatabase database) : base(database) { }
    }
}