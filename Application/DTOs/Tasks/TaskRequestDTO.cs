using SGT.Domain.Entities;
using SGT.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace SGT.Application.DTOs.Tasks
{
    public class TaskRequestDTO
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "A data de início é obrigatória.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "A data de término é obrigatória.")]
        public DateTime EndDate { get; set; }

        public StatusTask? Status { get; set; }

        [Required(ErrorMessage = "O id do usuário é obrigatório.")]
        public int UserId { get; set; }

        public TaskRequestDTO() { }

        public TaskRequestDTO(TaskEntity taskEntity)
        {
            Title = taskEntity.Title;
            Description = taskEntity.Description;
            StartDate = taskEntity.StartDate;
            EndDate = taskEntity.EndDate;
            Status = taskEntity.Status;
            UserId = taskEntity.UserId;
        }
    }
}