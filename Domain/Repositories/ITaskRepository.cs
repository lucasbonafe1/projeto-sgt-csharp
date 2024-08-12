using SGT.Domain.Entities;

namespace SGT.Domain.Repositories
{
    public interface ITaskRepository : IRepository<TaskEntity, int>
    {
        // TODO: rever se é necessário método específico
    }
}
