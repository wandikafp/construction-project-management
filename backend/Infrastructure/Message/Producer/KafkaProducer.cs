using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;
using Infrastructure.ValueObject;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Message.Producer
{
    public class KafkaProducer
    {
        private readonly ILogger<KafkaProducer> _logger;

        public KafkaProducer(ILogger<KafkaProducer> logger)
        {
            _logger = logger;
        }

        public async Task ProduceAsync(MessageObject message, CancellationToken cancellationToken)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            using var producer = new ProducerBuilder<Null, string>(config).Build();

            try
            {
                var deliveryResult = await producer.ProduceAsync("es.construction.hbx", new Message<Null, string>
                {
                    Value = JsonSerializer.Serialize(message)
                }, cancellationToken);

                _logger.LogInformation($"Delivered message to {deliveryResult.TopicPartitionOffset}");
            }
            catch (ProduceException<Null, string> e)
            {
                _logger.LogError($"Delivery failed: {e.Error.Reason}");
            }

            producer.Flush(TimeSpan.FromSeconds(10));
        }
    }
}
