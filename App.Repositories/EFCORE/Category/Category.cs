namespace App.Repositories.EFCORE.Category;

public class Category
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public ICollection<Product.Product> Products { get; set; } = default!;
}

