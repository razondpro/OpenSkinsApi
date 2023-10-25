namespace OpenSkinsApi.Domain
{
    public class AggregateRoot : Entity
    {
        protected AggregateRoot(UniqueIdentity? id) : base(id)
        {
        }
    }
}