using SGT.Domain.Entities;

namespace SGT.Domain.Interfaces
{
    public interface IUserRepository : IRepository<UserEntity, int>
    {
        // TODO: rever se é necessário método específico
    }
}
