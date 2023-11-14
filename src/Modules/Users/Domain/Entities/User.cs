namespace OpenSkinsApi.Modules.Users.Domain.Entities
{
    using OpenSkinsApi.Modules.Users.Domain.ValueObjects;
    using OpenSkinsApi.Domain;

    public class User : AggregateRoot, IAuditableEntity
    {
        public Name Name { get; private set; }
        public UserName UserName { get; private set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        //Ef Core constructor
        private User() : base(null)
        {
            Name = null!;
            UserName = null!;
        }
        private User(UniqueIdentity? id, UserName userName, Name name) : base(id)
        {
            Name = name;
            UserName = userName;
        }

        public static User Create(UniqueIdentity? id, UserName userName, Name name)
        {
            return new(id, userName, name);
        }

        public void UpdateName(Name name)
        {
            Name = name;
        }

    }
}