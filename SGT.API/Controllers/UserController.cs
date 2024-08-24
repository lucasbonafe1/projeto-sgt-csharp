using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGT.Application.DTOs.Users;
using SGT.Application.Interfaces;
using SGT.Infrastructure.Messaging.Producers.User;
using Swashbuckle.AspNetCore.Annotations;

namespace SGT.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserProducer _userProducer;  

        public UserController(IUserService userService, IUserProducer userProducer)
        {
            _userService = userService;
            _userProducer = userProducer;   
        }

        [HttpPost("create-account")]
        [SwaggerOperation(Summary = "Cria uma nova conta", Description = "Este endpoint cria uma nova conta e retorna o usuário criado.")]
        public async Task<IActionResult> Post([FromBody] UserRequestDTO user)
        {
            var userCreated = await _userService.AddUserAsync(user);
            //_userProducer.CreateAccountMessage(userCreated);

            return Ok(userCreated);
        }

        [Authorize]
        [HttpGet]
        [SwaggerOperation(Summary = "Busca todos os usuários registrados no sistema", Description = "Este endpoint busca todos os usuários salvos no sistema, com um DTO seguro.")]
        public async Task<IActionResult> FindAll()
        {
            IEnumerable<UserGetAllDTO> users = await _userService.GetAllUsersAsync();

            if (users == null || !users.Any())
            {
                return NotFound("Nenhum usuário encontrado.");
            }

            return Ok(users);
        }

        [Authorize]
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Busca cada usuário específico por id", Description = "Este endpoint busca cada usuário específico pelo id.")]
        public async Task<IActionResult> FindById(int id)
        {
            UserResponseDTO user = await _userService.GetUserByIdAsync(id);

            _userProducer.GetTimeTask(user);

            return Ok(user);
        }

        [Authorize]
        [HttpPut("update-account-data/{id}")]
        [SwaggerOperation(Summary = "Atualiza cada usuário específico pelo id", Description = "Este endpoint atualiza cada usuário pelo id.")]
        public async Task<IActionResult> Put([FromBody] UserUpdateDTO userDTO, int id)
        {
            await _userService.UpdateUserAsync(userDTO, id);

            _userProducer.UpdatedAccountMessage(userDTO);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deleta um usuário específico pelo id", Description = "Este endpoint deleta cada usuário pelo id (Delete lógico).")]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await _userService.DeleteUserAsync(id));
        }
    }
}
