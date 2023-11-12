namespace OpenSkinsApi.Infrastructure.Idempotence
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using OpenSkinsApi.Domain.Events;
    using OpenSkinsApi.Infrastructure.Persistence;
    using OpenSkinsApi.Infrastructure.Persistence.Core.Outbox;

    public sealed class IdempotentDomainEventHandler<T> : IDomainEventHandler<T>
        where T : DomainEvent
    {
        private readonly INotificationHandler<T> _handler;
        private readonly Database _database;


        public IdempotentDomainEventHandler(INotificationHandler<T> handler, Database database)
        {
            _handler = handler;
            _database = database;
        }
        public async Task Handle(T notification, CancellationToken cancellationToken)
        {
            var anyRecordOfConsumer = await _database.OutboxMessageConsumers.AnyAsync(
                obc => obc.EventId == notification.Id.Value && obc.EventType == _handler.GetType().Name,
                cancellationToken
            );

            if (anyRecordOfConsumer)
            {
                return;
            }

            await _handler.Handle(notification, cancellationToken);

            await _database.OutboxMessageConsumers.AddAsync(
                new OutboxMessageConsumer
                {
                    Id = Guid.NewGuid(),
                    EventId = notification.Id.Value,
                    EventType = _handler.GetType().Name,
                    Timestamp = DateTime.UtcNow
                },
                cancellationToken
            );

            await _database.SaveChangesAsync(cancellationToken);
        }
    }
}
