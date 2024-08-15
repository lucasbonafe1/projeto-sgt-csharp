using SGT.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace SGT.Application.DTOs.Tasks
{
    public class TaskUpdateDTO
    {
        [StringLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
        public string? Title { get; set; }

        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public StatusTask? Status { get; set; }

    }
}
