namespace App.Repositories.EFCORE.Categories;
public interface ICategoryRepository:IGenericRepositoryBase<Category>
{
    Task<Category?> GetCategoryByIdWithProductAsync(int id);
    IQueryable<Category?> GetCategoryAllWithProduct();
}
