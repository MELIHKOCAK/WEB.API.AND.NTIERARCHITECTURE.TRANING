using App.Repositories.EFCORE;
using App.Repositories.EFCORE.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Repositories.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<AppDbContext>(options =>
             {
                 var connectionString = configuration.GetSection(ConnectionStringOption.Key)
                     .Get<ConnectionStringOption>();

                 options.UseSqlServer(connectionString!.SqlServer, sqlServerOptionsAction =>
                 {
                     sqlServerOptionsAction.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName);
                 });
             });

            service.AddScoped<IProductRepository, ProductRepository>();
            service.AddScoped(typeof(IGenericRepositoryBase<>), typeof(GenericRepositoryBase<>));
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            return service;
        }
    }
}
