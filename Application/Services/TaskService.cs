using SGT.Application.DTOs;
using SGT.Application.Interfaces;
using SGT.Domain.Entities;
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

        public async Task<TaskResponseDTO> AddTaskAsync(TaskResponseDTO taskResponseDTO)
        {
            // add validação para se caso alguma informação for null

            TaskEntity task = new TaskEntity(taskResponseDTO.Title,
                                 taskResponseDTO.Description,
                                 taskResponseDTO.DurationInDays,
                                 taskResponseDTO.StartDate, // fazer com que busque a data atual
                                 taskResponseDTO.EndDate,
                                 taskResponseDTO.Status,
                                 new UserEntity { Id = taskResponseDTO.UserId });

            var taskCreated = await _taskRepository.Add(task);

            TaskResponseDTO taskConverted = new TaskResponseDTO(taskCreated);

            return taskConverted;

        }


        public async Task<IEnumerable<TaskResponseDTO>> GetAllTasksAsync()
        {
            IEnumerable<TaskEntity?> tasks = await _taskRepository.GetAll();

            if (tasks == null)
            {
                throw new ApplicationException("Nenhuma tarefa encontrada.");
            }

            var tasksConverted = tasks.Select(task => new TaskResponseDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DurationInDays = task.DurationInDays,
                StartDate = task.StartDate,
                EndDate = task.EndDate,
                Status = task.Status
            });

            return tasksConverted;
        }

        public async Task<TaskResponseDTO> GetTaskByIdAsync(int id)
        {
            TaskEntity? task = await _taskRepository.GetById(id);

            if (task == null)
            {
                throw new ApplicationException($"Nenhuma tarefa encontrada com o id {id}");
            }

            TaskResponseDTO taskConverted = new TaskResponseDTO(task);

            return taskConverted;
        }

        public Task UpdateTaskAsync(TaskResponseDTO task)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTaskAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
