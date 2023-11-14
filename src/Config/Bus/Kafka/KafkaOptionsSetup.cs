using Microsoft.Extensions.Options;

namespace OpenSkinsApi.Config.Bus.Kafka
{
    public class KafkaOptionsSetup : IConfigureOptions<KafkaOptions>
    {
        private readonly IConfiguration _configuration;

        public KafkaOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Configure(KafkaOptions options)
        {
            var bootstrapServers = _configuration["KAFKA_BOOTSTRAP_SERVERS"];

            options.BootstrapServers = bootstrapServers != null
                ? bootstrapServers.Split(',')
                : new string[] { "localhost:9092" };
            options.SchemaRegistryUrl = _configuration["KAFKA_SCHEMA_REGISTRY_URL"] ?? "localhost:8081";
        }
    }
}