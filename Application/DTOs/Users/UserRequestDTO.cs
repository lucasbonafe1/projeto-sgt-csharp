using SGT.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SGT.Application.DTOs.Users
{
    public class UserRequestDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
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
