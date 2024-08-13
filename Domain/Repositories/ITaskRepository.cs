using SGT.Domain.Entities;

namespace SGT.Domain.Repositories
{
    public interface ITaskRepository : IRepository<TaskEntity, int>
    {
        Task<IEnumerable<TaskEntity?>> GetTasksByUserId(int id);
    }
}
