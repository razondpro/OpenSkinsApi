
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenSkinsApi.Domain;
using OpenSkinsApi.Modules.Auth.Domain.Entities;

namespace AuthService.Infrastructure.Persistence.Models
{
    public class AuthRefreshTokenModelConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            //table name
            builder.ToTable("auth_refresh_tokens");

            //primary key
            builder.HasKey(refreshToken => refreshToken.Id);

            //indexes
            builder.HasIndex(refreshToken => refreshToken.Token)
                .IsUnique();

            //properties
            builder.Property(refreshToken => refreshToken.Id)
                .HasColumnName("id")
                .HasConversion(id => id.Value,
                    value => new UniqueIdentity(value));

            builder.Property(refreshToken => refreshToken.Token)
                .HasColumnName("token")
                .IsRequired();

            builder.Property(refreshToken => refreshToken.IsUsed)
                .HasColumnName("is_used");

            builder.Property(refreshToken => refreshToken.IsRevoked)
                .HasColumnName("is_revoked");

            builder.Property(refreshToken => refreshToken.ExpiresOn)
                .HasColumnName("expires_on")
                .IsRequired();

            builder.Property(refreshToken => refreshToken.CreatedOn)
                .HasColumnName("created_on")
                .IsRequired();

            builder.Property(refreshToken => refreshToken.LastModifiedOn)
                .HasColumnName("last_modified_on");

            //relationships
            builder.HasOne(refreshToken => refreshToken.User)
                .WithMany(user => user.RefreshTokens)
                .HasForeignKey("user_id")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}