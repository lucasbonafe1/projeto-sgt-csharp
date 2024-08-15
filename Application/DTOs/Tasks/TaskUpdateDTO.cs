﻿using SGT.Domain.Enum;

namespace SGT.Application.DTOs.Tasks
{
    public class TaskUpdateDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public StatusTask? Status { get; set; }

    }
}
