using KafkaFlow;
using KafkaFlow.Configuration;
using KafkaFlow.TypedHandler;

namespace OpenSkinsApi.Modules.Skins.Infrastructure.Bus.Kafka.Consumer
{
    public static class SkinConsumerBuilder
    {
        public static void Build(this IConsumerConfigurationBuilder builder)
        {
            var consumerName = "skin-consumer";
            var consumingTopics = new string[] { "auth" };
            var consumerGroupId = "skin-group";

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
                    // Transient needed because of mediatr(is transient by default)
                    .WithHandlerLifetime(InstanceLifetime.Transient)
                )
            );
        }
    }
}