using SGT.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGT.Domain.Entities
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PeriodOfDays { get; set; }
        public int CriationDate { get; set; }
        public int EndDate { get; set; }
        public StatusTask Status { get; set; }
        public User User { get; set; }

        public Task() { }
    }
}
