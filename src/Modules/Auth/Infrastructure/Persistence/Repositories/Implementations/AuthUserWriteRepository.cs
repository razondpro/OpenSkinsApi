namespace OpenSkinsApi.Modules.Auth.Infrastructure.Persistence.Repositories.Implementations
{
    using OpenSkinsApi.Modules.Auth.Domain.Repositories;
    using OpenSkinsApi.Modules.Auth.Domain.Entities;

    public class AuthUserWriteRepository : IAuthUserWriteRepository
    {

        private readonly AuthDatabase _dbContext;

        public AuthUserWriteRepository(AuthDatabase database)
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
            _dbContext.SaveChangesAsync();

            return Task.CompletedTask;
        }
    }

}