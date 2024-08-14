using SGT.Application.DTOs;
using SGT.Application.Interfaces;
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
                throw new ArgumentNullException("User não pode ser nulo.");
            }

            UserEntity user = new UserEntity(userDTO.Name,
                                 userDTO.PhoneNumber,
                                 userDTO.Email,
                                 userDTO.Password);

            var userCreated = await _userRepository.Add(user);

            UserResponseDTO userConverted = new UserResponseDTO(userCreated);

            return userConverted;
        }

        public async Task<IEnumerable<UserResponseDTO>> GetAllUsersAsync()
        {

            IEnumerable<UserEntity?> users = await _userRepository.GetAll();

            if (users == null)
            {
                throw new ApplicationException("Nenhum user encontrado.");
            }

            var usersConverted = users.Select(user => new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Password = user.Password,
                AccountCreationDate = user.AccountCreationDate
            });

            return usersConverted;
        }

        public async Task<UserResponseDTO> GetUserByIdAsync(int id)
        {
            UserEntity? user = await _userRepository.GetById(id);

            if (user == null)
            {
                throw new ApplicationException($"Nenhum user encontrado com o id {id}");
            }

            UserResponseDTO userConverted = new UserResponseDTO(user);

            return userConverted;
        }

        public async Task UpdateUserAsync(UserUpdateDTO userDTO, int id)
        {
            var existingUser = await _userRepository.GetById(id);

            if (existingUser == null)
            {
                throw new ApplicationException("User não encontrado.");
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

            await _userRepository.Update(existingUser, id);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return (id <= 0) ? throw new ApplicationException("Id inexistente.") : await _userRepository.Delete(id);
        }
    }
}
