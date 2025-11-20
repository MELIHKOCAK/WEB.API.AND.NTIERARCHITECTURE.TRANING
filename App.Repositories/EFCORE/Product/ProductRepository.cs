using Microsoft.EntityFrameworkCore;

namespace App.Repositories.EFCORE.Product
{
    public class ProductRepository(AppDbContext context) : GenericRepositoryBase<Product>(context), IProductRepository
    {


        public Task<List<Product>> GetTopPriceProductAsync(int count)
        {
           return context.Products.OrderByDescending(x => x.Price).Take(count).ToListAsync();
        }
    }
}

