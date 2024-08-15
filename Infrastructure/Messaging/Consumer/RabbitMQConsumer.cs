using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using SGT.Infrastructure.Messaging.ConfigMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGT.Infrastructure.Messaging.Consumer
{
    public class RabbitMQConsumer
    {
        private readonly RabbitMQSettings _settings;

        public RabbitMQConsumer(IOptions<RabbitMQSettings> options)
        {
            _settings = options.Value;
        }

        public void StartConsuming()
        {
            var factory = new ConnectionFactory() { HostName = _settings.Host, UserName = _settings.UserName, Password = _settings.Password };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "demo-queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received message: {message}");
            };

            channel.BasicConsume(queue: "demo-queue", autoAck: true, consumer: consumer);
        }
    }

}
