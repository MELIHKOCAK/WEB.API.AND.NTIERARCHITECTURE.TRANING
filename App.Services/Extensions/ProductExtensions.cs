using App.Services.Category;
using App.Services.Products;
using Microsoft.Extensions.DependencyInjection;

namespace App.Services.Extensions
{
    public static class ProductExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}
