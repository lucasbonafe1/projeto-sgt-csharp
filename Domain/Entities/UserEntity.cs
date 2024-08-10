using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGT.Domain.Entities
{
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("userId")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("tasks")]
        public ICollection<TaskEntity> Tasks{ get; set; }

        public UserEntity() { }

        public UserEntity(int id, string name, string email, string password, ICollection<TaskEntity> tasks)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Tasks = tasks;
        }
    }
}
