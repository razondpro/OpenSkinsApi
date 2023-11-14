namespace OpenSkinsApi.Modules.Users.Infrastructure.Persistence.Repositories.Implementations
{
    using OpenSkinsApi.Modules.Users.Domain.Repositories;
    using OpenSkinsApi.Modules.Users.Domain.Entities;

    public class UserWriteRepository : IUserWriteRepository
    {
        private readonly UserDatabase _dbContext;
        public UserWriteRepository(UserDatabase database)
        {
            _dbContext = database;
        }
        public async Task Create(User user)
        {
            await _dbContext.Users.AddAsync(user);
        }
        public Task Update(User user)
        {
            _dbContext.Users.Update(user);
            return Task.CompletedTask;
        }
    }

}