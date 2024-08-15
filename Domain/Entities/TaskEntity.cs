using SGT.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGT.Domain.Entities
{
    public class TaskEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("task_id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("status")]
        public StatusTask? Status { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }


        public TaskEntity() { }

        public TaskEntity(string title, string? description, DateTime startDate, DateTime endDate, StatusTask? status, int userId)
        {
            Title = title;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            UserId = userId;
        }
    }
}
