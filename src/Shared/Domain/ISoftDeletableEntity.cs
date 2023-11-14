namespace OpenSkinsApi.Domain
{
    public interface ISoftDeletableEntity
    {
        DateTime? DeletedAt { get; }
        public bool IsSoftDeleted();
        public void SoftDelete();
    }
}