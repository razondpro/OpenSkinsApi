namespace OpenSkinsApi.Infrastructure.Persistence.Core.UnitOfWork
{
    using Newtonsoft.Json;
    using OpenSkinsApi.Domain;
    using OpenSkinsApi.Infrastructure.Persistence;
    using OpenSkinsApi.Infrastructure.Persistence.Core.Outbox;

    public class UnitOfWork : IUnitOfWork
    {

        private readonly Database DbContext;

        public UnitOfWork(Database database)
        {
            DbContext = database;
        }
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            ConvertDomainEventsToOutboxMessage();
            await DbContext.SaveChangesAsync(cancellationToken);
        }

        private void ConvertDomainEventsToOutboxMessage()
        {

            var outboxMessages = DbContext.ChangeTracker
                .Entries<AggregateRoot>()
                .Select(x => x.Entity)
                .SelectMany(x =>
                {
                    var domainEvents = x.GetDomainEvents();
                    x.ClearDomainEvents();

                    return domainEvents;
                })
                .Select(domainEvent => new OutboxMessage(
                    domainEvent.GetType().Name,
                    JsonConvert.SerializeObject(domainEvent, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    })))
                .ToList();

            DbContext.OutboxMessages.AddRange(outboxMessages);
        }
    }
}