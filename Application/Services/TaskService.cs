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

        public async Task<TaskRequestDTO> AddTaskAsync(TaskRequestDTO taskRequestDTO)
        {
            // add validação para se caso alguma informação for null

            TaskEntity task = new TaskEntity(taskRequestDTO.Title,
                                 taskRequestDTO.Description,
                                 taskRequestDTO.DurationInDays,
                                 taskRequestDTO.StartDate, // fazer com que busque a data atual
                                 taskRequestDTO.EndDate,
                                 taskRequestDTO.Status,
                                 taskRequestDTO.UserId);

            var taskCreated = await _taskRepository.Add(task);

            TaskRequestDTO taskConverted = new TaskRequestDTO(taskCreated);

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
                Status = task.Status,
                UserId = task.UserId
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

        public async Task<IEnumerable<TaskResponseDTO>> GetTasksByUserIdAsync(int id)
        {
            IEnumerable<TaskEntity?> tasks = await _taskRepository.GetTasksByUserId(id);

            var tasksConverted = tasks.Select(task => new TaskResponseDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DurationInDays = task.DurationInDays,
                StartDate = task.StartDate,
                EndDate = task.EndDate,
                Status = task.Status,
                UserId = task.UserId
            });

            return tasksConverted;
        }

        public async Task UpdateTaskAsync(TaskRequestDTO taskDTO)
        {
            var existingTask = await _taskRepository.GetById(taskDTO.Id);

            if (existingTask == null)
            {
                throw new ApplicationException("User não encontrado.");
            }

            // atualiza os campos somente se forem passados no DTO
            existingTask.Title = !string.IsNullOrWhiteSpace(taskDTO.Title) ? taskDTO.Title : existingTask.Title;
            existingTask.Description = !string.IsNullOrWhiteSpace(taskDTO.Description) ? taskDTO.Description : existingTask.Description;
            existingTask.DurationInDays = taskDTO.DurationInDays != default ? taskDTO.DurationInDays : existingTask.DurationInDays;
            existingTask.StartDate = taskDTO.StartDate != default ? taskDTO.StartDate : existingTask.StartDate;
            existingTask.EndDate = taskDTO.EndDate != default ? taskDTO.EndDate : existingTask.EndDate;
            existingTask.Status = taskDTO.Status != default ? taskDTO.Status : existingTask.Status;

            await _taskRepository.Update(existingTask, taskDTO.Id);
        }

        public Task DeleteTaskAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
