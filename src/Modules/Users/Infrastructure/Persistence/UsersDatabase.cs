namespace OpenSkinsApi.Modules.Users.Infrastructure.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using OpenSkinsApi.Infrastructure.Persistence;
    using OpenSkinsApi.Modules.Users.Domain.Entities;

    public class UserDatabase : Database
    {
        public DbSet<User> Users { get; set; }
        public UserDatabase(DbContextOptions<UserDatabase> options) : base(options) { }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserDatabase).Assembly);
        }
    }
}