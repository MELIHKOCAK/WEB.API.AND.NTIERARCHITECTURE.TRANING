using App.Repositories;
using App.Repositories.EFCORE.Categories;
using App.Repositories.EFCORE.Products;
using App.Services.Category.Create;
using App.Services.Category.Update;
using App.Services.Products.Create;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace App.Services.Category
{
    //@:TO-DO : Namespace Çakışmalarını düzelt. Repository ve service katmanlarında category ismi tekrar ediyor ve çakışıyor
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
                return ServiceResult<CreateCategoryResponseDto>
                    .Fail("Aynı İsimde Bir Kategori Zaten Mevcut Lütfen Farklı Bir İsim Giriniz.");

            var category = _mapper.Map<Repositories.EFCORE.Categories.Category>(requestDto);
            await _categoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return ServiceResult<CreateCategoryResponseDto>
                 .SuccesAsCreated(new CreateCategoryResponseDto(category.Id), $"api/products/{category.Id}" );
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
                return ServiceResult.Fail("Categoryi Bulunamadı",System.Net.HttpStatusCode.NotFound);

            _categoryRepository.Delete(category);
            await _unitOfWork.SaveChangesAsync();
            return ServiceResult.Succes(HttpStatusCode.NoContent);

        }

        public async Task<ServiceResult<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAll(false).ToListAsync();
            var categoryAsDto = _mapper.Map<List<CategoryDto>>(categories);

            return ServiceResult<List<CategoryDto>>.Succes(categoryAsDto);
        }

        public async Task<ServiceResult<CategoryDto?>> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            
            if (category is null)
                return ServiceResult<CategoryDto?>.Fail("Kategori Bulunamadı");

            var categoryAsDto = _mapper.Map<CategoryDto>(category);

            return ServiceResult<CategoryDto?>.Succes(categoryAsDto);
        }

        public async Task<ServiceResult<List<CategoryDto>>> GetPagedAllListAsync(int pageNumber, int pageSize)
        {
            int skip = (pageNumber-1) * pageSize;
            
            var categories = await _categoryRepository
                .GetAll(false)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            var categoryAsDto = _mapper.Map<List<CategoryDto>>(categories);

            return ServiceResult<List<CategoryDto>>.Succes(categoryAsDto);
        }

        public async Task<ServiceResult> UpdateAsync(UpdateCategoryRequestDto requestDto)
        {
            var category = await _categoryRepository.GetByIdAsync(requestDto.Id);

            if (category is null)
                return ServiceResult.Fail("Kategori Bulunumadı");

            var isAnyCategoryExists = await _categoryRepository
                .Where((c => c.Name == requestDto.Name && requestDto.Id != c.Id), true)
                .AnyAsync();

            if (isAnyCategoryExists)
                return ServiceResult.Fail("Kategori ismi daha önceden mevcut farklı isim seçiniz!");

            category.Name = requestDto.Name;

            _categoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Succes();
        }

        public async Task<ServiceResult<CategoryWithProductsDto>> GetCategoryByIdWithProductAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdWithProductAsync(id);

            if (category is null)
                return ServiceResult<CategoryWithProductsDto>.Fail("Kategori Bulunamadı"
                    ,HttpStatusCode.NotFound);

            var categoryAsDto = _mapper.Map<CategoryWithProductsDto>(category);

            return ServiceResult<CategoryWithProductsDto>.Succes(categoryAsDto);
             
        }

        public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryAllWithProductAsync()
        {
            var category = await _categoryRepository.GetCategoryAllWithProduct().ToListAsync();
            var categoryAsDto = _mapper.Map<List<CategoryWithProductsDto>>(category);  
            return ServiceResult<List<CategoryWithProductsDto>>.Succes(categoryAsDto);
        }
    }
}
