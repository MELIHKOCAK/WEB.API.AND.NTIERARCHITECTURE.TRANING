using App.Repositories;
using App.Repositories.EFCORE.Products;
using App.Services.Products.Create;
using App.Services.Products.Update;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace App.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
        {
            var products = await _productRepository.GetTopPriceProductAsync(count);
            var productAsDto = _mapper.Map<List<ProductDto>>(products);
            //var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

            return ServiceResult<List<ProductDto>>.Succes(productAsDto);
        }

        public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;

            var products = await _productRepository.GetAll(false)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            //var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

            var productsAsDto = _mapper.Map<List<ProductDto>>(products);

            return ServiceResult<List<ProductDto>>.Succes(productsAsDto);

        }

        public async Task<ServiceResult<List<ProductDto>>> GetAllAsync()
        {
            var products = await _productRepository.GetAll(false).ToListAsync();

            //var productAsDto = products.Select(p => new ProductDto (p.Id, p.Name, p.Price, p.Stock)).ToList();

            var productAsDto = _mapper.Map<List<ProductDto>>(products);

            return ServiceResult<List<ProductDto>>.Succes(productAsDto);
        }

        public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product is null)
                return ServiceResult<ProductDto>.Fail("Product Not Found");

           // var productsAsDto = new ProductDto(product!.Id, product.Name, product.Price, product.Stock);
            var productAsDto = _mapper.Map<ProductDto>(product);

            return ServiceResult<ProductDto>.Succes(productAsDto)!;
        }

        public async Task<ServiceResult<CreateProductResponseDto>> CreateAsync(CreateProductRequestDto requestDto)
        {
            #region ManuelMaping
            //var product = new Product()
            //{
            //    Name = requestDto.Name,
            //    Price = requestDto.Price,
            //    Stock = requestDto.Stock,
            //    CategoryId = requestDto.CategoryId  
            //};
            #endregion

            var isProductNameExist = await _productRepository
                .Where(x => x.Name == requestDto.Name, false)
                .AnyAsync();

            if (isProductNameExist)
                return ServiceResult<CreateProductResponseDto>.Fail("Aynı İsimde Bir Ürün Zaten Mevcut Lütfen Farklı Bir İsim Giriniz.");

            var product = _mapper.Map<Product>(requestDto);
            await _productRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return ServiceResult<CreateProductResponseDto>.SuccesAsCreated(new CreateProductResponseDto(product.Id),
                $"api/products/{product.Id}");
        }

        public async Task<ServiceResult> UpdateAsync(UpdateProductRequestDto requestDto)
        {
            var product = await _productRepository
                .GetByIdAsync(requestDto.id);

            var isProductNameExist = await _productRepository
                .Where((x => x.Name == requestDto.name && requestDto.id != x.Id), false)
                .AnyAsync();

            if (product is null)
                return ServiceResult.Fail("Product Not Found");


            if (isProductNameExist)
                return ServiceResult.Fail("Aynı İsimde Bir Ürün Zaten Mevcut Lütfen Farklı Bir İsim Giriniz.");

            //@TODO: AutoMapper Kullanarak Map İşlemini Gerçekleştir.
            product.Name = requestDto.name;
            product.Price = requestDto.price;
            product.Stock = requestDto.stock;

            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Succes(System.Net.HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> UpdateStockAsync(int productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product is null)
                return ServiceResult.Fail("Product nor found", HttpStatusCode.NotFound);

            product.Stock = quantity;
            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Succes(HttpStatusCode.NoContent);
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
