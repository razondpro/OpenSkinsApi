using OpenSkinsApi.Domain;
using OpenSkinsApi.Modules.Skins.Domain.Enums;

namespace OpenSkinsApi.Modules.Skins.Domain.Entities
{
    public class Purchase : Entity, IAuditableEntity, ISoftDeletableEntity
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
        private Purchase() : base(null)
        {
            OwnerId = null!;
            SkinId = null!;
            Owner = null!;
            Skin = null!;
        }

        private Purchase(Owner owner, Skin skin) : base(null)
        {
            OwnerId = owner.Id;
            SkinId = skin.Id;
            Owner = owner;
            Skin = skin;
        }

        public static Purchase Create(Owner owner, Skin skin)
        {
            return new Purchase(owner, skin);
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