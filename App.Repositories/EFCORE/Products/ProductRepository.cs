using Microsoft.EntityFrameworkCore;

namespace App.Repositories.EFCORE.Products
{
    public class ProductRepository(AppDbContext context) : GenericRepositoryBase<Product>(context), IProductRepository
    {

        Task<List<Product>> IProductRepository.GetTopPriceProductAsync(int count)
        {
            return context.Products.OrderByDescending(x => x.Price).Take(count).ToListAsync();
        }
    }
}

