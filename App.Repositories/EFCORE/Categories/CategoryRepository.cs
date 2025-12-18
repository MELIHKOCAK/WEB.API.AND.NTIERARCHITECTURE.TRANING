using Microsoft.EntityFrameworkCore;

namespace App.Repositories.EFCORE.Categories;
public class CategoryRepository(AppDbContext context) : GenericRepositoryBase<Category, int>(context), ICategoryRepository
{
    public async Task<Category?> GetCategoryByIdWithProductAsync(int id)
    {
        return await context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public IQueryable<Category?> GetCategoryAllWithProduct()
    {
        return context.Categories.Include(c => c.Products).AsQueryable();
    }
}
