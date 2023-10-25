using OpenSkinsApi.Domain;
using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;

namespace OpenSkinsApi.Modules.Skins.Domain.Entities
{
    public class Owner : Entity, IAuditableEntity
    {
        public Email Email { get; private set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        // EF Constructor
        private Owner() : base(null)
        {
            Email = null!;
        }
        private Owner(UniqueIdentity? id, Email email) : base(id)
        {
            Email = email;
        }

        public static Owner Create(UniqueIdentity? id, Email email)
        {
            return new Owner(id, email);
        }
    }
}