using System;
using System.Collections.Generic;
using System.Text;

namespace App.Repositories.EFCORE.Categories
{
    public interface ICategoryRepository:IGenericRepositoryBase<Category>
    {
        Task<Category?> GetCategoryByIdWithProductAsync(int id);
        IQueryable<Category?> GetCategoryAllWithProduct();
    }
}
