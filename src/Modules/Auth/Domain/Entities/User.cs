namespace OpenSkinsApi.Modules.Auth.Domain.Entities
{
    using OpenSkinsApi.Domain;
    using OpenSkinsApi.Modules.Auth.Domain.Events.UserCreated;
    using OpenSkinsApi.Modules.Auth.Domain.ValueObjects;
    using Serilog;

    using System;

    public class User : AggregateRoot, IAuditableEntity
    {

        public Email Email { get; private set; }
        public Name Name { get; private set; }
        public UserName UserName { get; private set; }
        public Password? Password { get; private set; }
        public bool Verified { get; private set; } = false;
        private readonly List<RefreshToken> _refreshTokens = new();
        public IReadOnlyList<RefreshToken> RefreshTokens => _refreshTokens.ToList().AsReadOnly();
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        //Ef Core constructor
        private User() : base(null)
        {
            Email = null!;
            Name = null!;
            UserName = null!;
            Password = null!;
        }
        private User(
            UniqueIdentity? id,
            Email email,
            Name name,
            UserName userName
        ) : base(id)
        {
            Email = email;
            Name = name;
            UserName = userName;
        }

        public static User Create(UniqueIdentity? id, Email email, Name name, UserName userName)
        {
            User user = new(id, email, name, userName);

            if (id is null)
            {
                user.AddDomainEvent(new UserCreatedDomainEvent(
                    user.Id,
                    user.UserName.Value,
                    user.Name.FirstName,
                    user.Name.LastName
                ));
                Log.Information("New User created: {@user}", user.Id.Value.ToString());
            }

            return user;
        }

        public void AddRefreshToken(string refreshToken)
        {
            var token = RefreshToken.Create(
                null,
                this,
                refreshToken
            );

            _refreshTokens.Add(token);
        }

        public void SetVerified()
        {
            Verified = true;
        }

        public void SetPassword(Password password)
        {
            Password = password;
        }
    }
}