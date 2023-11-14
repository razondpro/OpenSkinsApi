namespace OpenSkinsApi.Modules.Auth.Infrastructure.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using OpenSkinsApi.Infrastructure.Persistence;
    using OpenSkinsApi.Modules.Auth.Domain.Entities;

    public class AuthDatabase : Database
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public AuthDatabase(DbContextOptions<AuthDatabase> options) : base(options) { }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthDatabase).Assembly);
        }
    }
}