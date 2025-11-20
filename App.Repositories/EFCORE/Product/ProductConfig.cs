using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Repositories.EFCORE.Product;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        builder
            .Property(p => p.Name)
            .HasMaxLength(50);

        builder
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Stock).HasColumnType("int");
    }
}
