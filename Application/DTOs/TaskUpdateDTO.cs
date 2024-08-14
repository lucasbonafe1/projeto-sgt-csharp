using SGT.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGT.Application.DTOs
{
    public class TaskUpdateDTO
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int? DurationInDays { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusTask? Status { get; set; }

    }
}
