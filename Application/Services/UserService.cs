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

        public async Task<UserRequestDTO> AddUserAsync(UserRequestDTO userDTO)
        {
            // add validação para se caso alguma informação for null

            UserEntity user = new UserEntity(userDTO.Name,
                                 userDTO.PhoneNumber,
                                 userDTO.Email,
                                 userDTO.Password);

            var userCreated = await _userRepository.Add(user);

            UserRequestDTO userConverted = new UserRequestDTO(userCreated);

            return userConverted;
        }

        public async Task<IEnumerable<UserResponseDTO>> GetAllUsersAsync()
        {

            IEnumerable<UserEntity?> users = await _userRepository.GetAll();

            if (users == null)
            {
                throw new ApplicationException("Nenhuma tarefa encontrada.");
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

        public async Task UpdateUserAsync(UserRequestDTO userDTO)
        {
            // SE O USUARIO NÂO SETAR MANTER O ANTIGO
            var existingUser = await _userRepository.GetById(userDTO.Id);

            if (existingUser == null)
            {
                throw new ApplicationException("User não encontrado.");
            }

            // atualiza os campos somente se forem passados no DTO
            existingUser.Name = !string.IsNullOrWhiteSpace(userDTO.Name) ? userDTO.Name : existingUser.Name;
            existingUser.PhoneNumber = !string.IsNullOrWhiteSpace(userDTO.PhoneNumber) ? userDTO.PhoneNumber : existingUser.PhoneNumber;
            existingUser.Email = !string.IsNullOrWhiteSpace(userDTO.Email) ? userDTO.Email : existingUser.Email;

            await _userRepository.Update(existingUser, userDTO.Id);
        }

        public Task DeleteUserAsync(int id)
        {
           throw new NotImplementedException();
        }
    }
}
