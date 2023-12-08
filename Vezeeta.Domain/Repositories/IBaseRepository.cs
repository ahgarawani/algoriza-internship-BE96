namespace Vezeeta.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int Id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        //Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> criteria, string[] includes = null);
        //Task<TEntity> AddAsync(TEntity entity);
    }
}
