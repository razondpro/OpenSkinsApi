using OpenSkinsApi.Domain.Events;
using OpenSkinsApi.Modules.Auth.Domain.Events.UserCreated;
using OpenSkinsApi.Infrastructure.Bus.Kafka.Events.Schemas.Auth.UserCreated;
using Serilog;

namespace OpenSkinsApi.Modules.Auth.Infrastructure.Bus.Kafka.Producer.Handlers
{
    public sealed class UserCreatedDomainEventHandler : IDomainEventHandler<UserCreatedDomainEvent>
    {
        private readonly AuthProducer _kafkaProducer;
        public UserCreatedDomainEventHandler(AuthProducer kafkaProducer)
        {
            _kafkaProducer = kafkaProducer;
        }
        public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var aggregateId = notification.AggregateId.Value.ToString();
            var message = new UserCreated
            {
                UserId = aggregateId,
                UserName = notification.UserName,
                FirstName = notification.FirstName,
                LastName = notification.LastName
            };

            //we pass aggregateId as key to make sure that all events for the same aggregate will be in the same partition
            await _kafkaProducer.ProduceAsync(aggregateId, message);

            Log.Information("UserCreatedDomainEvent with aggregateId: {@aggregateId} was sent to Kafka", aggregateId);
        }
    }
}