using App.Services.Category.Create;
using App.Services.Category.Update;
using App.Services.Products;
using App.Services.Products.Create;
using App.Services.Products.Update;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services.Category
{
    public interface ICategoryService
    {
        Task<ServiceResult<CategoryDto?>> GetByIdAsync(int id);
        Task<ServiceResult<CreateCategoryResponseDto>> CreateAsync(CreateCategoryRequestDto requestDto);
        Task<ServiceResult> UpdateAsync(UpdateCategoryRequestDto requestDto);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult<List<CategoryDto>>> GetAllAsync();
        Task<ServiceResult<List<CategoryDto>>> GetPagedAllListAsync(int pageNumber, int pageSize);
    }
}
