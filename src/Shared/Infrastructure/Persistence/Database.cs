namespace OpenSkinsApi.Infrastructure.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using OpenSkinsApi.Infrastructure.Persistence.Core.Outbox;
    using OpenSkinsApi.Modules.Skins.Domain.Entities;

    public class Database : DbContext
    {
        public DbSet<Skin> Skins { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        public DbSet<OutboxMessageConsumer> OutboxMessageConsumers { get; set; }
        public Database(DbContextOptions<Database> options) : base(options) { }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Database).Assembly);
        }
    }
}