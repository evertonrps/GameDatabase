using System.Linq.Expressions;
using GameDatabase.Data.Context;
using GameDatabase.Domain.Interfaces.Repositories;
using GameDatabase.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace GameDatabase.Data.Repository;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntity>
{
    protected GameDatabaseContext Db;
    protected DbSet<TEntity> DbSet;

    protected Repository(GameDatabaseContext context)
    {
        Db = context;
        DbSet = Db.Set<TEntity>();
    }

    public virtual async Task<TEntity> Add(TEntity obj)
    {
        await DbSet.AddAsync(obj);
        return obj;
    }

    public virtual void Delete(int id)
    {
        DbSet.Remove(DbSet.Find(id));
    }

    public virtual void Dispose()
    {
        Db.Dispose();
    }

    public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match, string include = null)
    {
        try
        {
            if (string.IsNullOrEmpty(include)) return await DbSet.SingleOrDefaultAsync(match);

            //Include
            var args = include.Split(',');
            TEntity entidade = null;
            foreach (var item in args)
                if (!string.IsNullOrEmpty(item.Trim()))
                    entidade = await DbSet.Include(item.Trim()).SingleOrDefaultAsync(match);

            return entidade;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<ICollection<TEntity>> FindAll(Expression<Func<TEntity, bool>> match, string include = null)
    {
        try
        {
            var query = DbSet.AsQueryable();
            List<TEntity> list;
            ;

            if (string.IsNullOrEmpty(include))
                return await DbSet.Where(match).ToListAsync();
            var args = include.Split(',');

            foreach (var item in args)
                if (!string.IsNullOrEmpty(item.Trim()))
                    query = query.Include(item.Trim());

            query = query.Where(match);

            return list = new List<TEntity>(query);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public virtual async Task<IEnumerable<TEntity>> GetAll()
    {
        return await DbSet.ToListAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> GetByFunc(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public virtual async Task<TEntity> GetById(int id)
    {
        return await DbSet.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
    }

    public virtual async Task<int> SaveChanges()
    {
        return await Db.SaveChangesAsync();
    }

    public virtual void Update(TEntity obj)
    {
        DbSet.Update(obj);
    }
}