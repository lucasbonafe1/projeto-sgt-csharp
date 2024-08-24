using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGT.Application.DTOs.Tasks;
using SGT.Application.Interfaces;
using SGT.Infrastructure.Messaging.Producers.Task;
using Swashbuckle.AspNetCore.Annotations;

namespace SGT.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("tasks")]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly ITaskProducer _taskProducer;


        public TaskController(ITaskService taskService, ITaskProducer taskProducer)
        {
            _taskService = taskService;
            _taskProducer = taskProducer;
        }

        [HttpPost("create-task")]
        [SwaggerOperation(Summary = "Cria uma nova tarefa", Description = "Este endpoint cria uma nova tarefa e retorna a tarefa criada.")]
        public async Task<IActionResult> Post([FromBody] TaskRequestDTO task)
        { 
            var taskCreated = await _taskService.AddTaskAsync(task);

            //_taskProducer.SendTaskMessage(taskCreated);
            
            return Ok(taskCreated);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Busca todas as tarefas registradas no sistema", Description = "Este endpoint busca todas as tasks salvas no sistema.")]
        public async Task<IActionResult> FindAll()
        {
            IEnumerable<TaskResponseDTO> tasks = await _taskService.GetAllTasksAsync();

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Busca cada tarefa específica por id", Description = "Este endpoint busca cada task específico pelo id.")]
        public async Task<IActionResult> FindById(int id)
        {
            TaskResponseDTO task = await _taskService.GetTaskByIdAsync(id);

            return Ok(task);
        }

        [HttpGet("my-tasks/{id}")]
        [SwaggerOperation(Summary = "Busca cada tarefa específica por cada user_id", Description = "Este endpoint busca cada tarefa pelo id do usuário (Busca por tasks espefíficas de cada user).")]
        public async Task<IActionResult> FindTasksByUserId(int id)
        {
            IEnumerable<TaskResponseDTO> tasks = await _taskService.GetTasksByUserIdAsync(id);

            return Ok(tasks);
        }

        [HttpPut("update-task{id}")]
        [SwaggerOperation(Summary = "Atualiza cada tarefa específica pelo id", Description = "Este endpoint atualiza cada tarefa pelo id.")]
        public async Task<ActionResult> Put([FromBody] TaskUpdateDTO taskUpdateDTO, int id)
        { 
            await _taskService.UpdateTaskAsync(taskUpdateDTO, id);

            //_taskProducer.UpdatedTaskMessage(taskUpdateDTO);

            return NoContent();

        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deleta uma tarefa específica pelo id", Description = "Este endpoint deleta cada tarefa pelo id (Delete lógico).")]
        public async Task<ActionResult> Delete(int id)
        { 
            return Ok(await _taskService.DeleteTaskAsync(id));
        }
    }
}
