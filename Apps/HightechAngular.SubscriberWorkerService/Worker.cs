using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Force.Cqrs;
using Force.Ddd.DomainEvents;
using HightechAngular.DomainEvents;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HightechAngular.SubscriberWorkerService
{
    public class Worker : BackgroundService
    {
        const string ExchangeName = "domain-events";
            
        private readonly ILogger<Worker> _logger;
        private readonly IHandler<IEnumerable<IDomainEvent>> _domainEventDispatcher;

        public Worker(ILogger<Worker> logger, IHandler<IEnumerable<IDomainEvent>> domainEventDispatcher)
        {
            _logger = logger;
            _domainEventDispatcher = domainEventDispatcher;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Fanout);

                var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName,
                    exchange: ExchangeName,
                    routingKey: "");

                Console.WriteLine(" [*] Waiting for logs.");

                var consumer = new EventingBasicConsumer(channel);
                
                consumer.Received += (model, ea) =>
                {
                    var message = Deserialize(ea);
                    Dispatch(message);
                };
                channel.BasicConsume(queue: queueName,
                    autoAck: true,
                    consumer: consumer);
            }
            
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
        
        private static DomainEventMessage[] Deserialize(BasicDeliverEventArgs input)
        {
            var body = input.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            
            return JsonConvert.DeserializeObject<DomainEventMessage[]>(message);
        }

        private void Dispatch(dynamic message)
        {
            // TODO: здесь диспетчеризация
        }
    }
}