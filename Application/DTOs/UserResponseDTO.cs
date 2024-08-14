using SGT.Domain.Entities;

namespace SGT.Application.DTOs
{
    public class UserResponseDTO
    { 
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime AccountCreationDate { get; set; }

        public ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();

        public UserResponseDTO() { }

        public UserResponseDTO(UserEntity userEntity)
        {
            Id = userEntity.Id;
            Name = userEntity.Name;
            PhoneNumber = userEntity.PhoneNumber;
            Email = userEntity.Email;
            Password = userEntity.Password;
            AccountCreationDate = userEntity.AccountCreationDate;
            Tasks = userEntity.Tasks;
        }
    }
}
