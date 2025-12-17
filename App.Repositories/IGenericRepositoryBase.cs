using System.Linq.Expressions;

namespace App.Repositories;
public interface IGenericRepositoryBase<T> where T : class
{
    /// <summary>
    /// Veri Tabanındaki Tüm Verileri Getirmeye Yarar
    /// </summary>
    /// <param name="trackChanges">AsNoTracking() ifadesini yapılandırır True: Takip Etmek İstiyorum   False: Takip etmek istemiyorum</param>
    /// <returns></returns>
    IQueryable<T> GetAll(bool trackChanges);
    IQueryable<T> Where(Expression<Func<T,bool>> predicate, bool trackChanges);
    ValueTask<T?> GetByIdAsync(int id);
    ValueTask AddAsync(T Entity);
    void Update(T entity);
    void Delete(T entity);
}
