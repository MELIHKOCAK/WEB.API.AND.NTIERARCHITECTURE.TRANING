namespace App.Repositories.EFCORE.Categories;
public interface ICategoryRepository:IGenericRepositoryBase<Category, int>
{
    Task<Category?> GetCategoryByIdWithProductAsync(int id);
    IQueryable<Category?> GetCategoryAllWithProduct();
}
