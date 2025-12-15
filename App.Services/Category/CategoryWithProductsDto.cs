using App.Services.Products;


namespace App.Services.Category;

public record CategoryWithProductsDto(int Id, string Name, List<ProductDto> Products);

