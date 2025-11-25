using App.Repositories;
using App.Repositories.EFCORE.Products;
using Microsoft.EntityFrameworkCore;
using System.Net;


namespace App.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
        {
            var products = await _productRepository.GetTopPriceProductAsync(count);

            var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

            return new ServiceResult<List<ProductDto>>
            {
                Data = productsAsDto
            };
        }

        public async Task<ServiceResult<List<ProductDto>>> GetAllAsync()
        {
            var products = await _productRepository.GetAll(false).ToListAsync();

            var productAsDto = products.Select(p => new ProductDto (p.Id, p.Name, p.Price, p.Stock)).ToList();

            return ServiceResult<List<ProductDto>>.Succes(productAsDto);
        }

        public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product is null)
                return ServiceResult<ProductDto>.Fail("Product Not Found");

            var productsAsDto = new ProductDto(product!.Id, product.Name, product.Price, product.Stock);

            return ServiceResult<ProductDto>.Succes(productsAsDto)!;
        }

        public async Task<ServiceResult<CreateProductResponseDto>> CreateAsync(CreateProductRequestDto requestDto)
        {
            var product = new Product()
            {
                Name = requestDto.name,
                Price = requestDto.price,
                Stock = requestDto.stock,
                CategoryId = requestDto.categoryId  
            };

            await _productRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return ServiceResult<CreateProductResponseDto>.Succes(new CreateProductResponseDto(product.Id));
        }

        public async Task<ServiceResult> UpdateAsync(UpdateProductRequestDto requestDto)
        {
            var product = await _productRepository.GetByIdAsync(requestDto.id);

            if(product is null)
                return ServiceResult.Fail("Product Not Found", HttpStatusCode.BadRequest);

            //@TODO: AutoMapper Kullanarak Map İşlemini Gerçekleştir.
            product.Name = requestDto.name;
            product.Price = requestDto.price;
            product.Stock = requestDto.stock;

            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync();

            return  ServiceResult.Succes(System.Net.HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product is null)
                return ServiceResult.Fail("Product Not Found");

            _productRepository.Delete(product);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Succes(HttpStatusCode.NoContent);
        }
    }
}
