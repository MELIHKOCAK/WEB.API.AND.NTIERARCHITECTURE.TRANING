namespace App.Services.Products.Update;
public record UpdateProductRequestDto(int id, string name, decimal price, int stock, int categoryId);

