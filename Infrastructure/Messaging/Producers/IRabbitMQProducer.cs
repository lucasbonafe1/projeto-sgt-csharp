
namespace SGT.Infrastructure.Messaging.Producers
{
    public interface IRabbitMQProducer
    {
        void SendMessage(string message, string queueName);
    }
}
