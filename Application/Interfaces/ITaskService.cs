using SGT.Application.DTOs.Tasks;

namespace SGT.Application.Interfaces
{
    public interface ITaskService
    {
        Task<TaskResponseDTO> GetTaskByIdAsync(int id);
        Task<IEnumerable<TaskResponseDTO>> GetAllTasksAsync();
        Task<IEnumerable<TaskResponseDTO>> GetTasksByUserIdAsync(int id);
        Task<TaskResponseDTO> AddTaskAsync(TaskRequestDTO task);
        Task UpdateTaskAsync(TaskUpdateDTO task, int id);
        Task<bool> DeleteTaskAsync(int id);
    }
}
