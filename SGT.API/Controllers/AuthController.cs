using Microsoft.AspNetCore.Mvc;
using SGT.Domain.Repositories;
using SGT.Infrastructure.Security;

namespace SGT.API.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Auth(string email, string password)
        {
             var userExist = await _userRepository.AuthLoginAsync(email, password);

            if (userExist)
            {
                var token = TokenService.GenerateToken(new Domain.Entities.UserEntity());
                return Ok(token);            
            }

            return BadRequest("Usuário inválido.");
        }
    }
}
