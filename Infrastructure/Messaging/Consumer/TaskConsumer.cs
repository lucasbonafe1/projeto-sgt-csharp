using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace SGT.Infrastructure.Messaging.Consumer
{
    public class TaskConsumer
    {
        private readonly IModel _channel;

        public TaskConsumer(IModel channel)
        {
            _channel = channel;
        }

        public void StartTaskConsuming()
        {
            _channel.QueueDeclare(queue: "task-queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Informação de task recebida: {message}");
            };

            _channel.BasicConsume(queue: "task-queue", autoAck: true, consumer: consumer);
        }
    }
}
