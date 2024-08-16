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
    public class UserConsumer
    {
        private readonly IModel _channel;

        public UserConsumer(IModel channel)
        {
            _channel = channel;
        }

        public void StartUserConsuming()
        {
            _channel.QueueDeclare(queue: "user-queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Mensagem recebida: {message}");
            };

            _channel.BasicConsume(queue: "user-queue", autoAck: true, consumer: consumer);
        }
    }

}
