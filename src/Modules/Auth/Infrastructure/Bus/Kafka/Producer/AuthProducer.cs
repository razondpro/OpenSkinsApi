using Avro.Specific;
using KafkaFlow;
using OpenSkinsApi.Infrastructure.Bus.Kafka.Producer;

namespace OpenSkinsApi.Modules.Auth.Infrastructure.Bus.Kafka.Producer
{
    public class AuthProducer : IProducer
    {
        private readonly IMessageProducer<AuthProducer> _producer;

        public AuthProducer(IMessageProducer<AuthProducer> producer)
        {
            _producer = producer;
        }

        public async Task ProduceAsync<T>(string key, T message) where T : ISpecificRecord
        {
            await _producer.ProduceAsync(key, message);
        }
    }
}