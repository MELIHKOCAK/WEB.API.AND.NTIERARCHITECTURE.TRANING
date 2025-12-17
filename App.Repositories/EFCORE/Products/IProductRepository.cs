namespace App.Repositories.EFCORE.Products;
public interface IProductRepository : IGenericRepositoryBase<Product>
{
    public Task<List<Product>> GetTopPriceProductAsync(int count);
}
