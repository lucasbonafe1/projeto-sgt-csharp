using SGT.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGT.Application.Interfaces
{
    public interface ITaskService
    {
        Task<TaskResponseDTO> GetTaskByIdAsync(int id);
        Task<IEnumerable<TaskResponseDTO>> GetAllTasksAsync();
        Task AddTaskAsync(TaskResponseDTO task);
        Task UpdateTaskAsync(TaskResponseDTO task);
        Task DeleteTaskAsync(int id);
    }
}
