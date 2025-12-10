namespace App.Services.Products.Create;

public record CreateProductRequestDto(string Name, decimal Price, int Stock, int CategoryId);

