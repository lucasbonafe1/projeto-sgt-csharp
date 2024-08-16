using RabbitMQ.Client;
using SGT.Application.DTOs.Users;
using System;
using System.Text;

namespace SGT.Infrastructure.Messaging.Producers.User
{
    public class UserProducer : IUserProducer
    {
        private readonly IRabbitMQProducer _rabbitMqProducer;
        public UserProducer(IRabbitMQProducer rabbitMqProducer)
        {
            _rabbitMqProducer = rabbitMqProducer;
        }

        public void CreateAccountMessage(UserResponseDTO user)
        {
            var message = $"Olá, {user.Name}. \nSua conta foi criada com sucesso!";
            _rabbitMqProducer.SendMessage(message, "user-queue");
        }

        public void UpdatedAccountMessage(UserUpdateDTO user)
        {
            var message = $"Informações atualizadas com sucesso!";
            _rabbitMqProducer.SendMessage(message, "user-queue");
        }

        public void GetTimeTask(UserResponseDTO user)
        {
            string message;

            if (user.Tasks == null || user.Tasks.Count == 0)
            {
                message = $"Olá {user.Name}, Você ainda não tem tarefas alistadas.";
            }
            else
            {
                var taskDetails = user.Tasks
                    .Select(task => $"Título: {task.Title}, Data de Início: {task.StartDate}, Data de Término: {task.EndDate}")
                    .Aggregate((current, next) => current + "\n" + next);

                message = $"Tarefas pendentes: \n{taskDetails}";
            }

            _rabbitMqProducer.SendMessage(message, "user-queue");
        }

    }
}
