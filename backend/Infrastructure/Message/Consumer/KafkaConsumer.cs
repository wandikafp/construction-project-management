using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;
using Domain.Entities;
using Infrastructure.ValueObject;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nest;
using static Confluent.Kafka.ConfigPropertyNames;

namespace Infrastructure.Message.Consumer
{
    public class KafkaConsumer : BackgroundService
    {
        private readonly ILogger<KafkaConsumer> _logger;
        private readonly IConsumer<string, string> _consumer;
        private readonly ElasticClient _elasticClient;

        public KafkaConsumer(ILogger<KafkaConsumer> logger, ElasticClient elasticClient)
        {
            _logger = logger;
            _elasticClient = elasticClient;
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "es.construction.hbx",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<string, string>(config).Build();
            _consumer.Subscribe("es.construction.hbx");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _ = Task.Run(() => StartKafkaListener(stoppingToken), stoppingToken); // Run separately
            return Task.CompletedTask;
        }

        private void StartKafkaListener(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = _consumer.Consume(stoppingToken);
                        _logger.LogInformation($"Received message: {consumeResult.Message.Value}");
                        // Process message asynchronously
                        _ = Task.Run(() => ProcessMessageAsync(consumeResult.Message.Value), stoppingToken);
                    }
                    catch (OperationCanceledException)
                    {
                        _logger.LogInformation("Kafka Consumer is stopping...");
                        break;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error while consuming message.");
                    }
                }
            }
            finally
            {
                _consumer.Close();
            }
        }

        private async Task ProcessMessageAsync(string message)
        {
            var messageObject = JsonSerializer.Deserialize<MessageObject>(message);
            _logger.LogInformation($"Processing: {messageObject.action}");
            switch(messageObject.action)
            {
                case "create":
                    _logger.LogInformation($"Creating: {messageObject.project.ToString()}");
                    _elasticClient.Index<ConstructionProject>(messageObject.project, i => i.Id(messageObject.project.ProjectId));
                    break;
                case "update":
                    _logger.LogInformation($"Updating: {messageObject.project.ToString()}");
                    _elasticClient.Index<ConstructionProject>(messageObject.project, i => i.Id(messageObject.project.ProjectId));
                    break;
                case "delete":
                    _logger.LogInformation($"Deleting: {messageObject.id.ToString()}");
                    _elasticClient.Delete<ConstructionProject>(messageObject.id);
                    break;
                default:
                    _logger.LogWarning($"Unknown action: {messageObject.action}");
                    break;
            }

            _logger.LogInformation($"Processed: {message}");
        }
    }
}
