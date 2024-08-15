using SGT.Application.DTOs.Users;

namespace SGT.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDTO> GetUserByIdAsync(int id);
        Task<IEnumerable<UserGetAllDTO>> GetAllUsersAsync();
        Task<UserResponseDTO> AddUserAsync(UserRequestDTO user);
        Task UpdateUserAsync(UserUpdateDTO user, int id);
        Task<bool> DeleteUserAsync(int id);
    }
}
