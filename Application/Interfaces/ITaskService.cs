using SGT.Application.DTOs;

namespace SGT.Application.Interfaces
{
    public interface ITaskService
    {
        Task<TaskResponseDTO> GetTaskByIdAsync(int id);
        Task<IEnumerable<TaskResponseDTO>> GetAllTasksAsync();
        Task<IEnumerable<TaskResponseDTO>> GetTasksByUserIdAsync(int id);
        Task<TaskRequestDTO> AddTaskAsync(TaskRequestDTO task);
        Task UpdateTaskAsync(TaskRequestDTO task);
        Task DeleteTaskAsync(int id);
    }
}
