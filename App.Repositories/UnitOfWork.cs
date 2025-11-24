using App.Repositories.EFCORE;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Repositories
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public Task<int> SaveChangesAsync() => context.SaveChangesAsync();
    }
}
