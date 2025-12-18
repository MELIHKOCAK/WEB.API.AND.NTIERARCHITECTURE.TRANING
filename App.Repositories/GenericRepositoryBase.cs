using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Repositories;
public class GenericRepositoryBase<T, TId> : IGenericRepositoryBase<T, TId>
    where T : BaseEntity<TId>
    where TId : struct
{
    private readonly DbContext _dbcontext;
    private readonly DbSet<T> _dbSet;
    public GenericRepositoryBase(DbContext dbContext)
    {
        _dbcontext = dbContext;
        _dbSet = _dbcontext.Set<T>();
    }
    public async ValueTask AddAsync(T Entity) => await _dbcontext.AddAsync(Entity);

    public async ValueTask<T?> GetByIdAsync(int id) => await _dbcontext.Set<T>().FindAsync(id);

    public void Delete(T entity) => _dbcontext.Remove(entity);

    public IQueryable<T> GetAll(bool trackChanges)
    {
        if (trackChanges)
            return _dbcontext.Set<T>().AsQueryable();
        else
            return _dbcontext.Set<T>().AsNoTracking().AsQueryable();
    }

    public void Update(T entity) => _dbcontext.Update(entity);

    public IQueryable<T> Where(Expression<Func<T, bool>> predicate, bool trackChanges)
    {
        if(trackChanges)
           return _dbcontext.Set<T>().Where(predicate);
        else
           return _dbcontext.Set<T>().Where(predicate).AsNoTracking();
    }

    public Task<bool> AnyAsync(TId id) => _dbSet.AnyAsync(x => x.Id.Equals(id));
}
