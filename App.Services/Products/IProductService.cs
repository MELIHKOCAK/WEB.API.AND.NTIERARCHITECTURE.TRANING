using App.Services.Products.Create;
using App.Services.Products.Update;
namespace App.Services.Products;
public interface IProductService
{
    Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count);
    Task<ServiceResult<ProductDto?>> GetByIdAsync(int id);
    Task<ServiceResult<CreateProductResponseDto>> CreateAsync(CreateProductRequestDto requestDto);
    Task<ServiceResult> UpdateAsync(UpdateProductRequestDto requestDto);
    Task<ServiceResult> DeleteAsync(int id);
    Task<ServiceResult<List<ProductDto>>> GetAllAsync();
    Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize);
    Task<ServiceResult> UpdateStockAsync(int productId, int quantity);
}
