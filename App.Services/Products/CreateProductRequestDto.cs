namespace App.Services.Products;

public record CreateProductRequestDto(string name, decimal price, int stock);

