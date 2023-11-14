namespace UserService.Shared.Infrastructure.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OpenSkinsApi.Domain;
    using OpenSkinsApi.Modules.Users.Domain.Entities;
    using OpenSkinsApi.Modules.Users.Domain.ValueObjects;

    public class UserModelConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // table name
            builder.ToTable("users_users");

            // primay key
            builder.HasKey(user => user.Id);
            //index
            builder.HasIndex(user => user.UserName)
                .IsUnique();
            // properties
            builder.Property(user => user.Id)
                .HasColumnName("id")
                .HasConversion(id => id.Value,
                value => new UniqueIdentity(value));

            builder.Property(user => user.UserName)
                .HasMaxLength(UserName.MaxLength)
                .HasColumnName("user_name")
                .IsRequired()
                .HasConversion(userName => userName.Value,
                value => UserName.Create(value));

            builder.OwnsOne(user => user.Name, name =>
            {
                name.Property(n => n.FirstName)
                    .HasMaxLength(Name.MaxLength)
                    .HasColumnName("first_name")
                    .IsRequired();

                name.Property(n => n.LastName)
                    .HasMaxLength(Name.MaxLength)
                    .HasColumnName("last_name");
            });

            builder.Property(user => user.CreatedOn)
                .HasColumnName("created_on")
                .IsRequired();

            builder.Property(user => user.LastModifiedOn)
                .HasColumnName("last_modified_on");
        }
    }
}