namespace OpenSkinsApi.Modules.Skins.Domain.Entities
{
    using OpenSkinsApi.Domain;
    using OpenSkinsApi.Modules.Skins.Domain.Enums;
    using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;
    public class Skin : AggregateRoot, IAuditableEntity
    {
        public Name Name { get; private set; }
        public Type Type { get; private set; }
        public Color Color { get; private set; }
        public Money Price { get; private set; }
        public bool IsAvailable { get; private set; }
        private readonly List<Purchase> _purchases = new();
        public IReadOnlyList<Purchase> Purchases => _purchases.ToList().AsReadOnly();
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        //EF Constructor
        private Skin() : base(null)
        {
            Name = null!;
            Price = null!;
        }
        private Skin(UniqueIdentity? id, Name name, Money price, Type type, Color color) : base(id)
        {
            Name = name;
            Price = price;
            Type = type;
            Color = color;
        }

        public static Skin Create(UniqueIdentity? id, Name name, Money price, Type type, Color color)
        {
            return new Skin(id, name, price, type, color);
        }

        public void MakeItUnavailable()
        {
            IsAvailable = false;
        }

        public void MakeItAvailable()
        {
            IsAvailable = true;
        }

        public void Buy(Owner owner)
        {
            _purchases.Add(Purchase.Create(owner, this));
        }

    }
}