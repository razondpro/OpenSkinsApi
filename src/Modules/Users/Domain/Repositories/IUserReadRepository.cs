namespace OpenSkinsApi.Modules.Users.Domain.Repositories
{
    using OpenSkinsApi.Modules.Users.Domain.Entities;
    using OpenSkinsApi.Modules.Users.Domain.ValueObjects;
    using OpenSkinsApi.Domain;

    public interface IUserReadRepository
    {
        Task<User?> Get(UniqueIdentity id);
        Task<User?> Get(UserName userName);
        Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken = default);

    }
}