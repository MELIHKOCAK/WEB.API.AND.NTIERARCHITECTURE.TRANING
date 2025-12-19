using App.Services.Categories.Create;
using App.Services.Categories.Update;
namespace App.Services.Categories
{
    public interface ICategoryService
    {
        Task<ServiceResult<CategoryWithProductsDto>> GetCategoryByIdWithProductAsync(int id);
        Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryAllWithProductAsync();
        Task<ServiceResult<CategoryDto?>> GetByIdAsync(int id);
        Task<ServiceResult<CreateCategoryResponseDto>> CreateAsync(CreateCategoryRequestDto requestDto);
        Task<ServiceResult> UpdateAsync(UpdateCategoryRequestDto requestDto);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult<List<CategoryDto>>> GetAllAsync();
        Task<ServiceResult<List<CategoryDto>>> GetPagedAllListAsync(int pageNumber, int pageSize);
    }
}
