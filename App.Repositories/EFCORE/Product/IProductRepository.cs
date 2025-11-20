namespace App.Repositories.EFCORE.Product
{
    public interface IProductRepository : IGenericRepositoryBase<Product>
    {
        public Task<List<Product>> GetTopPriceProductAsync(int count);
    }
}
