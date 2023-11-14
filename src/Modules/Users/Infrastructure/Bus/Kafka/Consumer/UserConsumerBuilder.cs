using KafkaFlow;
using KafkaFlow.Configuration;
using KafkaFlow.TypedHandler;
using OpenSkinsApi.Modules.Users.Infrastructure.Bus.Kafka.Consumer.Handlers;
using Serilog;

namespace OpenSkinsApi.Modules.Users.Infrastructure.Bus.Kafka.Consumer
{
    public static class UserConsumerBuilder
    {
        public static void Build(this IConsumerConfigurationBuilder builder)
        {
            var consumerName = "user-consumer";
            var consumingTopics = new string[] { "auth" };
            var consumerGroupId = "users-group";

            builder.Topics(consumingTopics);
            builder.WithName(consumerName);
            builder.WithGroupId(consumerGroupId);
            builder.WithBufferSize(100);
            builder.WithWorkersCount(3);
            builder.WithAutoOffsetReset(AutoOffsetReset.Earliest);
            builder.WithManualStoreOffsets();
            builder.AddMiddlewares(middlewares => middlewares
                .AddSchemaRegistryAvroSerializer()
                .AddTypedHandlers(handlers => handlers
                    .WithHandlerLifetime(InstanceLifetime.Transient)
                    .AddHandler<UserCreatedConsumerEventHandler>()
                )
            );
        }
    }
}