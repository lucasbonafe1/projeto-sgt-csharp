using SGT.Domain.Entities;
using SGT.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGT.Application.DTOs
{
    public class TaskResponseDTO
    {        
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int DurationInDays { get; set; }
        public DateTime CriationDate { get; set; } 
        public DateTime EndDate { get; set; }
        public StatusTask Status { get; set; }
        public UserEntity User { get; set; }
        public TaskResponseDTO() { }

        public TaskResponseDTO(int id, string title, string description, int durationInDays, DateTime criationDate, DateTime endDate, StatusTask status, UserEntity user)
        {
            Id = id;
            Title = title;
            Description = description;
            DurationInDays = durationInDays;
            CriationDate = criationDate;
            EndDate = endDate;
            Status = status;
            User = user;
        }
    }
}
