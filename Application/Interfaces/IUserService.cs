using SGT.Application.DTOs;

namespace SGT.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDTO> GetUserByIdAsync(int id);
        Task<IEnumerable<UserResponseDTO>> GetAllUsersAsync();
        Task<UserResponseDTO> AddUserAsync(UserRequestDTO user);
        Task UpdateUserAsync(UserUpdateDTO user, int id);
        Task<bool> DeleteUserAsync(int id);
    }
}
