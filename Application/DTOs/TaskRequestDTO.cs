using SGT.Domain.Entities;
using SGT.Domain.Enum;

namespace SGT.Application.DTOs
{
    public class TaskRequestDTO
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int DurationInDays { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusTask? Status { get; set; }
        public int UserId { get; set; }

        public TaskRequestDTO() { }

        public TaskRequestDTO(TaskEntity userEntity)
        {
            Title = userEntity.Title;
            Description = userEntity.Description;
            DurationInDays = userEntity.DurationInDays;
            StartDate = userEntity.StartDate;
            EndDate = userEntity.EndDate;
            Status = userEntity.Status;
            UserId = userEntity.UserId;
        }
    }
}
