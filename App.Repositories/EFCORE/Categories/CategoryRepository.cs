using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace App.Repositories.EFCORE.Categories
{
    public class CategoryRepository(AppDbContext context) : GenericRepositoryBase<Category>(context), ICategoryRepository
    {
        public async Task<Category?> GetCategoryByIdWithProductAsync(int id)
        {
            return await context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public IQueryable<Category?> GetCategoryAllWithProductAsync()
        {
            return context.Categories.Include(c => c.Products).AsQueryable();
        }
    }
}
