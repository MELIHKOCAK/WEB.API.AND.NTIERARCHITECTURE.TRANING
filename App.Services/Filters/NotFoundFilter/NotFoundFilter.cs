using App.Repositories;
using App.Services.Products;
using AutoMapper.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Update;
using System.Data.Common;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
namespace App.Services.Filters.NotFoundFilter;

public class NotFoundFilter<T, TId> : Attribute, IAsyncActionFilter 
    where T : class 
    where TId : struct, IConvertible, IComparable
{
    private readonly IGenericRepositoryBase<T, TId> _genericRepository;
    private readonly IEnumerable<IIdResolver<TId>> _strategies;

    public NotFoundFilter(IGenericRepositoryBase<T, TId> genericRepository, IEnumerable<IIdResolver<TId>> strategies)
    {
        _genericRepository = genericRepository;
        _strategies = strategies;

    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var value = context.ActionArguments.Values.FirstOrDefault();

        if (value == null)
        {
            await next();
            return;
        }

        var strategy = _strategies.FirstOrDefault(s => s.CanResolve(value!));

        if (strategy == null)
        {
            await next();
            return;
        }

        TId convertedId = strategy.Resolve(value!);
    
        if (await _genericRepository.AnyAsync(convertedId))
        {
            await next();
            return;
        }

        var entityName = typeof(T).Name;
        var actionName = context.ActionDescriptor.RouteValues["action"];
        var result = ServiceResult.Fail($"data bulunumamıştır ({entityName}) ({actionName}).");
        context.Result = new NotFoundObjectResult(result);
        return;
    }
}
