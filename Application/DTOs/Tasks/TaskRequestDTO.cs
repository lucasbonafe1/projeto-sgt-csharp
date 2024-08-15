using SGT.Domain.Entities;
using SGT.Domain.Enum;

namespace SGT.Application.DTOs.Tasks
{
    public class TaskRequestDTO
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusTask? Status { get; set; }
        public int UserId { get; set; }

        public TaskRequestDTO() { }

        public TaskRequestDTO(TaskEntity userEntity)
        {
            Title = userEntity.Title;
            Description = userEntity.Description;
            StartDate = userEntity.StartDate;
            Status = userEntity.Status;
            UserId = userEntity.UserId;
        }
    }
}
