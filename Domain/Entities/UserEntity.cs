using System.ComponentModel.DataAnnotations;
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
        public string Name { get; set; }

        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("account_creation_date")]
        public DateTime AccountCreationDate { get; set; }

        [Column("tasks")]
        public ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();

        public UserEntity() { }

        public UserEntity(int id, string name, string phoneNumber, string email, string password, DateTime accountCreationDate, ICollection<TaskEntity> tasks)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            Password = password;
            AccountCreationDate = accountCreationDate;
            Tasks = tasks ?? new List<TaskEntity>(); // inicializado para que não haja erro se o param for null
        }
    }
}
