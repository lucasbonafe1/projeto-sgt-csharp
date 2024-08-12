using SGT.Domain.Entities;

namespace SGT.Domain.Repositories
{
    public interface IUserRepository : IRepository<UserEntity, int>
    {
        // TODO: rever se é necessário método específico
    }
}
