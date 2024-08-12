using SGT.Application.DTOs;
using SGT.Application.Interfaces;
using SGT.Domain.Repositories;

namespace SGT.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskrepository)
        {
            _taskRepository = taskrepository;
        }

        public Task AddTaskAsync(TaskResponseDTO task)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTaskAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskResponseDTO>> GetAllTasksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TaskResponseDTO> GetTaskByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTaskAsync(TaskResponseDTO task)
        {
            throw new NotImplementedException();
        }
    }
}
