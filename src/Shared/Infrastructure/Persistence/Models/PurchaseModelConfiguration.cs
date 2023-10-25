using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenSkinsApi.Domain;
using OpenSkinsApi.Modules.Skins.Domain.Entities;

namespace OpenSkinsApi.Modules.Skins.Infrastructure.Persistence.Configurations
{
    public class PurchaseModelConfiguration : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            // Table name
            builder.ToTable("purchases");
            // Primary key
            builder.HasKey(p => new { p.Id, p.OwnerId, p.SkinId });

            // Properties
            builder.Property(p => p.Id)
                .IsRequired()
                .HasColumnName("id")
                .HasConversion(
                    id => id.Value,
                    value => new UniqueIdentity(value));

            builder.Property(p => p.OwnerId)
                .IsRequired()
                .HasColumnName("owner_id")
                .HasConversion(
                    id => id.Value,
                    value => new UniqueIdentity(value));

            builder.Property(p => p.SkinId)
                .IsRequired()
                .HasColumnName("skin_id")
                .HasConversion(
                    id => id.Value,
                    value => new UniqueIdentity(value));

            builder.Property(p => p.CreatedOn)
                .IsRequired()
                .HasColumnName("created_on");

            builder.Property(p => p.LastModifiedOn)
                .HasColumnName("last_modified_on");

            builder.Property(p => p.Color)
                .IsRequired()
                .HasColumnName("color");

            builder.Property(p => p.DeletedAt)
                .HasColumnName("deleted_at");

            // Relationships
            builder.HasOne(p => p.Owner)
                .WithMany()
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Skin)
                .WithMany(s => s.Purchases)
                .HasForeignKey(p => p.SkinId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}