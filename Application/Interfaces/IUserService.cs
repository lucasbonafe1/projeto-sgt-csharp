using SGT.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
