using OpenSkinsApi.Domain;
using OpenSkinsApi.Modules.Skins.Domain.Enums;
using OpenSkinsApi.Shared.Domain;

namespace OpenSkinsApi.Modules.Skins.Domain.Entities
{
    public class SkinOwner : Entity, IAuditableEntity, ISoftDeletableEntity
    {
        public UniqueIdentity OwnerId { get; private set; }
        public UniqueIdentity SkinId { get; private set; }
        public Owner Owner { get; private set; }
        public Skin Skin { get; private set; }
        public Color Color { get; private set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public DateTime? DeletedAt { get; private set; }

        // EF Constructor
        private SkinOwner() : base(null)
        {
            OwnerId = null!;
            SkinId = null!;
            Owner = null!;
            Skin = null!;
        }

        private SkinOwner(Owner owner, Skin skin) : base(null)
        {
            OwnerId = owner.Id;
            SkinId = skin.Id;
            Owner = owner;
            Skin = skin;
        }

        public static SkinOwner Create(Owner owner, Skin skin)
        {
            return new SkinOwner(owner, skin);
        }

        public void ChangeColor(Color newColor)
        {
            Color = newColor;
        }

        public void SoftDelete()
        {
            DeletedAt = DateTime.UtcNow;
        }

        public bool IsSoftDeleted()
        {
            return DeletedAt != null;
        }
    }
}