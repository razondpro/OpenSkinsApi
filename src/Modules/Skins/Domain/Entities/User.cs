using OpenSkinsApi.Domain;
using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;

namespace OpenSkinsApi.Modules.Skins.Domain.Entities
{
    public class User : Entity
    {
        public Email Email { get; private set; }
        private User(UniqueIdentity? id, Email email) : base(id)
        {
            Email = email;
        }

        public static User Create(UniqueIdentity? id, Email email)
        {
            return new User(id, email);
        }
    }
}