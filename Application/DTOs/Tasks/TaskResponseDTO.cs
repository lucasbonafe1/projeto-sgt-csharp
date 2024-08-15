using SGT.Domain.Entities;
using SGT.Domain.Enum;

namespace SGT.Application.DTOs.Tasks
{
    public class TaskResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusTask? Status { get; set; }
        public int UserId { get; set; }

        public TaskResponseDTO() { }

        public TaskResponseDTO(int id, string title, string description, DateTime criationDate, DateTime endDate, StatusTask status, int userId)
        {
            Id = id;
            Title = title;
            Description = description;
            StartDate = criationDate;
            EndDate = endDate;
            Status = status;
            UserId = userId;
        }

        public TaskResponseDTO(TaskEntity userEntity)
        {
            Id = userEntity.Id;
            Title = userEntity.Title;
            Description = userEntity.Description;
            StartDate = userEntity.StartDate;
            EndDate = userEntity.EndDate;
            Status = userEntity.Status;
            UserId = userEntity.UserId;
        }
    }
}
