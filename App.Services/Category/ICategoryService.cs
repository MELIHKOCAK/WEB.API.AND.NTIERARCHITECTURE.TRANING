using App.Services.Category.Create;
using App.Services.Category.Update;
namespace App.Services.Category
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
