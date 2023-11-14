namespace AuthService.Infrastructure.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OpenSkinsApi.Domain;
    using OpenSkinsApi.Modules.Auth.Domain.Entities;
    using OpenSkinsApi.Modules.Auth.Domain.ValueObjects;

    public class AuthUserModelConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // table name
            builder.ToTable("auth_users");

            // primay key
            builder.HasKey(user => user.Id);
            //index
            builder.HasIndex(user => user.Email)
                .IsUnique();
            builder.HasIndex(user => user.UserName)
                .IsUnique();
            // properties
            builder.Property(user => user.Id)
                .HasColumnName("id")
                .HasConversion(id => id.Value,
                value => new UniqueIdentity(value));

            builder.Property(user => user.Email)
                .HasMaxLength(Email.MaxLength)
                .HasColumnName("email")
                .IsRequired()
                .HasConversion(email => email.Value,
                value => Email.Create(value));

            builder.Property(user => user.UserName)
                .HasMaxLength(UserName.MaxLength)
                .HasColumnName("user_name")
                .IsRequired()
                .HasConversion(userName => userName.Value,
                value => UserName.Create(value));

            // we do not want to save names in auth-service
            /*
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
            */
            //Ignore name
            builder.Ignore(user => user.Name);

            builder.OwnsOne(user => user.Password, password =>
            {
                password.Property(p => p.Salt)
                    .HasColumnName("password_salt")
                    .HasColumnType("blob");

                password.Property(p => p.Hash)
                    .HasColumnName("password_hash")
                    .HasColumnType("blob");
            });

            builder.Property(user => user.Verified)
                .HasColumnName("verified")
                .HasDefaultValue(false);

            builder.Property(user => user.CreatedOn)
                .HasColumnName("created_on")
                .IsRequired();

            builder.Property(user => user.LastModifiedOn)
                .HasColumnName("last_modified_on");
        }
    }
}