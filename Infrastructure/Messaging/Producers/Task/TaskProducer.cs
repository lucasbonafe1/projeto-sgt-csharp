using SGT.Application.DTOs.Tasks;
using RabbitMQ.Client;
using System;
using System.Text;

namespace SGT.Infrastructure.Messaging.Producers.Task
{
    public class TaskProducer : ITaskProducer
    {
        private readonly IRabbitMQProducer _rabbitMQProducer;

        public TaskProducer(IRabbitMQProducer rabbitMQProducer)
        {
            _rabbitMQProducer = rabbitMQProducer;
        }

        public void SendTaskMessage(TaskResponseDTO taskCreated)
        {
            var message = $"TAREFA CRIADA: {taskCreated.Title} \n {taskCreated.StartDate} à {taskCreated.EndDate}";
            _rabbitMQProducer.SendMessage(message, "task-queue");
        }

        public void UpdatedTaskMessage(TaskUpdateDTO taskUpdated)
        {
            var message = "TAREFA ATUALIZADA COM SUCESSO.";
            _rabbitMQProducer.SendMessage(message, "task-queue");
        }
    }
}
