﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGT.Domain.Entities
{
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("user_id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("password")]
        public string Password { get; set; } = string.Empty;

        [Column("account_creation_date")]
        public DateTime AccountCreationDate { get; set; } = DateTime.UtcNow;

        public ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();

        public UserEntity() { }

        public UserEntity(string name, string phoneNumber, string email, string password)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            Password = password;
        }
    }
}
