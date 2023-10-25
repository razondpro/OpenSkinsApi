namespace OpenSkinsApi.Infrastructure.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using OpenSkinsApi.Modules.Skins.Domain.Entities;

    public class Database : DbContext
    {
        public DbSet<Skin> Skins { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public Database(DbContextOptions<Database> options) : base(options)
        {
        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Database).Assembly);
        }
    }
}