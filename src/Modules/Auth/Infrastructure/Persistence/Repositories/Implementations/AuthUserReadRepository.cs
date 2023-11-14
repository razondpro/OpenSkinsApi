namespace OpenSkinsApi.Modules.Auth.Infrastructure.Persistence.Repositories.Implementations
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Modules.Auth.Domain.Entities;
    using Modules.Auth.Domain.Repositories;
    using Modules.Auth.Domain.ValueObjects;
    using OpenSkinsApi.Domain;
    using Microsoft.EntityFrameworkCore;

    public class AuthUserReadRepository : IAuthUserReadRepository
    {
        private readonly AuthDatabase _dbContext;

        public AuthUserReadRepository(AuthDatabase database)
        {
            _dbContext = database;
        }

        public async Task<User?> Get(UniqueIdentity id)
        {
            return await _dbContext.Users.FindAsync(id.Value);
        }

        public async Task<User?> Get(Email email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => ((string)u.Email).Equals(email.Value));
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