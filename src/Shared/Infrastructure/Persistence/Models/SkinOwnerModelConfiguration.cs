using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenSkinsApi.Domain;
using OpenSkinsApi.Modules.Skins.Domain.Entities;

namespace OpenSkinsApi.Modules.Skins.Infrastructure.Persistence.Configurations
{
    public class SkinOwnerModelConfiguration : IEntityTypeConfiguration<SkinOwner>
    {
        public void Configure(EntityTypeBuilder<SkinOwner> builder)
        {
            // Table name
            builder.ToTable("skin_owners");
            // Primary key
            builder.HasKey(us => new { us.Id, us.OwnerId, us.SkinId });

            // Properties
            builder.Property(us => us.Id)
                .IsRequired()
                .HasColumnName("id")
                .HasConversion(
                    id => id.Value,
                    value => new UniqueIdentity(value));

            builder.Property(us => us.OwnerId)
                .IsRequired()
                .HasColumnName("owner_id")
                .HasConversion(
                    id => id.Value,
                    value => new UniqueIdentity(value));

            builder.Property(us => us.SkinId)
                .IsRequired()
                .HasColumnName("skin_id")
                .HasConversion(
                    id => id.Value,
                    value => new UniqueIdentity(value));

            builder.Property(us => us.CreatedOn)
                .IsRequired()
                .HasColumnName("created_on");

            builder.Property(us => us.LastModifiedOn)
                .HasColumnName("last_modified_on");

            builder.Property(us => us.Color)
                .IsRequired()
                .HasColumnName("color");

            builder.Property(us => us.DeletedAt)
                .HasColumnName("deleted_at");

            // Relationships
            builder.HasOne(us => us.Owner)
                .WithMany()
                .HasForeignKey(us => us.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(us => us.Skin)
                .WithMany(s => s.SkinOwners)
                .HasForeignKey(us => us.SkinId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}