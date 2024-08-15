using SGT.Domain.Entities;

namespace SGT.Application.DTOs.Users
{
    public class UserGetAllDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public DateTime AccountCreationDate { get; set; }

        public UserGetAllDTO() { }

        public UserGetAllDTO(UserEntity userEntity)
        {
            Id = userEntity.Id;
            Name = userEntity.Name;
            PhoneNumber = userEntity.PhoneNumber;
            Email = userEntity.Email;
            AccountCreationDate = userEntity.AccountCreationDate;
        }
    }
}
