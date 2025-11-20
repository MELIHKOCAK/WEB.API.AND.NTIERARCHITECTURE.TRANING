using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace App.Repositories.EFCORE
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Product.Product> Products { get; set; }
        public DbSet<Category.Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
