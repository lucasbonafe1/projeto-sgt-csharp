using SGT.Domain.Entities;

namespace SGT.Application.DTOs
{
    public class UserRequestDTO
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public UserRequestDTO() { }

        public UserRequestDTO(UserEntity userEntity)
        {
            Name = userEntity.Name;
            PhoneNumber = userEntity.PhoneNumber;
            Email = userEntity.Email;
            Password = userEntity.Password;
        }
    }
}
