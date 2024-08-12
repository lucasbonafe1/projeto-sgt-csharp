using System.ComponentModel;

namespace SGT.Domain.Enum
{
    public enum StatusTask
    {
        [Description("Pending task")]
        Pending = 1,

        [Description("In progress")]
        In_Progress = 2, 

        [Description("Task completed successfully")]
        Completed = 3
    }
}
