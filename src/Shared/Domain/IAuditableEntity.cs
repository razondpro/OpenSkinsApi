namespace OpenSkinsApi.Domain
{
    public interface IAuditableEntity
    {
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}