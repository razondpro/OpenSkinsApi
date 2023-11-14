namespace OpenSkinsApi.Infrastructure.Persistence.Core.UnitOfWork
{
    using Newtonsoft.Json;
    using OpenSkinsApi.Domain;
    using OpenSkinsApi.Infrastructure.Persistence;
    using OpenSkinsApi.Infrastructure.Persistence.Core.Outbox;

    public abstract class UnitOfWork<TContext> : IUnitOfWork where TContext : Database
    {
        private readonly TContext _dbContext;
        public UnitOfWork(TContext database)
        {
            _dbContext = database;
        }
        public virtual async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            ConvertDomainEventsToOutboxMessage();
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private void ConvertDomainEventsToOutboxMessage()
        {
            var outboxMessages = _dbContext.ChangeTracker
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

            _dbContext.OutboxMessages.AddRange(outboxMessages);
        }
    }
}