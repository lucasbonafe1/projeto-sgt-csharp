using SGT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public UserResponseDTO(int id, string name, string phoneNumber, string email, string password, DateTime accountCreationDate, ICollection<TaskEntity> tasks)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            Password = password;
            AccountCreationDate = accountCreationDate;
            Tasks = tasks ?? new List<TaskEntity>();
        }

        public UserResponseDTO(string name, string phoneNumber, string email, string password)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            Password = password;
        }

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

        public UserResponseDTO(UserRequestDTO userRequestDTO)
        {
            Name = userRequestDTO.Name;
            PhoneNumber = userRequestDTO.PhoneNumber;
            Email = userRequestDTO.Email;
        }
    }
}
