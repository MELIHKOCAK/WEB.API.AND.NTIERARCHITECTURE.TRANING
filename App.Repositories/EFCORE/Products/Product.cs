namespace App.Repositories.EFCORE.Products;
public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public Categories.Category Category { get; set; } = default!;
    public int CategoryId { get; set; }
}
