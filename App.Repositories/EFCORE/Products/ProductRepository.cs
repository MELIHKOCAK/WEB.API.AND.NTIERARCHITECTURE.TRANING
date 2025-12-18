using Microsoft.EntityFrameworkCore;

namespace App.Repositories.EFCORE.Products;
public class ProductRepository(AppDbContext context) : GenericRepositoryBase<Product, int>(context), IProductRepository
{
    Task<List<Product>> IProductRepository.GetTopPriceProductAsync(int count)
        => context.Products.OrderByDescending(x => x.Price).Take(count).ToListAsync();
}

