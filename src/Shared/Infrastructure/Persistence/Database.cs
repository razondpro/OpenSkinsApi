namespace OpenSkinsApi.Infrastructure.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using OpenSkinsApi.Infrastructure.Persistence.Core.Outbox;
    public class Database : DbContext
    {
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        public DbSet<OutboxMessageConsumer> OutboxMessageConsumers { get; set; }
        public Database(DbContextOptions options) : base(options) { }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Database).Assembly);
        }
    }
}