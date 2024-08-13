using SGT.Application.DTOs;

namespace SGT.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDTO> GetUserByIdAsync(int id);
        Task<IEnumerable<UserResponseDTO>> GetAllUsersAsync();
        Task<UserRequestDTO> AddUserAsync(UserRequestDTO user);
        Task UpdateUserAsync(UserRequestDTO user);
        Task DeleteUserAsync(int id);
    }
}
