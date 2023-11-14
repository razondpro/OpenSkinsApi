namespace OpenSkinsApi.Modules.Auth.Domain.Repositories
{
    using Modules.Auth.Domain.Entities;
    public interface IAuthUserWriteRepository
    {
        Task Create(User user);
        Task Update(User user);
    }
}