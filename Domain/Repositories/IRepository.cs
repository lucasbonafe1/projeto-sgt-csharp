namespace SGT.Domain.Repositories
{
    public interface IRepository<TEntity, Id> where TEntity : class // Vai ser reutilizado em outras interfaces devido ao CRUD ser o mesmo
    {
        Task<TEntity> Add(TEntity entity);
        Task<IEnumerable<TEntity?>> GetAll();
        Task<TEntity?> GetById(Id id);
        Task<TEntity?> Update(TEntity entity, int id);
        Task Delete(TEntity entity);
    }
}
