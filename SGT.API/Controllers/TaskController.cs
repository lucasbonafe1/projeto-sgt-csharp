using Microsoft.AspNetCore.Mvc;
using SGT.Application.DTOs;
using SGT.Application.Interfaces;
using SGT.Domain.Entities;
using SGT.Domain.Repositories;

namespace SGT.API.Controllers
{
    [ApiController]
    [Route("tasks")]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost("create-task")]
        public async Task<IActionResult> Post([FromBody] TaskResponseDTO task)
        { 
            //TODO: adicionar validação para se Task for null

            var taskCreated = await _taskService.AddTaskAsync(task);

            return Ok(taskCreated);
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            IEnumerable<TaskResponseDTO> tasks = await _taskService.GetAllTasksAsync();

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            TaskResponseDTO task = await _taskService.GetTaskByIdAsync(id);

            return Ok(task);
        }
    }
}
