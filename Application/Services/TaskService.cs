﻿using SGT.Application.DTOs.Tasks;
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

        public async Task<TaskResponseDTO> AddTaskAsync(TaskRequestDTO taskRequestDTO)
        {
            if(taskRequestDTO == null)
            {
                throw new ArgumentNullException("Task não pode ser nula.");
            }

            TaskEntity task = new TaskEntity(taskRequestDTO.Title,
                                 taskRequestDTO.Description,
                                 taskRequestDTO.StartDate,
                                 taskRequestDTO.EndDate,
                                 taskRequestDTO.Status,
                                 taskRequestDTO.UserId);

            var taskCreated = await _taskRepository.Add(task);

            TaskResponseDTO taskConverted = new TaskResponseDTO(taskCreated);

            return taskConverted;

        }

        public async Task<IEnumerable<TaskResponseDTO>> GetAllTasksAsync()
        {
            IEnumerable<TaskEntity> tasks = await _taskRepository.GetAll();

            if (tasks == null)
            {
                throw new ApplicationException("Nenhuma tarefa encontrada.");
            }

            var tasksConverted = tasks.Select(task => new TaskResponseDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
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
            IEnumerable<TaskEntity> tasks = await _taskRepository.GetTasksByUserId(id);


            var tasksConverted = tasks.Select(task => new TaskResponseDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                StartDate = task.StartDate,
                EndDate = task.EndDate,
                Status = task.Status,
                UserId = task.UserId
            });

            return tasksConverted;
        }

        public async Task UpdateTaskAsync(TaskUpdateDTO taskDTO, int id)
        { 
            var existingTask = await _taskRepository.GetById(id);

            if (existingTask == null)
            {
                throw new ApplicationException("Task não encontrado.");
            }

            var taskProperties = typeof(TaskEntity).GetProperties();
            var dtoProperties = typeof(TaskUpdateDTO).GetProperties();

            foreach (var dtoProperty in dtoProperties)
            {
                var taskProperty = taskProperties.FirstOrDefault(p => p.Name == dtoProperty.Name && (p.PropertyType == dtoProperty.PropertyType ||( p.PropertyType == typeof(DateTime) && dtoProperty.PropertyType == typeof(DateTime?))));
                if (taskProperty != null && taskProperty.CanWrite)
                {
                    var value = dtoProperty.GetValue(taskDTO);
                    if (value != null)
                    {
                        if (taskProperty.PropertyType == typeof(DateTime) && value is DateTime?)
                        {
                            value = ((DateTime?)value).Value;
                        }
                        taskProperty.SetValue(existingTask, value);
                    }
                }
            }

            await _taskRepository.Update(existingTask, id);
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            return (id <= 0) ? throw new ApplicationException("Id inexistente.") : await _taskRepository.Delete(id);
        }
    }
}
