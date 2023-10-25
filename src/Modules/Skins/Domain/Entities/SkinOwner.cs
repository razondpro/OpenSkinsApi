using OpenSkinsApi.Domain;
using OpenSkinsApi.Modules.Skins.Domain.Enums;

namespace OpenSkinsApi.Modules.Skins.Domain.Entities
{
    public class SkinOwner : Entity, IAuditableEntity
    {
        public UniqueIdentity UserId { get; private set; }
        public UniqueIdentity SkinId { get; private set; }
        public User User { get; private set; }
        public Skin Skin { get; private set; }
        public Color Color { get; private set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        // EF Constructor
        private SkinOwner() : base(null)
        {
            UserId = null!;
            SkinId = null!;
            User = null!;
            Skin = null!;
        }

        private SkinOwner(User user, Skin skin) : base(null)
        {
            UserId = user.Id;
            SkinId = skin.Id;
            User = user;
            Skin = skin;
        }

        public static SkinOwner Create(User user, Skin skin)
        {
            return new SkinOwner(user, skin);
        }

        public void ChangeColor(Color newColor)
        {
            Color = newColor;
        }
    }
}