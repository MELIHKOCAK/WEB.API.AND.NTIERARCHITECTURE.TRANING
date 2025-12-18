namespace App.Repositories.EFCORE.Categories;
public class Category: BaseEntity<int>
{
    public string? Name { get; set; } = default!;
    public ICollection<Products.Product> Products { get; set; } = default!;
}

