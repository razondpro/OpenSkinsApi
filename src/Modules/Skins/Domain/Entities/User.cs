using OpenSkinsApi.Domain;
using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;

namespace OpenSkinsApi.Modules.Skins.Domain.Entities
{
    public class User : Entity, IAuditableEntity
    {
        public Email Email { get; private set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        // EF Constructor
        private User() : base(null)
        {
            Email = null!;
        }
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