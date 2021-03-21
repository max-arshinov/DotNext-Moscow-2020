using System.Collections.Generic;
using System.Linq;
using System.Text;
using Force.Cqrs;
using Force.Ddd.DomainEvents;
using HightechAngular.DomainEvents;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Infrastructure.Extensions
{
    public class RabbitDomainEventDispatcher : IHandler<IEnumerable<IDomainEvent>>
    {
        public const string ExchangeName = "domain-events";
        
        public void Handle(IEnumerable<IDomainEvent> input)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Fanout);
                var body =  GetBody(input);
                
                channel.BasicPublish(exchange: ExchangeName,
                    routingKey: "",
                    basicProperties: null,
                    body: body);
            }
        }

        private static byte[] GetBody(IEnumerable<IDomainEvent> input)
        {
            var messages = input
                .Select(x => new DomainEventMessage(x))    
                .ToArray();
            
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messages));
        }
    }
}