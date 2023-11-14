using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using KafkaFlow;
using KafkaFlow.Configuration;

namespace OpenSkinsApi.Modules.Auth.Infrastructure.Bus.Kafka.Producer
{
    public static class AuthProducerBuilder
    {
        public static void Build(this IProducerConfigurationBuilder builder)
        {
            const string producerTopic = "auth";

            builder.DefaultTopic(producerTopic);
            builder.WithAcks(Acks.All);
            builder.AddMiddlewares(middlewares =>
            {
                middlewares.AddSchemaRegistryAvroSerializer(
                    new AvroSerializerConfig
                    {
                        SubjectNameStrategy = SubjectNameStrategy.TopicRecord,
                        AutoRegisterSchemas = true
                    }
                );
            });
        }
    }
}