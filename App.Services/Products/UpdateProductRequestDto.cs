

namespace App.Services.Products;

public record UpdateProductRequestDto(int id, string name, decimal price, int stock);

