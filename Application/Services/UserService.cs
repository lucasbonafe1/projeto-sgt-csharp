
using SGT.Application.DTOs.Users;
using SGT.Application.Interfaces;
using SGT.Core.Exceptions;
using SGT.Domain.Entities;
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

        public async Task<UserResponseDTO> AddUserAsync(UserRequestDTO userDTO)
        {
            if(userDTO == null)
            { 
                throw new BadRequestException("User não pode ser nulo.");
            }

            // criptografa a senha para ser armazenada no bd
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);

            UserEntity user = new UserEntity(userDTO.Name,
                                             userDTO.PhoneNumber,
                                             userDTO.Email,
                                             hashedPassword);


            var userCreated = await _userRepository.Add(user);

            UserResponseDTO userConverted = new UserResponseDTO(userCreated);

            return userConverted;
        }

        public async Task<IEnumerable<UserGetAllDTO>> GetAllUsersAsync()
        {

            IEnumerable<UserEntity?> users = await _userRepository.GetAll();

            if (users == null)
            {
                throw new NotFoundException("Nenhum user encontrado.");
            }

            var usersConverted = users.Select(user => new UserGetAllDTO
            {
                Id = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                AccountCreationDate = user.AccountCreationDate
            });

            return usersConverted;
        }

        public async Task<UserResponseDTO> GetUserByIdAsync(int id)
        {
            UserEntity? user = await _userRepository.GetById(id);

            if (user == null)
            {
                throw new NotFoundException("Nenhum user encontrado.");
            }

            UserResponseDTO userConverted = new UserResponseDTO(user);

            return userConverted;
        }

        public async Task UpdateUserAsync(UserUpdateDTO userDTO, int id)
        {
            var existingUser = await _userRepository.GetById(id);

            if (existingUser == null)
            {
                throw new NotFoundException("User não encontrado.");
            }

            var taskProperties = typeof(UserEntity).GetProperties();
            var dtoProperties = typeof(UserUpdateDTO).GetProperties();

            foreach (var dtoProperty in dtoProperties)
            {
                var taskProperty = taskProperties.FirstOrDefault(p => p.Name == dtoProperty.Name && p.PropertyType == dtoProperty.PropertyType);
                if (taskProperty != null && taskProperty.CanWrite)
                {
                    var value = dtoProperty.GetValue(userDTO);
                    if (value != null)
                    {
                        taskProperty.SetValue(existingUser, value);
                    }
                }
            }

            existingUser.Password = BCrypt.Net.BCrypt.HashPassword(existingUser.Password);

            await _userRepository.Update(existingUser, id);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return (id <= 0) ? throw new NotFoundException("Id inexistente.") : await _userRepository.Delete(id);
        }
    }
}
