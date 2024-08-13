using Microsoft.AspNetCore.Mvc;
using SGT.Application.DTOs;
using SGT.Application.Interfaces;

namespace SGT.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create-account")]
        public async Task<IActionResult> Post([FromBody] UserRequestDTO user)
        {
            //TODO: adicionar validação para se User for null

            var userCreated = await _userService.AddUserAsync(user);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro na criação de conta.");
            }

            return Ok(userCreated);
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            IEnumerable<UserResponseDTO> users = await _userService.GetAllUsersAsync();

            if (users == null || !users.Any())
            {
                return NotFound("Nenhum user encontrado com o id");
            }


            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            UserResponseDTO user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound($"Nenhum user encontrado com o id {id}");
            }

            return Ok(user);
        }
    }
}
