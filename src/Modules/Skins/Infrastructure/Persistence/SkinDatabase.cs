using Microsoft.EntityFrameworkCore;
using OpenSkinsApi.Infrastructure.Persistence;
using OpenSkinsApi.Modules.Skins.Domain.Entities;

namespace OpenSkinsApi.Modules.Skins.Infrastructure.Persistence
{
    public class SkinDatabase : Database
    {
        public DbSet<Skin> Skins { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public SkinDatabase(DbContextOptions<SkinDatabase> options) : base(options) { }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SkinDatabase).Assembly);
        }

    }
}