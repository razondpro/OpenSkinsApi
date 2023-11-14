using KafkaFlow;
using OpenSkinsApi.Infrastructure.Bus.Kafka.Logs;
using OpenSkinsApi.Modules.Auth.Infrastructure.Bus.Kafka.Producer;
using OpenSkinsApi.Modules.Skins.Infrastructure.Bus.Kafka.Consumer;
using OpenSkinsApi.Modules.Users.Infrastructure.Bus.Kafka.Consumer;

namespace OpenSkinsApi.Config.Bus.Kafka
{
    public class KafkaServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<AuthProducer>();
            services.ConfigureOptions<KafkaOptionsSetup>();


            var kafkaServers = configuration["KAFKA_BOOTSTRAP_SERVERS"];
            var bootstrapServers = kafkaServers != null
                ? kafkaServers.Split(',')
                : new string[] { "localhost:9092" };
            var schemaRegistryUrl = configuration["KAFKA_SCHEMA_REGISTRY_URL"] ?? "http://localhost:8081";

            services.AddKafkaFlowHostedService(configuration =>
            {
                configuration.UseLogHandler<CustomLogHandler>();
                configuration.AddCluster(cluster =>
                {
                    cluster.WithBrokers(bootstrapServers);
                    cluster.WithSchemaRegistry(config => config.Url = schemaRegistryUrl);

                    cluster.AddConsumer(SkinConsumerBuilder.Build);
                    cluster.AddConsumer(UserConsumerBuilder.Build);

                    cluster.AddProducer<AuthProducer>(AuthProducerBuilder.Build);
                });
            });
        }
    }
}