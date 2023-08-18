using System.Linq.Expressions;
using GameDatabase.Domain.SeedWork;

namespace GameDatabase.Domain.Interfaces.Repositories;

public interface IRepository<TEntity> : IDisposable where TEntity : Entity<TEntity>
{
    Task<IEnumerable<TEntity>> GetAll();

    Task<TEntity> Add(TEntity obj);

    Task<TEntity> GetById(int id);

    void Update(TEntity obj);

    void Delete(int id);

    Task<IEnumerable<TEntity>> GetByFunc(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match, string include = null);
    Task<ICollection<TEntity>> FindAll(Expression<Func<TEntity, bool>> match, string include = null);

    Task<int> SaveChanges();
}