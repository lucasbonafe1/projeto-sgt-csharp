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
            String message = $"Tarefas pendentes: \n{user.Tasks}";

            if (user.Tasks == null)
            {
                message = $"Olá {user.Name}, Você ainda não tem tarefas alistadas. ";
            }

            _rabbitMqProducer.SendMessage(message, "user-queue");
        }
    }
}
