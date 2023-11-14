namespace OpenSkinsApi.Modules.Users.Domain.Repositories
{
    using OpenSkinsApi.Modules.Users.Domain.Entities;
    public interface IUserWriteRepository
    {
        Task Create(User user);
        Task Update(User user);
    }
}