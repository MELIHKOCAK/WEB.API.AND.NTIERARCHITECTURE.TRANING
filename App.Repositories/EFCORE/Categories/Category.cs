namespace App.Repositories.EFCORE.Categories;
public class Category
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public ICollection<Products.Product> Products { get; set; } = default!;
}

