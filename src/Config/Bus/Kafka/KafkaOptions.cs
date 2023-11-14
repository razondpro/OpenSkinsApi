namespace OpenSkinsApi.Config.Bus.Kafka
{
    public class KafkaOptions
    {
        public string[] BootstrapServers { get; set; } = Array.Empty<string>();
        public string SchemaRegistryUrl { get; set; } = string.Empty;

    }
}