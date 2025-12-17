using App.Repositories.EFCORE;

namespace App.Repositories;
public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public Task<int> SaveChangesAsync() => context.SaveChangesAsync();
}
