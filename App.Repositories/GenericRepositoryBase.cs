using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace App.Repositories
{
    public class GenericRepositoryBase<T> : IGenericRepositoryBase<T> where T : class
    {
        private readonly DbContext _dbcontext;
        public GenericRepositoryBase(DbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async ValueTask AddAsync(T Entity) => await _dbcontext.AddAsync(Entity);

        public void Delete(T entity)
        {
            _dbcontext.Remove(entity);
        }

        public IQueryable<T> GetAll(bool trackChanges)
        {
            if (trackChanges)
                return _dbcontext.Set<T>().AsQueryable();
            else
                return _dbcontext.Set<T>().AsNoTracking().AsQueryable();
        }

        public ValueTask<T?> GetByIdAsync(int id) => _dbcontext.Set<T>().FindAsync(id);

        public void Update(T entity) => _dbcontext.Update(entity);

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate, bool trackChanges)
        {
            if(trackChanges)
               return _dbcontext.Set<T>().Where(predicate);
            else
               return _dbcontext.Set<T>().Where(predicate).AsNoTracking();
        }
    }
}
