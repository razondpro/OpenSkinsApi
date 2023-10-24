namespace OpenSkinsApi.Infrastructure.Persistence.Models
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OpenSkinsApi.Domain;
    using OpenSkinsApi.Infrastructure.Persistence.Seed;
    using OpenSkinsApi.Modules.Skins.Domain.Entities;
    using OpenSkinsApi.Modules.Skins.Domain.Enums;
    using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;

    public class SkinModelConfiguration : IEntityTypeConfiguration<Skin>
    {
        public void Configure(EntityTypeBuilder<Skin> builder)
        {
            //table name
            builder.ToTable("skins");

            //primary key
            builder.HasKey(s => s.Id);

            //properties
            builder.Property(s => s.Id)
                .HasColumnName("id")
                .HasConversion(id => id.Value,
                value => new UniqueIdentity(value));

            builder.Property(s => s.Name)
                .HasMaxLength(Name.MaxLength)
                .HasColumnName("name")
                .IsRequired()
                .HasConversion(
                    v => v.Value,
                    v => Name.Create(v)
                );

            builder.Property(s => s.Price)
                .HasColumnName("price")
                .IsRequired()
                .HasConversion(
                    v => v.Amount,
                    v => Money.Create(v)
                );

            builder.Property(s => s.Type)
                .HasColumnName("type")
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (Type)Enum.Parse(typeof(Type), v)
                );

            builder.Property(s => s.Color)
                .HasColumnName("color")
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (Color)Enum.Parse(typeof(Color), v)
                );

            builder.Property(s => s.IsAvailable)
                .HasColumnName("is_available")
                .IsRequired();

            builder.Property(s => s.CreatedOn)
                .HasColumnName("created_on")
                .IsRequired();

            builder.Property(s => s.LastModifiedOn)
                .HasColumnName("last_modified_on");

            //seed data
            builder.HasData(SeedHelper.LoadSkinsFromJsonFile());

        }
    }
}