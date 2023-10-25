using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenSkinsApi.Domain;
using OpenSkinsApi.Infrastructure.Persistence.Seed;
using OpenSkinsApi.Modules.Skins.Domain.Entities;
using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;

namespace OpenSkinsApi.Infrastructure.Persistence.Models
{
    public class OwnerModelConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            //table name
            builder.ToTable("owners");

            //primary key
            builder.HasKey(u => u.Id);

            //index
            builder.HasIndex(u => u.Email)
                .IsUnique();

            //properties
            builder.Property(owner => owner.Id)
                .HasColumnName("id")
                .HasConversion(id => id.Value,
                value => new UniqueIdentity(value));

            builder.Property(owner => owner.Email)
                .HasMaxLength(Email.MaxLength)
                .HasColumnName("email")
                .IsRequired()
                .HasConversion(
                    v => v.Value,
                    v => Email.Create(v)
                );

            builder.Property(owner => owner.CreatedOn)
                .HasColumnName("created_on")
                .IsRequired();

            builder.Property(owner => owner.LastModifiedOn)
                .HasColumnName("last_modified_on");

            //seed data
            builder.HasData(SeedHelper.LoadOwners());
        }
    }
}