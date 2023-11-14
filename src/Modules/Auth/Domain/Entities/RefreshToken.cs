using OpenSkinsApi.Modules.Auth.Domain.Exceptions;
using OpenSkinsApi.Domain;

namespace OpenSkinsApi.Modules.Auth.Domain.Entities
{
    public class RefreshToken : Entity, IAuditableEntity
    {
        public string Token { get; private set; }
        public bool IsUsed { get; private set; }
        public bool IsRevoked { get; private set; }
        public User User { get; private set; }
        public DateTime ExpiresOn { get; private set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        //Ef Core constructor
        private RefreshToken() : base(null)
        {
            Token = null!;
            User = null!;
        }
        private RefreshToken(UniqueIdentity? id, User user, string token) : base(id)
        {
            Token = token;
            User = user;
            IsUsed = false;
            IsRevoked = false;
            ExpiresOn = DateTime.UtcNow.AddDays(28);
        }

        public static RefreshToken Create(UniqueIdentity? id, User user, string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new InvalidRefreshTokenException("Refresh token cannot be empty");
            }
            return new RefreshToken(id, user, token);
        }
        public void MarkAsUsed()
        {
            IsUsed = true;
        }

        public void MarkAsRevoked()
        {
            IsRevoked = true;
        }

        public bool IsExpired()
        {
            return DateTime.UtcNow >= ExpiresOn;
        }

        public bool IsActive()
        {
            return !IsUsed && !IsRevoked && !IsExpired();
        }
    }
}