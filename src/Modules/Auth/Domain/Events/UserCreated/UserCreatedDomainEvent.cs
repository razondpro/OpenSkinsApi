namespace OpenSkinsApi.Modules.Auth.Domain.Events.UserCreated
{
    using OpenSkinsApi.Domain;
    using OpenSkinsApi.Domain.Events;

    public sealed record UserCreatedDomainEvent(
        UniqueIdentity AggregateId,
        string UserName,
        string FirstName,
        string? LastName
        ) : DomainEvent(new UniqueIdentity(null), DateTime.UtcNow, AggregateId)
    {

    }
}