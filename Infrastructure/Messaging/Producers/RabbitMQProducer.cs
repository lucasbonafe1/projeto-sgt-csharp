using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using SGT.Infrastructure.Messaging.ConfigMQ;
using System.Text;

namespace SGT.Infrastructure.Messaging.Producers
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        private readonly RabbitMQSettings _settings;

        public RabbitMQProducer(IOptions<RabbitMQSettings> options)
        {
            _settings = options.Value;
        }

        public void SendMessage(string message, string queueName)
        {
            var factory = new ConnectionFactory() { HostName = _settings.Host, UserName = _settings.UserName, Password = _settings.Password };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            }
        }
    }
}
