using App.Repositories;
using App.Repositories.EFCORE.Categories;
using App.Repositories.EFCORE.Products;
using App.Services.Category.Create;
using App.Services.Category.Update;
using App.Services.Products.Create;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace App.Services.Category
{

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<CreateCategoryResponseDto>> CreateAsync(CreateCategoryRequestDto requestDto)
        {
            var isProductNameExist = await _categoryRepository
                .Where(x => x.Name == requestDto.Name, false)
                .AnyAsync();

            if (isProductNameExist)
                return ServiceResult<CreateCategoryResponseDto>.Fail("Aynı İsimde Bir Category Zaten Mevcut Lütfen Farklı Bir İsim Giriniz.");

            var category = _mapper.Map<Repositories.EFCORE.Categories.Category>(requestDto);
            await _categoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return ServiceResult<CreateCategoryResponseDto>
                 .SuccesAsCreated(new CreateCategoryResponseDto(category.Id), $"api/products/{category.Id}");
        }

        public Task<ServiceResult> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<List<CategoryDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<CategoryDto?>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<List<CategoryDto>>> GetPagedAllListAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> UpdateAsync(UpdateCategoryRequestDto requestDto)
        {
            throw new NotImplementedException();
        }
    }
}
