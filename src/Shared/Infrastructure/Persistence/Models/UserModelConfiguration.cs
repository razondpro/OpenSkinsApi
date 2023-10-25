using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenSkinsApi.Domain;
using OpenSkinsApi.Infrastructure.Persistence.Seed;
using OpenSkinsApi.Modules.Skins.Domain.Entities;
using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;

namespace OpenSkinsApi.Infrastructure.Persistence.Models
{
    public class UserModelConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //table name
            builder.ToTable("users");

            //primary key
            builder.HasKey(u => u.Id);

            //index
            builder.HasIndex(u => u.Email)
                .IsUnique();

            //properties
            builder.Property(user => user.Id)
                .HasColumnName("id")
                .HasConversion(id => id.Value,
                value => new UniqueIdentity(value));

            builder.Property(user => user.Email)
                .HasMaxLength(Email.MaxLength)
                .HasColumnName("email")
                .IsRequired()
                .HasConversion(
                    v => v.Value,
                    v => Email.Create(v)
                );

            builder.Property(user => user.CreatedOn)
                .HasColumnName("created_on")
                .IsRequired();

            builder.Property(user => user.LastModifiedOn)
                .HasColumnName("last_modified_on");

            //seed data
            builder.HasData(SeedHelper.LoadUsers());
        }
    }
}