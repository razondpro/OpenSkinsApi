namespace OpenSkinsApi.Modules.Users.Infrastructure.Persistence.Repositories.Implementations
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenSkinsApi.Modules.Users.Domain.Entities;
    using OpenSkinsApi.Modules.Users.Domain.Repositories;
    using OpenSkinsApi.Modules.Users.Domain.ValueObjects;
    using OpenSkinsApi.Domain;
    using Microsoft.EntityFrameworkCore;

    public class UserReadRepository : IUserReadRepository
    {
        private readonly UserDatabase _dbContext;
        public UserReadRepository(UserDatabase database)
        {
            _dbContext = database;
        }
        public async Task<User?> Get(UniqueIdentity id)
        {
            return await _dbContext.Users.FindAsync(id);
        }
        public async Task<User?> Get(UserName userName)
        {
            return await _dbContext.Users
                .Where(u => ((string)u.UserName).Equals(userName.Value))
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users.ToListAsync(cancellationToken);
        }
    }
}