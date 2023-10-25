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
            builder.HasKey(us => new { us.Id, us.UserId, us.SkinId });

            // Properties
            builder.Property(us => us.Id)
                .IsRequired()
                .HasColumnName("id")
                .HasConversion(
                    id => id.Value,
                    value => new UniqueIdentity(value));

            builder.Property(us => us.UserId)
                .IsRequired()
                .HasColumnName("user_id")
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

            // Relationships
            builder.HasOne(us => us.User)
                .WithMany()
                .HasForeignKey(us => us.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(us => us.Skin)
                .WithMany(s => s.SkinOwners)
                .HasForeignKey(us => us.SkinId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}