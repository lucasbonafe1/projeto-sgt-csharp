using SGT.Application.DTOs;
using SGT.Application.Interfaces;
using SGT.Domain.Repositories;

namespace SGT.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task AddUserAsync(UserResponseDTO user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserResponseDTO>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserResponseDTO> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(UserResponseDTO user)
        {
            throw new NotImplementedException();
        }
    }
}
