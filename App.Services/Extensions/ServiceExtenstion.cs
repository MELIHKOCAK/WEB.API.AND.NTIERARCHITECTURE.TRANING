using App.Services.Categories;
using App.Services.ExceptionHandlers;
using App.Services.Filters.NotFoundFilter;
using App.Services.Products;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace App.Services.Extensions
{
    public static class ServiceExtenstion
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped(typeof(NotFoundFilter<,>));
            services.AddScoped(typeof(IIdResolver<>), typeof(PrimitiveIdResolver<>));
            services.AddScoped(typeof(IIdResolver<>), typeof(EntityIdResolver<>));
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());
            services.AddExceptionHandler<GlobalExceptionHandler>();

            return services;
        }
    }
}
