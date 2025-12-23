using App.Repositories;
using App.Repositories.EFCORE.Categories;
using App.Repositories.EFCORE.Products;
using App.Services.Categories.Create;
using App.Services.Categories.Update;
using App.Services.Filters.NotFoundFilter;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System.Net;

namespace App.Services.Categories
{
    //@:TO-DO : Namespace Çakışmalarını düzelt. Repository ve service katmanlarında category ismi tekrar ediyor ve çakışıyor
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoryService> _logger;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, IUnitOfWork unitOfWork, ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ServiceResult<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAll(false).ToListAsync();
            var categoryAsDto = _mapper.Map<List<CategoryDto>>(categories);
            _logger.LogInformation("All Category Listed");
            return ServiceResult<List<CategoryDto>>.Succes(categoryAsDto);
        }

        public async Task<ServiceResult<CategoryDto?>> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            var categoryAsDto = _mapper.Map<CategoryDto>(category);
            _logger.LogInformation("Category listed with by id {id}",id);

            return ServiceResult<CategoryDto?>.Succes(categoryAsDto);
        }

        public async Task<ServiceResult<CategoryWithProductsDto>> GetCategoryByIdWithProductAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdWithProductAsync(id);
            var categoryAsDto = _mapper.Map<CategoryWithProductsDto>(category);
            _logger.LogInformation("Category listed with product by id {id}",id);


            return ServiceResult<CategoryWithProductsDto>.Succes(categoryAsDto);

        }

        public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryAllWithProductAsync()
        {
            var category = await _categoryRepository.GetCategoryAllWithProduct().ToListAsync();
            var categoryAsDto = _mapper.Map<List<CategoryWithProductsDto>>(category);
            _logger.LogInformation("All Category Listed With Product");

            return ServiceResult<List<CategoryWithProductsDto>>.Succes(categoryAsDto);
        }

        public async Task<ServiceResult<List<CategoryDto>>> GetPagedAllListAsync(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;

            var categories = await _categoryRepository
                .GetAll(false)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            var categoryAsDto = _mapper.Map<List<CategoryDto>>(categories);

            return ServiceResult<List<CategoryDto>>.Succes(categoryAsDto);
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
            _logger.LogInformation("Category Created {id}", category.Id);

            return ServiceResult<CreateCategoryResponseDto>
                 .SuccesAsCreated(new CreateCategoryResponseDto(category.Id), $"api/products/{category.Id}" );
        }

        public async Task<ServiceResult> UpdateAsync(UpdateCategoryRequestDto requestDto)
        {
            var isAnyCategoryExists = await _categoryRepository
                .Where((c => c.Name == requestDto.Name && requestDto.Id != c.Id), true)
                .AnyAsync();

            if (isAnyCategoryExists)
                return ServiceResult.Fail("Kategori ismi daha önceden mevcut farklı isim seçiniz!");

            var category = _mapper.Map<Category>(requestDto);

            _categoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Category Updated: {id}",category.Id);

            return ServiceResult.Succes(HttpStatusCode.NoContent);
        }
        
        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            _categoryRepository.Delete(category!);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Category Deleted: {id}", category!.Id);
            return ServiceResult.Succes(HttpStatusCode.NoContent);

        }
       
    }
}