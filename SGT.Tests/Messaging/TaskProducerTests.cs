using Moq;
using SGT.Application.DTOs.Tasks;
using SGT.Infrastructure.Messaging.Producers.Task;
using SGT.Infrastructure.Messaging.Producers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGT.Tests.Messaging
{
    public class TaskProducerTests
    {
        private readonly Mock<IRabbitMQProducer> _mockRabbitMQProducer;
        private readonly TaskProducer _taskProducer;

        public TaskProducerTests()
        {
            _mockRabbitMQProducer = new Mock<IRabbitMQProducer>();
            _taskProducer = new TaskProducer(_mockRabbitMQProducer.Object);
        }

        [Fact]
        public void SendTaskMessage_DeveMandarMensagemComInformacoesDaTarefaPraQueue()
        {
           
            var taskResponseDTO = new TaskResponseDTO
            {
                Title = "Test Task",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(1)
            };

            var expectedMessage = $"TAREFA CRIADA: {taskResponseDTO.Title} \n {taskResponseDTO.StartDate} à {taskResponseDTO.EndDate}";
           
            _taskProducer.SendTaskMessage(taskResponseDTO);

            _mockRabbitMQProducer
                .Verify(p => p.SendMessage(expectedMessage, "task-queue"), Times.Once);
        }

        [Fact]
        public void UpdatedTaskMessage_DeveMandarAMensagemDeAtualizacaoParaQueue()
        {
            var taskUpdateDTO = new TaskUpdateDTO();
            var expectedMessage = "TAREFA ATUALIZADA COM SUCESSO.";

            _taskProducer.UpdatedTaskMessage(taskUpdateDTO);

            _mockRabbitMQProducer
                .Verify(p => p.SendMessage(expectedMessage, "task-queue"), Times.Once);
        }
    }
}
