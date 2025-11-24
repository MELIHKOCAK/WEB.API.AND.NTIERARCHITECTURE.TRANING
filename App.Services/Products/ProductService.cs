using App.Repositories;
using App.Repositories.EFCORE.Products;
using System.Net;


namespace App.Services.Products
{
    public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork) : IProductService
    {
        public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
        {
            var products = await productRepository.GetTopPriceProductAsync(count);

            var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

            return new ServiceResult<List<ProductDto>>
            {
                Data = productsAsDto
            };
        }

        public async Task<ServiceResult<ProductDto>> GetByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null)
                ServiceResult<ProductDto>.Fail("Product Not Found");

            var productsAsDto = new ProductDto(product!.Id, product.Name, product.Price, product.Stock);

            return ServiceResult<ProductDto>.Succes(productsAsDto!);
        }

        public async Task<ServiceResult<CreateProductResponseDto>> CreateProductAsync(CreateProductRequestDto requestDto)
        {
            var product = new Product()
            {
                Name = requestDto.name,
                Price = requestDto.price,
                Stock = requestDto.stock,
            };

            await productRepository.AddAsync(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult<CreateProductResponseDto>.Succes(new CreateProductResponseDto(product.Id));
        }

        public async Task<ServiceResult> UpdateProductAsync(UpdateProductRequestDto requestDto)
        {
            var product = await productRepository.GetByIdAsync(requestDto.id);

            if(product is null)
                return ServiceResult.Fail("Product Not Found", HttpStatusCode.BadRequest);

            //@TODO: AutoMapper Kullanarak Map İşlemini Gerçekleştir.
            product.Name = requestDto.name;
            product.Price = requestDto.price;
            product.Stock = requestDto.stock;

            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();

            return  ServiceResult.Succes(System.Net.HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteProductAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null)
                return ServiceResult.Fail("Product Not Found");

            productRepository.Delete(product);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Succes(HttpStatusCode.NoContent);
        }
    }
}
