using SGT.Domain.Entities;

namespace SGT.Domain.Repositories
{
    public interface IUserRepository : IRepository<UserEntity, int>
    {
        Task<bool> AuthLoginAsync(string email, string password);
    }
}
