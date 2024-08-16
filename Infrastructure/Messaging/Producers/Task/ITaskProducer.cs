using SGT.Application.DTOs.Tasks;

namespace SGT.Infrastructure.Messaging.Producers.Task
{
    public interface ITaskProducer
    {
        void SendTaskMessage(TaskResponseDTO taskCreated);
        void UpdatedTaskMessage(TaskUpdateDTO taskCreated);
    }
}
