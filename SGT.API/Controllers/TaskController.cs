using Microsoft.AspNetCore.Mvc;
using SGT.Application.DTOs.Tasks;
using SGT.Application.Interfaces;
using SGT.Application.Services;

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
        public async Task<IActionResult> Post([FromBody] TaskRequestDTO task)
        { 
            var taskCreated = await _taskService.AddTaskAsync(task);

            if (taskCreated == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro na criação de tarefa.");
            }

            return Ok(taskCreated);
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            IEnumerable<TaskResponseDTO> tasks = await _taskService.GetAllTasksAsync();

            if (tasks == null || !tasks.Any())
            {
                return NotFound("Nenhuma tarefa encontrada.");
            }

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            TaskResponseDTO task = await _taskService.GetTaskByIdAsync(id);

            if (task == null)
            {
                return NotFound($"Nenhuma tarefa encontrada com id {id}");
            }

            return Ok(task);
        }

        [HttpGet("my-tasks/{id}")]
        public async Task<IActionResult> FindTasksByUserId(int id)
        {
            IEnumerable<TaskResponseDTO> tasks = await _taskService.GetTasksByUserIdAsync(id);

            if (tasks == null || !tasks.Any())
            {
                return NotFound("Você ainda não possui nenhuma tarefa.");
            }

            return Ok(tasks);
        }

        [HttpPut("update-task{id}")]
        public async Task<ActionResult> Put([FromBody] TaskUpdateDTO taskUpdateDTO, int id)
        {

            await _taskService.UpdateTaskAsync(taskUpdateDTO, id);

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        { 
            return Ok(await _taskService.DeleteTaskAsync(id));
        }
    }
}
