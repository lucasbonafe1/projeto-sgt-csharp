using Microsoft.AspNetCore.Mvc;
using SGT.Application.DTOs;
using SGT.Application.Interfaces;
using SGT.Application.Services;

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

            return Ok(userCreated);
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            IEnumerable<UserResponseDTO> users = await _userService.GetAllUsersAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(int id)
        {
            UserResponseDTO user = await _userService.GetUserByIdAsync(id);

            return Ok(user);
        }
    }
}
