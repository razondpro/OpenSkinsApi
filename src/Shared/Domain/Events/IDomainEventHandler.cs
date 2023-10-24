using MediatR;

namespace OpenSkinsApi.Domain.Events
{
    public interface IDomainEventHandler<T> : INotificationHandler<T> where T : DomainEvent
    {

    }
}