namespace OpenSkinsApi.Domain.Events
{
    using MediatR;
    using OpenSkinsApi.Domain;

    public interface IDomainEvent : INotification
    {
        UniqueIdentity Id { get; }
        DateTime Timestamp { get; }
        UniqueIdentity AggregateId { get; }

    }
}