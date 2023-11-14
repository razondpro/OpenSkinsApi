namespace OpenSkinsApi.Modules.Auth.Domain.Repositories
{
    using Modules.Auth.Domain.Entities;
    using Modules.Auth.Domain.ValueObjects;
    using OpenSkinsApi.Domain;

    public interface IAuthUserReadRepository
    {
        Task<User?> Get(UniqueIdentity id);
        Task<User?> Get(Email email);
        Task<User?> Get(UserName userName);
        Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken = default);

    }
}